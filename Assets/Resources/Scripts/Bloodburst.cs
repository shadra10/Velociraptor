using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodburst : MonoBehaviour
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
        if (gameObject.GetComponent<Stats>().mana >= 60)
        {
            gameObject.GetComponent<Stats>().mana -= 60;

            GameObject burst = (GameObject)Resources.Load("Prefabs/Bloodburst", typeof(GameObject));
            Instantiate(burst, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            Collider[] collArr = Physics.OverlapSphere(transform.position, 10.0F);

            int totStolen = 0;
            List<GameObject> allyList = new List<GameObject>();

            foreach (Collider curColl in collArr)
            {
                GameObject curObj = curColl.gameObject;

                if (curObj.GetComponent<Stats>() != null)
                {
                    if (curObj.GetComponent<Stats>().faction == 0)
                    {
                        curObj.GetComponent<Stats>().health -= 5;
                        totStolen += 5;
                        //if (curObj.GetComponent<Stats>().health > curObj.GetComponent<Stats>().maxHealth)
                        //{
                        //    curObj.GetComponent<Stats>().health = curObj.GetComponent<Stats>().maxHealth;
                        //}
                    }

                    if (curObj.GetComponent<Stats>().faction == 1)
                    {
                        allyList.Add(curObj);
                    }
                }
            }

            totStolen = Mathf.FloorToInt(totStolen / allyList.Count);

            foreach (GameObject curO in allyList)
            {
                GameObject curObj = curO;

                if (curObj.GetComponent<Stats>() != null)
                {
                    curObj.GetComponent<Stats>().health += totStolen;

                    if (curObj.GetComponent<Stats>().health > curObj.GetComponent<Stats>().maxHealth)
                    {
                        curObj.GetComponent<Stats>().health = curObj.GetComponent<Stats>().maxHealth;
                    }
                }
            }
        }
    }
}
