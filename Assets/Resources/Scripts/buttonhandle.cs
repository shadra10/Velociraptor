using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonhandle : MonoBehaviour
{
    GameObject selector;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        selector = GameObject.FindWithTag("mainselector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked()
    {
        string send = "";
        send = myText.text;
       selector.GetComponent<MainSelector>().clicked(send);

    }
}
