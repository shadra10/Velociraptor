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
                StartCoroutine("moveSet");
            }
        }
    }

    IEnumerator moveSet()
    {
        GameObject objUsed = GameObject.FindWithTag("Selected");
        GameObject tempStart = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), objUsed.transform.position, objUsed.transform.rotation);
        GameObject tempEnd = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), this.gameObject.transform.position, Quaternion.identity);

        objUsed.GetComponent<Unit>().tarPos = this.gameObject.transform.position;
        objUsed.GetComponent<Unit>().t = Time.time;
        objUsed.GetComponent<Unit>().pos = GameObject.FindWithTag("Selected").GetComponent<Unit>().transform.position;
        objUsed.GetComponent<Unit>().target = this.gameObject;

        if (objUsed.GetComponent<HSplineMove>() != null)
        {
            Destroy(objUsed.GetComponent<HSplineMove>().path[0]);
            Destroy(objUsed.GetComponent<HSplineMove>().path[1]);
            Destroy(objUsed.GetComponent<HSplineMove>());
            yield return new WaitForSeconds(0.05f);
        }
        objUsed.AddComponent(typeof(HSplineMove));
        objUsed.GetComponent<HSplineMove>().speed = objUsed.GetComponent<Unit>().speed;
        objUsed.GetComponent<HSplineMove>().path = new GameObject[2];
        objUsed.GetComponent<HSplineMove>().path[0] = tempStart;
        objUsed.GetComponent<HSplineMove>().path[1] = tempEnd;

        yield return null;
    }
}
