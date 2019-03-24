using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        StartCoroutine("moveSet");
    }

    IEnumerator moveSet()
    {
        Ray rayUsed = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane pUsed = new Plane(Vector3.up, transform.position);
        float distUsed = 0;
        if (pUsed.Raycast(rayUsed, out distUsed))
        {
            Vector3 clickPos = rayUsed.GetPoint(distUsed);
            //Debug.Log(clickPos);
            GameObject objUsed = GameObject.FindWithTag("Selected");
            GameObject tempStart = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), objUsed.transform.position, objUsed.transform.rotation);
            GameObject tempEnd = Instantiate((GameObject)Resources.Load("Prefabs/Target", typeof(GameObject)), clickPos, Quaternion.identity);

            objUsed.GetComponent<Unit>().tarPos = clickPos;
            objUsed.GetComponent<Unit>().t = Time.time;
            objUsed.GetComponent<Unit>().pos = GameObject.FindWithTag("Selected").GetComponent<Unit>().transform.position;
            objUsed.GetComponent<Unit>().target = null;

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

            Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().tarPos);
            Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().pos);
        }

        yield return null;
    }
}
