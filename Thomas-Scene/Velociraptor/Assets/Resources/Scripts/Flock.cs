using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject leader;
    public List<GameObject> units;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            units.Add(child.gameObject);
            // int i = units.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
