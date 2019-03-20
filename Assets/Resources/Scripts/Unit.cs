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
    public float speed = 1.0F;
    public GameObject target;
    public bool deathTimer = false;
    public bool isMoving = false;
    public Animation anim;

    void Start()
    {
        pos = tarPos = oldPos = this.transform.position;
        t = Time.time;

        this.GetComponent<Stats>().health = this.GetComponent<Stats>().maxHealth;

        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Stats>().health <= 0)
        {
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
                     Destroy(gameObject);
                }
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

                    t = Time.time;
                    pos = this.transform.position;
                }

                if (Vector3.Distance(transform.position, tarPos) <= range)
                {
                    tarPos = transform.position;
                    isMoving = false;
                    anim.CrossFade("Attack");

                    if (Time.time - attT >= attSpeed)
                    {

                        attT = Time.time;
                        target.GetComponent<Stats>().health -= dmg;

                        if (gameObject.GetComponent<Stats>().type == 1 && target.GetComponent<Stats>().faction == 2)
                        {
                            target.GetComponent<Stats>().health -= (dmg * 4);
                            Camera.main.GetComponent<PlayerScript>().eggs += (dmg * 5);
                        }
                    }
                }
                else
                {

                    // Distance moved = time * speed.
                    float distCovered = (Time.time - t) * speed;

                    // Fraction of journey completed = current distance divided by total distance.
                    float fracJourney = distCovered / Vector3.Distance(pos, tarPos);

                    // Set our position as a fraction of the distance between the markers.
                    if (this.transform.position != tarPos)
                    {
                        oldPos = this.transform.position;
                        this.transform.position = Vector3.Lerp(pos, tarPos, fracJourney);
                        //this.transform.Translate(this.transform.position);
                    }
                    else
                    {
                        pos = tarPos;
                    }
                }
            }
            else
            {
                isMoving = false;
                anim.CrossFade("Idle");
                // Distance moved = time * speed.
                float distCovered = (Time.time - t) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / Vector3.Distance(pos, tarPos);

                // Set our position as a fraction of the distance between the markers.
                if (this.transform.position != tarPos)
                {
                    isMoving = true;
                    anim.CrossFade("Walk");
                    oldPos = this.transform.position;
                    this.transform.position = Vector3.Lerp(pos, tarPos, fracJourney);
                    //this.transform.Translate(this.transform.position);
                }
                else
                {
                    pos = tarPos;
                }

            }

        }
    }

    void OnMouseDown() {
        if (this.GetComponent<Stats>().faction == 0)
        {
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed != null)
            {
                oUsed.tag = "None";
            }

            this.gameObject.tag = "Selected";

            Debug.Log(tarPos);
        }
        else if (this.GetComponent<Stats>().faction == 1)
        {
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed != null)
            {
                oUsed.GetComponent<Unit>().target = this.gameObject;
            }
        }

        else if (this.GetComponent<Stats>().faction == 2)
        {
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed.GetComponent<Stats>().type == 1)
            {
                if (oUsed != null)
                {
                    oUsed.GetComponent<Unit>().target = this.gameObject;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        this.transform.position = oldPos;

        tarPos = transform.position;
    }
}
