using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstExpand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale*1.2f;
        if (transform.localScale.x > 30)
            Destroy(gameObject);
    }
}
