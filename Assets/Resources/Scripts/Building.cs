using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        selector = GameObject.FindWithTag("mainselector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<string> supplyWithActions()
    {
        List<string> output = new List<string>();
        //GameObject me = this.GetComponent<GameObject>();
        //Component comp =this.GetComponent<>
        string log = "";
        MonoBehaviour[] behave = GetComponents<MonoBehaviour>();


        if (GetComponent<SpawnMiner>() != null)
        {
            output.Add("Spawn Miner");
        }
        if (GetComponent<SpawnSpitter>() != null)
        {
            output.Add("Spawn Spitter");
        }
        if (GetComponent<SpawnWarrior>() != null)
        {
            output.Add("Spawn Warrior");
        }
        if (GetComponent<SpawnWizard>() != null)
        {
            output.Add("Spawn Wizard");
        }
        /*
        for (int i = 0; i < behave.Length; i++)
        {
            log += behave[i].name + " ";
            output.Add(behave[i].name);
        }*/

        return output;
    }

    void OnMouseDown()
    {
        Debug.Log("you clicked me!");
        if (this.GetComponent<Stats>().faction == 0)
        {
            selector.GetComponent<MainSelector>().selected = this.gameObject;
            selector.GetComponent<MainSelector>().resetButtons();
            GameObject[] oUsed = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in oUsed)
            {
                objUsed.tag = "None";
            }

            this.gameObject.tag = "Selected";

            GameObject select = GameObject.FindWithTag("SelectAll");
            if (select.GetComponent<SelectButton>().controller.GetComponent<PlayerScript>().selectAll)
            {

                select.GetComponent<SelectButton>().myImage.sprite = select.GetComponent<SelectButton>().notSelected;
                select.GetComponent<SelectButton>().controller.GetComponent<PlayerScript>().selectAll = false;
            }

            Debug.Log(transform.position);
        }
        else if (this.GetComponent<Stats>().faction == 1)
        {
            GameObject[] oUsed = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in oUsed)
            {
                objUsed.GetComponent<Unit>().target = this.gameObject;
                objUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;

                if (objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    objUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = this.gameObject.transform.position;
                }
            }
        }
    }
}
