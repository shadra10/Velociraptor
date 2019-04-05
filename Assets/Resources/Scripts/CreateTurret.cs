using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    GameObject prefabUsed;

    // Start is called before the first frame update
    void Start()
    {
        prefabUsed = (GameObject)Resources.Load("Prefabs/SpitterTower", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && this.gameObject.tag == "Selected")
        {
            if (Camera.main.GetComponent<PlayerScript>().units < Camera.main.GetComponent<PlayerScript>().unitsMax) {
                if (Camera.main.GetComponent<PlayerScript>().eggs >= prefabUsed.GetComponent<Stats>().cost) {
                    GameObject objUsed = Instantiate(prefabUsed, new Vector3(transform.position.x + 2.0F, transform.position.y + 4.349F, transform.position.z + 2.0F), Quaternion.identity);
                    objUsed.AddComponent(typeof(CollisionChecker));

                    Camera.main.GetComponent<PlayerScript>().eggs -= prefabUsed.GetComponent<Stats>().cost;
                }
            }
        }
    }
}
