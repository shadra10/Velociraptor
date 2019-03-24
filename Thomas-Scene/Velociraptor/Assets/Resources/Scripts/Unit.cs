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
    GameObject selector;

    void Start()
    {
        pos = tarPos = oldPos = this.transform.position;
        t = Time.time;
        selector=GameObject.FindWithTag("mainselector");
        this.GetComponent<Stats>().health = this.GetComponent<Stats>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Stats>().health <= 0)
        {
            Destroy(gameObject);
        }

        if (target != null)
        {
            if (tarPos != target.transform.position)
            {
                tarPos = target.transform.position;

                t = Time.time;
                pos = this.transform.position;
            }

            if (Vector3.Distance(transform.position, tarPos) <= range)
            {
                tarPos = transform.position;

                if (this.GetComponent<HSplineMove>() != null)
                {
                    Destroy(this.GetComponent<HSplineMove>().path[0]);
                    Destroy(this.GetComponent<HSplineMove>().path[1]);
                    Destroy(this.GetComponent<HSplineMove>());
                }

                if (Time.time - attT >= attSpeed)
                {
                    attT = Time.time;
                    target.GetComponent<Stats>().health -= dmg;
                    
                    if (gameObject.GetComponent<Stats>().type == 1 && target.GetComponent<Stats>().faction == 2)
                    {
                        target.GetComponent<Stats>().health -= (dmg*4);
                        Camera.main.GetComponent<PlayerScript>().eggs += (dmg * 5);
                    }
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
                StartCoroutine("moveSet");


            }
        }

        else if (this.GetComponent<Stats>().faction == 2)
        {
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed.GetComponent<Stats>().type == 1)
            {
                if (oUsed != null)
                {
                    StartCoroutine("moveSet");
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        this.transform.position = oldPos;

        tarPos = transform.position;

        if (this.GetComponent<HSplineMove>() != null)
        {
            Destroy(this.GetComponent<HSplineMove>().path[0]);
            Destroy(this.GetComponent<HSplineMove>().path[1]);
            Destroy(this.GetComponent<HSplineMove>());
        }
    }

    IEnumerator moveSet()
    {
        Debug.Log("I'M HERE");
        GameObject objUsed = GameObject.FindWithTag("Selected");
        GameObject tempStart = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), objUsed.transform.position, objUsed.transform.rotation);
        GameObject tempEnd = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), this.gameObject.transform.position, Quaternion.identity);

        objUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;
        objUsed.GetComponent<Unit>().t = Time.time;
        objUsed.GetComponent<Unit>().pos = GameObject.FindWithTag("Selected").GetComponent<Unit>().transform.position;
        objUsed.GetComponent<Unit>().target = this.gameObject;

        if (objUsed.GetComponent<HSplineMove>() != null)
        {
            Destroy(objUsed.GetComponent<HSplineMove>().path[0]);
            Destroy(objUsed.GetComponent<HSplineMove>().path[1]);
            Destroy(objUsed.GetComponent<HSplineMove>());
            yield return new WaitForSeconds(0.05f);
        }
        objUsed.AddComponent(typeof(HSplineMove));
        objUsed.GetComponent<HSplineMove>().speed = objUsed.GetComponent<Unit>().speed;
        objUsed.GetComponent<HSplineMove>().path = new GameObject[2];
        objUsed.GetComponent<HSplineMove>().path[0] = tempStart;
        objUsed.GetComponent<HSplineMove>().path[1] = tempEnd;

        yield return null;
    }
}
