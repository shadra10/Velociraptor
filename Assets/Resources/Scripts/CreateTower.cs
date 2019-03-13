using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    GameObject prefabUsed;

    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/Stonehenge", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && this.gameObject.tag == "Selected")
        {
            //Collider[] collArr = Physics.OverlapBox(new Vector3(transform.position.x + 0.5F, 0.75F, transform.position.z + 7.0F), new Vector3(3.5F, 0.74F, 3.5F));
            //if (collArr.Length <= 0)
            //{
            if (Camera.main.GetComponent<PlayerScript>().units < Camera.main.GetComponent<PlayerScript>().unitsMax) {
                if (Camera.main.GetComponent<PlayerScript>().eggs >= prefabUsed.GetComponent<Stats>().cost) {
                    Instantiate(prefabUsed, new Vector3(transform.position.x + 2.0F, 0, transform.position.z + 2.0F), Quaternion.identity);

                    Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                    Camera.main.GetComponent<PlayerScript>().units += 1;
                }
            }
            //}
        }
    }
}
