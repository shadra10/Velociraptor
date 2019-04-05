using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiner : MonoBehaviour
{
    GameObject prefabUsed;

    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/MinerVelociraptor", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.gameObject.tag == "Selected")
        {
            if (Camera.main.GetComponent<PlayerScript>().units < Camera.main.GetComponent<PlayerScript>().unitsMax)
            {
                if (Camera.main.GetComponent<PlayerScript>().eggs >= prefabUsed.GetComponent<Stats>().cost)
                {
                    GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x + 6.0F, transform.position.y + 0.15F, transform.position.z + 6.0F), Quaternion.identity);
                    objUsed.AddComponent(typeof(CollisionChecker));

                    Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                    Camera.main.GetComponent<PlayerScript>().units += 1;
                }
            }
        }
    }
}
