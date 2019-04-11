using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthburst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && this.gameObject.tag == "Selected")
        {
            if (gameObject.GetComponent<Stats>().mana >= 35)
            {
                gameObject.GetComponent<Stats>().mana -= 35;

                Collider[] collArr = Physics.OverlapSphere(transform.position, 30.0F);

                foreach (Collider curColl in collArr)
                {
                    GameObject curObj = curColl.gameObject;

                    if (curObj.GetComponent<Stats>() != null)
                    {
                        if (curObj.GetComponent<Stats>().faction == 0)
                        {
                            curObj.GetComponent<Stats>().health += 15;

                            if (curObj.GetComponent<Stats>().health > curObj.GetComponent<Stats>().maxHealth)
                            {
                                curObj.GetComponent<Stats>().health = curObj.GetComponent<Stats>().maxHealth;
                            }
                        }
                    }
                }
            }
        }
    }
}
