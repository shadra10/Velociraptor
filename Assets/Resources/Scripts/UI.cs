using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Texture aTexture;

    public int width;
    public int height;
    //public List<GameObject> units=new List<GameObject>();
    public List<GameObject> units;

    public GameObject hoverUsed;
    public Text eggs, unitTxt, sName, sHealth, sMana, hName, hHealth, hMana;
    int eggNum;
    // Start is called before the first frame update
    void Start()
    {
        eggNum = 0;
        foreach (Transform child in transform)
        {
            units.Add(child.gameObject);
           // int i = units.Count;
        }
        
        /*
        foreach (Transform child in EggParent.transform)
        {
            playerEggs.Add(child.gameObject);
            // int i = units.Count;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        eggs.text ="Egg Count: " + Camera.main.GetComponent<PlayerScript>().eggs;
        unitTxt.text = "Units: " + Camera.main.GetComponent<PlayerScript>().units + "/" + Camera.main.GetComponent<PlayerScript>().unitsMax;

        GameObject oUsed = GameObject.FindWithTag("Selected");
        if (oUsed != null)
        {
            sName.text = oUsed.name;
            sHealth.text = "Health: " + oUsed.GetComponent<Stats>().health + "/" + oUsed.GetComponent<Stats>().maxHealth;
            sMana.text = "Mana: " + oUsed.GetComponent<Stats>().mana + "/" + oUsed.GetComponent<Stats>().manaMax;
        } else
        {
            sName.text = "N/A";
            sHealth.text = "Health: N/A";
            sMana.text = "Mana: N/A";
        }

        if (hoverUsed != null)
        {
            hName.text = hoverUsed.name;
            hHealth.text = "Health: " + hoverUsed.GetComponent<Stats>().health + "/" + hoverUsed.GetComponent<Stats>().maxHealth;
            hMana.text = "Mana: " + hoverUsed.GetComponent<Stats>().mana + "/" + hoverUsed.GetComponent<Stats>().manaMax;
        }
        else
        {
            hName.text = "N/A";
            hHealth.text = "Health: N/A";
            hMana.text = "Mana: N/A";
        }
    }

    void OnGUI()
    {
        width = 100;
        height = 100;

        Texture2D texture = new Texture2D(width, height);
        //GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
               // Color color = ((x & y) != 0 ? Color.white : Color.gray);
                texture.SetPixel(x, y, Color.black);
            }
        }
        int offset = 50;
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] == null)
            {
                units.RemoveAt(i);
                if (!(i < units.Count))
                {
                    break;
                }
            }
            Transform current = units[i].GetComponent<Transform>();
            for (int x = 0; x < 3 && current.localPosition.x + x <= width+100; x++)
            {
                for (int y = 0; y < 3 && current.localPosition.z + y <= height+100; y++)
                {
                    texture.SetPixel((int)current.localPosition.x + x+offset, (int)current.localPosition.z + y+offset, Color.blue);
                }
            }
        }
        // for (unitX.)

        texture.Apply();
        
        if (!aTexture)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }
        float stretchX = Screen.height / 600.0f;
        float myHeight = Screen.height;
        float startY = myHeight * 0.75f;
        float endY = (myHeight * 0.98f) - startY;

        float myWidth = Screen.width;
        float startX = myWidth * 0.01f;

        float endX = (myWidth * 0.25f) - startX;
        float stretchY = Screen.width / 900.0f;
        //Debug.Log(stretchX+ ", "+ stretchY);
        GUI.DrawTexture(new Rect(startX, startY, endX, endY), texture, ScaleMode.StretchToFill, true, 10.0F);

        //GUI.DrawText(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
    }
}
