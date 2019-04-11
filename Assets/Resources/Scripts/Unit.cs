using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 pos, oldPos;
    public Vector3 tarPos;

    public int dmg;
    public float t, attT;
    public float range, attSpeed;
    public float speed = 2.0F;
    public GameObject target;
    GameObject selector;
    public bool deathTimer = false;
    public bool isMoving = false;
    public Animation anim;
    public TerrainData terrain;
    public UnityEngine.AI.NavMeshAgent aUsed;

    void Start()
    {
        aUsed = GetComponent<UnityEngine.AI.NavMeshAgent>();
        pos = tarPos = oldPos = this.transform.position;
        t = Time.time;
        selector=GameObject.FindWithTag("mainselector");
        this.GetComponent<Stats>().health = this.GetComponent<Stats>().maxHealth;
        this.anim = GetComponent<Animation>();
        if (anim != null)
        {
            foreach (AnimationState state in anim)
            {
                state.speed = 0.5F;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Stats>().health <= 0)
        {
            if (gameObject.GetComponent<Animation>() != null) {
                if (!deathTimer)
                {
                    if (isMoving)
                        anim.CrossFade("Move Die");
                    else
                        anim.CrossFade("Stand Die");

                    deathTimer = true;
                }
                else
                {
                    if (!anim.IsPlaying("Move Die") && !anim.IsPlaying("Stand Die"))
                    {
                        if (GetComponent<Stats>().faction == 0)
                        {
                            Camera.main.GetComponent<PlayerScript>().units -= 1;
                        }

                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {

            if (target != null)
            {
                isMoving = true;
                anim.CrossFade("Walk");
                if (tarPos != target.transform.position)
                {
                    tarPos = target.transform.position;
                    if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                    {
                        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.transform.position;
                    }

                    t = Time.time;
                    pos = this.transform.position;
                }

                if (Vector3.Distance(transform.position, tarPos) <= range)
                {
                    tarPos = transform.position;
                    if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                    {
                        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
                    }

                    isMoving = false;
                    anim.CrossFade("Attack");

                    if (this.GetComponent<HSplineMove>() != null)
                    {
                        Destroy(this.GetComponent<HSplineMove>().path[0]);
                        Destroy(this.GetComponent<HSplineMove>().path[1]);
                        Destroy(this.GetComponent<HSplineMove>());
                    }

                    if (Time.time - attT >= attSpeed)
                    {
                        Vector3 deltaVec = target.transform.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(deltaVec);
                        transform.rotation = rotation;


                        attT = Time.time;

                        if (range < 10)
                            target.GetComponent<Stats>().health -= dmg;
                        else if(range < 25)
                        {
                            GameObject firebolto = (GameObject)Resources.Load("PyroParticles/Prefab/Prefab/Spit");
                            Vector3 dir;

                            dir = transform.position + (transform.forward * 5);

                            dir.y += 1;

                            Instantiate(firebolto, dir, this.transform.rotation);

                        }
                        else
                        {
                            GameObject firebolto = (GameObject)Resources.Load("PyroParticles/Prefab/Prefab/Firebolt");
                            Vector3 dir;

                            dir = transform.position + (transform.forward * 5);

                            dir.y += 1;

                            Instantiate(firebolto, dir, this.transform.rotation);

                        }

                        if (gameObject.GetComponent<Stats>().type == 1 && target.GetComponent<Stats>().faction == 2)
                        {
                            target.GetComponent<Stats>().health -= (dmg * 4);
                            if (gameObject.GetComponent<Stats>().faction == 0)
                            {
                                Camera.main.GetComponent<PlayerScript>().eggs += (dmg * 5);
                            } else
                            {
                                Camera.main.GetComponent<EnemyScript>().eggs += (dmg * 5);
                            }
                        }
                    }
                }
            } else if (this.GetComponent<UnityEngine.AI.NavMeshAgent>() != null){
                if (this.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity != new Vector3(0.0f, 0.0f, 0.0f)) {
                    isMoving = true;
                    if (GetComponent<Animation>() != null) {
                        anim.CrossFade("Walk");
                    }
                }
                else
                {
                    isMoving = false;
                    if (GetComponent<Animation>() != null)
                    {
                        anim.CrossFade("Idle");
                    }
                }
            }
            else {
                isMoving = false;
                if (GetComponent<Animation>() != null)
                {
                    anim.CrossFade("Idle");
                }
            }
        }

        if (terrain != null)
        {
            pos.y = terrain.GetHeight((int)this.transform.position.x, (int)this.transform.position.z);
        }
    }

    void OnMouseDown() {
        if (this.GetComponent<Stats>().faction == 0)
        {
            selector.GetComponent<MainSelector>().selected = this.gameObject;
            selector.GetComponent<MainSelector>().resetButtons();

            GameObject[] oUsed = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in oUsed)
            {
                objUsed.tag = "None";
            }

            this.gameObject.tag = "Selected";

            GameObject select = GameObject.FindWithTag("SelectAll");
            if (select.GetComponent<SelectButton>().controller.GetComponent<PlayerScript>().selectAll)
            {

                select.GetComponent<SelectButton>().myImage.sprite = select.GetComponent<SelectButton>().notSelected;
                select.GetComponent<SelectButton>().controller.GetComponent<PlayerScript>().selectAll = false;
            }

            Debug.Log(tarPos);
        }
        else if (this.GetComponent<Stats>().faction == 1)
        {
            GameObject[] oUsed = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in oUsed)
            {
                objUsed.GetComponent<Unit>().target = this.gameObject;
                objUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;

                if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = this.gameObject.transform.position;
                }
            }
        }

        else if (this.GetComponent<Stats>().faction == 2)
        {
            GameObject[] oUsed = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in oUsed)
            {
                if (objUsed.GetComponent<Stats>().type == 1)
                {
                    objUsed.GetComponent<Unit>().target = this.gameObject;
                    objUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;

                    if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                    {
                        objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = this.gameObject.transform.position;
                    }
                }
            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    this.transform.position = oldPos;
    //
    //    tarPos = transform.position;
    //
    //    if (this.GetComponent<HSplineMove>() != null)
    //    {
    //        Destroy(this.GetComponent<HSplineMove>().path[0]);
    //        Destroy(this.GetComponent<HSplineMove>().path[1]);
    //        Destroy(this.GetComponent<HSplineMove>());
    //    }
    //}
}
