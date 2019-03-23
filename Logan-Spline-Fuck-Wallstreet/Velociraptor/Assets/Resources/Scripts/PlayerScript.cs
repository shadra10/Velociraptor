using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int eggs, units, unitsMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Units - " + units + "/" + unitsMax);
            Debug.Log("Eggs - " + eggs);
        }
    }
}
