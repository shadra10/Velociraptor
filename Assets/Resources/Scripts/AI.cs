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

            objUsed.GetComponent<Unit>().target = null;
            objUsed.GetComponent<Unit>().tarPos = formup;

            if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = formup;

            }
            combatUnits.Add(objUsed);
        }

        if (combatUnits.Count >= 5)
        {
            for (int i = 0; i < combatUnits.Count; i++)
            {
                if (combatUnits[i] == null)
                {
                    combatUnits.RemoveAt(i);
                    i--;
                    continue;
                }

                GameObject target = findTarget();
                if (target == null)
                    break;
                combatUnits[i].GetComponent<Unit>().target = target;
                combatUnits[i].GetComponent<Unit>().tarPos = target.gameObject.transform.position;

                if (combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    combatUnits[i].GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.gameObject.transform.position;
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
