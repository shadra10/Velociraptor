using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpitter : MonoBehaviour
{
    GameObject prefabUsed;

    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/SpitterVelociraptor", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && this.gameObject.tag == "Selected")
        {
            if (Camera.main.GetComponent<PlayerScript>().units < Camera.main.GetComponent<PlayerScript>().unitsMax)
            {
                if (Camera.main.GetComponent<PlayerScript>().eggs >= prefabUsed.GetComponent<Stats>().cost)
                {
                    GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x - 3.0F, transform.position.y + 12.15F, transform.position.z + 20.0F), Quaternion.identity);
                    objUsed.AddComponent(typeof(CollisionChecker));

                    Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                    Camera.main.GetComponent<PlayerScript>().units += 1;
                }
            }
        }
    }

    public bool spawn()
    {
        if (Camera.main.GetComponent<PlayerScript>().units < Camera.main.GetComponent<PlayerScript>().unitsMax)
        {
            if (Camera.main.GetComponent<PlayerScript>().eggs >= prefabUsed.GetComponent<Stats>().cost)
            {
                GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x - 1.0F, 0.05F, transform.position.z + 8.0F), Quaternion.identity);
                objUsed.AddComponent(typeof(CollisionChecker));

                Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                Camera.main.GetComponent<PlayerScript>().units += 1;
            }
        }
        return false;
    }
}
