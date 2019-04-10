using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int eggs, units, unitsMax;
    public bool selectAll;
    // Start is called before the first frame update
    void Start()
    {
        selectAll = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Enemy Units - " + units + "/" + unitsMax);
            Debug.Log("Enemy Eggs - " + eggs);
        }
    }
}
