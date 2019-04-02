using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    GameObject mainFoe = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collArr = Physics.OverlapSphere(transform.position, 20.0F);
        int foeList = 0;
        int allyList = 0;
        GameObject tarFoe = null;

        foreach (Collider curColl in collArr)
        {
            GameObject curObj = curColl.gameObject;

            if (curObj.GetComponent<Stats>() != null)
            {

                if (curObj.GetComponent<Stats>().faction == 0)
                {
                    foeList++;
                    Debug.Log("El Stupido");

                    if (tarFoe != null)
                    {
                        if (tarFoe.GetComponent<Stats>().health > curObj.GetComponent<Stats>().health)
                        {
                            tarFoe = curObj;
                        }
                    } else
                    {
                        tarFoe = curObj;
                    }
                }

                if (curObj.GetComponent<Stats>().faction == 1)
                {
                    allyList++;
                }
            }
        }

        if (foeList * 2 >= allyList)
        {
            //Flee behaviour
        } else
        {
            if (tarFoe != mainFoe)
            {
                mainFoe = tarFoe;
                StartCoroutine("moveSet");
            }
        }
    }

    IEnumerator moveSet()
    {

        Debug.Log("I'M HERE");
        GameObject objUsed = mainFoe;
        GameObject tempStart = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), this.gameObject.transform.position, gameObject.transform.rotation);
        GameObject tempEnd = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), objUsed.transform.position, Quaternion.identity);

        gameObject.GetComponent<Unit>().tarPos = objUsed.transform.position;
        gameObject.GetComponent<Unit>().t = Time.time;
        gameObject.GetComponent<Unit>().pos = gameObject.transform.position;
        gameObject.GetComponent<Unit>().target = objUsed.gameObject;

        if (gameObject.GetComponent<HSplineMove>() != null)
        {
            Destroy(gameObject.GetComponent<HSplineMove>().path[0]);
            Destroy(gameObject.GetComponent<HSplineMove>().path[1]);
            Destroy(gameObject.GetComponent<HSplineMove>());
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.AddComponent(typeof(HSplineMove));
        gameObject.GetComponent<HSplineMove>().speed = gameObject.GetComponent<Unit>().speed;
        gameObject.GetComponent<HSplineMove>().path = new GameObject[2];
        gameObject.GetComponent<HSplineMove>().path[0] = tempStart;
        gameObject.GetComponent<HSplineMove>().path[1] = tempEnd;

        yield return null;
    }
}
