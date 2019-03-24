using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > Screen.width - 10)
        {
            transform.position -= new Vector3(5 * Time.deltaTime, 0.0f, -5 * Time.deltaTime);
        }
        else if (Input.mousePosition.x < 0 + 10)
        {
            transform.position += new Vector3(5 * Time.deltaTime, 0.0f, -5 * Time.deltaTime);
        }

        if (Input.mousePosition.y > Screen.height - 10)
        {
            transform.position -= new Vector3(5 * Time.deltaTime, 0.0f, 5 * Time.deltaTime);
        }
        else if (Input.mousePosition.y < 0 + 10)
        {
            transform.position += new Vector3(5 * Time.deltaTime, 0.0f, 5 * Time.deltaTime);
        }

        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * 5;
        fov = Mathf.Clamp(fov, 15, 45);
        Camera.main.fieldOfView = fov;
    }
}
