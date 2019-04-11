using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soulburst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSpell()
    {
        if (gameObject.GetComponent<Stats>().mana >= 30)
        {
            gameObject.GetComponent<Stats>().health += 10;
            gameObject.GetComponent<Stats>().mana -= 30;

            GameObject burst = (GameObject)Resources.Load("Prefabs/Soulburst", typeof(GameObject));
            Instantiate(burst, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            Collider[] collArr = Physics.OverlapSphere(transform.position, 10.0F);

            foreach (Collider curColl in collArr)
            {
                GameObject curObj = curColl.gameObject;

                if (curObj.GetComponent<Stats>() != null)
                {
                    if (curObj.GetComponent<Stats>().faction != 2)
                    {
                        curObj.GetComponent<Stats>().health -= 10;
                    }
                }
            }
        }
    }
}
