using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireburst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.gameObject.tag == "Selected")
        {
            if (gameObject.GetComponent<Stats>().mana >= 50)
            {
                gameObject.GetComponent<Stats>().health += 15;
                gameObject.GetComponent<Stats>().mana -= 50;

                Collider[] collArr = Physics.OverlapSphere(transform.position, 10.0F);

                foreach (Collider curColl in collArr)
                {
                    GameObject curObj = curColl.gameObject;

                    if (curObj.GetComponent<Stats>() != null)
                    {
                        if (curObj.GetComponent<Stats>().faction != 2)
                        {
                            curObj.GetComponent<Stats>().health -= 15;
                        }
                    }
                }
            }
        } 
    }
}
