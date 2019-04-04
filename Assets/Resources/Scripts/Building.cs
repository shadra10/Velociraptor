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
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed != null)
            {
                oUsed.tag = "None";
            }

            this.gameObject.tag = "Selected";

            Debug.Log(transform.position);
        }
        else if (this.GetComponent<Stats>().faction == 1)
        {
            GameObject oUsed = GameObject.FindWithTag("Selected");

            if (oUsed != null)
            {
                oUsed.GetComponent<Unit>().target = this.gameObject;
                oUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;

                if (oUsed.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    oUsed.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = this.gameObject.transform.position;
                }
            }
        }
    }
}
