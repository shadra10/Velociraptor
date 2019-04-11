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
