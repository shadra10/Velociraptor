using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.gameObject.tag == "Selected") {
            Destroy(gameObject);
        }
    }
}
