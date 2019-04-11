using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int dmg;
    public float attT, range, attSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collArr = Physics.OverlapSphere(transform.position, range);

        foreach (Collider curColl in collArr)
        {
            GameObject curObj = curColl.gameObject;

            if (curObj.GetComponent<Stats>() != null)
            {

                if (curObj.GetComponent<Stats>().faction != GetComponent<Stats>().faction && curObj.GetComponent<Stats>().faction != 2)
                {
                    Debug.Log("El Stupido");

                    if (Time.time - attT >= attSpeed)
                    {
                        attT = Time.time;

                        Vector3 deltaVec = curObj.transform.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(deltaVec);


                        GameObject firebolto = (GameObject)Resources.Load("PyroParticles/Prefab/Prefab/Spit");
                        Vector3 dir;

                        dir = transform.position + (transform.forward * 5);

                        dir.y += 2.5f;

                        Instantiate(firebolto, dir, rotation);
                        curObj.GetComponent<Stats>().health -= dmg;
                        break;
                    }
                }
            }
        }

        /*GameObject[] objList = GameObject.FindGameObjectsWithTag("None");

        foreach (GameObject objUsed in objList)
        {
            if (objUsed.GetComponent<Unit>() != null)
            {
                if (Time.time - attT >= attSpeed)
                {
                    objUsed.gameObject.tag = "Selected";
                }
            }
        }*/
    }
}
