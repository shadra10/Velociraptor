using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Sprite selected, notSelected;
    Image myImage;
    public GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        myImage= GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pressed()
    {
        Debug.Log("YO HO");
        if (controller.GetComponent<PlayerScript>().selectAll)
        {

            myImage.sprite = notSelected;
            controller.GetComponent<PlayerScript>().selectAll = false;
        }
        else
        {
            myImage.sprite = selected;
            controller.GetComponent<PlayerScript>().selectAll = true;
        }
    }
}
