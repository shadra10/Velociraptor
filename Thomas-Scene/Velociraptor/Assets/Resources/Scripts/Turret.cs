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
        GameObject[] objList = GameObject.FindGameObjectsWithTag("Foe");

        foreach (GameObject objUsed in objList)
        {
            if (Vector3.Distance(transform.position, objUsed.transform.position) <= range)
            {
                if (Time.time - attT >= attSpeed)
                {
                    attT = Time.time;
                    objUsed.GetComponent<Stats>().health -= dmg;
                    break;
                }
            }
        }
    }
}
