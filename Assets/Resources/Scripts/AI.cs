using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    int count = 0;
    GameObject prefabUsed;
    public GameObject placeUnder;
    public GameObject buildings;
    //public List<GameObject> targets;
    public List<GameObject> combatUnits=new List<GameObject>();
    GameObject theTarget;
    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/NecroKnight", typeof(GameObject));

    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time/10;

        if (time > count)
        {
            count++;

            GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x - 3.0F, transform.position.y + 12.15F, transform.position.z + 20.0F), Quaternion.identity);
            objUsed.AddComponent(typeof(CollisionChecker));
            objUsed.transform.parent = placeUnder.transform;
            Vector3 formup = new Vector3(-494f, 3f, 67.32f);

            combatUnits.Add(objUsed);
            if (combatUnits.Count >= 5&&theTarget!=null)
            {
               
                   
                objUsed.GetComponent<Unit>().target = theTarget;
                objUsed.GetComponent<Unit>().tarPos = theTarget.gameObject.transform.position;

                if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = theTarget.gameObject.transform.position;
                }
            }
            else
            {
                objUsed.GetComponent<Unit>().target = null;
                objUsed.GetComponent<Unit>().tarPos = formup;

                if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = formup;

                }
            }
            
        }

        if (combatUnits.Count >= 5)
        {
            GameObject temptarget= findTarget();
            if (theTarget != temptarget)
            {
                theTarget = temptarget;
                for (int i = 0; i < combatUnits.Count; i++)
                {
                    if (combatUnits[i] == null)
                    {
                        combatUnits.RemoveAt(i);
                        i--;
                        continue;
                    }

                    //GameObject target = findTarget();
                    if (theTarget == null)
                        break;
                    combatUnits[i].GetComponent<Unit>().target = theTarget;
                    combatUnits[i].GetComponent<Unit>().tarPos = theTarget.gameObject.transform.position;

                    if (combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                    {
                        combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination = theTarget.gameObject.transform.position;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < combatUnits.Count; i++)
            {
                if (combatUnits[i] == null)
                {
                    combatUnits.RemoveAt(i);
                    i--;
                    continue;
                }

                Vector3 formup = new Vector3(-494f, 3f, 67.32f);

                combatUnits[i].GetComponent<Unit>().target = null;
                combatUnits[i].GetComponent<Unit>().tarPos = formup;

                if (combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination = formup;
                }
            }
        }

    }


    GameObject findTarget()
    {
        foreach (Transform child in (buildings.transform))
        {
            if (child.GetComponent<Stats>().faction == 0)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
