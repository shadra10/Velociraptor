using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWizard : MonoBehaviour
{
    GameObject prefabUsed;

    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/WizardVelociraptor", typeof(GameObject));
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
                    GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x - 0.0F, transform.position.y + 12.15F, transform.position.z + 15.0F), Quaternion.identity);
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
                GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x - 4.0F, 0.05F, transform.position.z + 10.0F), Quaternion.identity);
                objUsed.AddComponent(typeof(CollisionChecker));

                Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                Camera.main.GetComponent<PlayerScript>().units += 1;
            }
        }
        return false;
    }
}
