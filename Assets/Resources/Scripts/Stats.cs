using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health, maxHealth, faction, abilities, mana, manaMax, cost, type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseEnter()
    {
        if (faction == 0)
        {
            Debug.Log("Your Piece - Health: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
        }
        else if (faction == 1)
        {
            Debug.Log("Enemy Piece - Health: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
        }
        else if (faction == 2)
        {
            Debug.Log("Resource - Value: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
        }
    }

}
