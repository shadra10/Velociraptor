using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Sprite selected, notSelected;
    public Image myImage;
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
        /*GameObject select = GameObject.FindWithTag("mainselector");
        if (select.GetComponent<MainSelector>().controller.GetComponent<PlayerScript>().selectAll)
        {

            select.GetComponent<MainSelector>().myImage.sprite = notSelected;
            select.GetComponent<MainSelector>().controller.GetComponent<PlayerScript>().selectAll = false;
        }*/
        Debug.Log("YO HO");
        if (controller.GetComponent<PlayerScript>().selectAll)
        {

            myImage.sprite = notSelected;
            controller.GetComponent<PlayerScript>().selectAll = false;

            GameObject[] objList = GameObject.FindGameObjectsWithTag("Selected");

            foreach (GameObject objUsed in objList)
            {
                if (objUsed.GetComponent<Unit>() != null) {
                    objUsed.gameObject.tag = "None";
                }
            }
        }
        else
        {
            myImage.sprite = selected;
            controller.GetComponent<PlayerScript>().selectAll = true;

            GameObject[] objList = GameObject.FindGameObjectsWithTag("None");

            foreach (GameObject objUsed in objList)
            {
                if (objUsed.GetComponent<Unit>() != null)
                {
                    objUsed.gameObject.tag = "Selected";
                }
            }
        }
    }
}
