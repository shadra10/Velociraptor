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
        Ray rayUsed = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane pUsed = new Plane(Vector3.up, transform.position);
        float distUsed = 0;
        if (pUsed.Raycast(rayUsed, out distUsed))
        {
            Vector3 clickPos = rayUsed.GetPoint(distUsed);
            //Debug.Log(clickPos);

            GameObject.FindWithTag("Selected").GetComponent<Unit>().tarPos = clickPos;
            GameObject.FindWithTag("Selected").GetComponent<Unit>().t = Time.time;
            GameObject.FindWithTag("Selected").GetComponent<Unit>().pos = GameObject.FindWithTag("Selected").GetComponent<Unit>().transform.position;
            GameObject.FindWithTag("Selected").GetComponent<Unit>().target = null;

            Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().tarPos);
            Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().pos);
        }

        //Debug.Log(clickPos);
        //GameObject.FindWithTag("Selected").GetComponent<Unit>().tarPos.position = clickPos;

        //Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().tarPos.position);
        //Debug.Log(GameObject.FindWithTag("Selected").GetComponent<Unit>().pos.position);
    }
}
