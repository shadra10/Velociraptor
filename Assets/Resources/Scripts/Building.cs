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

    void OnMouseDown()
    {
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
