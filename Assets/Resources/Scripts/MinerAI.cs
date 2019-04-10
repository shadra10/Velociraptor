using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAI : MonoBehaviour
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
        int oreList = 0;
        GameObject tarFoe = null;
        GameObject tarOre = null;

        foreach (Collider curColl in collArr)
        {
            GameObject curObj = curColl.gameObject;

            if (curObj.GetComponent<Stats>() != null)
            {
                if (curObj.GetComponent<Stats>().faction == 2)
                {
                    oreList++;
                    Debug.Log("El Stupido");

                    if (tarOre == null)
                    {
                        tarOre = curObj;
                    }
                }else if (curObj.GetComponent<Stats>().faction == GetComponent<Stats>().faction)
                {
                    allyList++;
                }

                if (curObj.GetComponent<Stats>().faction == 0 && GetComponent<Stats>().faction == 1)
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

                if (curObj.GetComponent<Stats>().faction == 1 && GetComponent<Stats>().faction == 0)
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
            }
        }


        if (foeList == 0)
        {
            if (oreList != 0)
            {
                if (tarOre != mainFoe)
                {
                    mainFoe = tarOre;
                    GetComponent<Unit>().target = mainFoe;
                    GetComponent<Unit>().tarPos = mainFoe.transform.position;
                    GetComponent<UnityEngine.AI.NavMeshAgent>().destination = mainFoe.transform.position;
                }
            } else
            {
                mainFoe = null;
                /*if (GetComponent<Unit>().target != null) {
                    GetComponent<Unit>().target = null;
                    GetComponent<Unit>().tarPos = transform.position;
                    GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
                }*/
            }
        }
        else if (foeList >= allyList)
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
