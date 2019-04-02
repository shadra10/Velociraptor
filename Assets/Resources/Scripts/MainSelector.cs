using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSelector : MonoBehaviour
{
    public GameObject buildings, eggHolder, MapAndUI, selected;
    public List<GameObject> buttons;
    public List<string> actions;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetButtons()
    {
        int iter = 0;
        while (iter < buttons.Count)
        {
            if (iter < actions.Count)
            {
                Text myT = buttons[iter].GetComponent<Text>();
                myT.text = actions[iter];
                //buildings.GetComponentInChildren
                //GetComponent<Image>();
                
            }
            else
            {
                Text myT = buttons[iter].GetComponent<Text>();
                myT.text = "";
            }
            iter++;
        }
    }
}
