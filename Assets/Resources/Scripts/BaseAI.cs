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
        Collider[] collArr = Physics.OverlapSphere(transform.position, 30.0F);
        int foeList = 0;
        int allyList = 0;
        int allyHealth = 0;
        GameObject tarFoe = null;

        foreach (Collider curColl in collArr)
        {
            GameObject curObj = curColl.gameObject;

            if (curObj.GetComponent<Stats>() != null)
            {

                if (curObj.GetComponent<Stats>().faction != GetComponent<Stats>().faction && curObj.GetComponent<Stats>().faction != 2)
                {
                    foeList++;
                    Debug.Log("El Stupido");

                    if (tarFoe != null)
                    {
                        if (tarFoe.GetComponent<Stats>().health > curObj.GetComponent<Stats>().health)
                        {
                            tarFoe = curObj;
                        }
                    }
                    else
                    {
                        tarFoe = curObj;
                    }
                }

                if (curObj.GetComponent<Stats>().faction == 1)
                {
                    allyList++;
                    allyHealth += (curObj.GetComponent<Stats>().maxHealth - curObj.GetComponent<Stats>().health);
                }
            }
        }

        if (foeList == 0)
        {
            mainFoe = null;
            /*if (GetComponent<Unit>().target != null)
            {
                GetComponent<Unit>().target = null;
                GetComponent<Unit>().tarPos = transform.position;
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
            }*/
        }
        else if (foeList * 2 >= allyList)
        {
            //Flee behaviour
        }
        else
        {
            if (tarFoe != mainFoe)
            {
                mainFoe = tarFoe;
                GetComponent<Unit>().target = mainFoe;
                GetComponent<Unit>().tarPos = mainFoe.transform.position;
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = mainFoe.transform.position;
            }
        }
    }
}
