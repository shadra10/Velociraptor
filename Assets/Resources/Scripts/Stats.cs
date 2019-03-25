using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health, maxHealth, faction, abilities, mana, manaMax, cost, type;
    public float manaT, collT;
    public bool collOccur = false;
    // Start is called before the first frame update
    void Start()
    {
        collT = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CollisionChecker>() != null)
        {
            if (gameObject.GetComponent<CollisionChecker>().col)
            {
                Camera.main.GetComponent<PlayerScript>().eggs += gameObject.GetComponent<Stats>().cost;

                if (gameObject.GetComponent<Unit>() != null)
                {
                    Camera.main.GetComponent<PlayerScript>().units -= 1;
                }

                Destroy(gameObject);
            }
        }

        if (gameObject.GetComponent<CollisionChecker>() != null && Time.time - 0.5F >= collT)
        {
            Destroy(gameObject.GetComponent<CollisionChecker>());
        }

        if (health <= 0)
        {
            if (faction == 0 && gameObject.GetComponent<Unit>())
            {
                Camera.main.GetComponent<PlayerScript>().units -= 1;
            }

            Destroy(gameObject);
        }

        if (Time.time - 1.0F >= manaT && mana < manaMax)
        {
            manaT = Time.time;
            mana += 1;
        }
    }

    void OnMouseEnter()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<UI>().hoverUsed = this.gameObject;

        if (faction == 0)
        {
            Debug.Log("Your Piece - Health: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
            Debug.Log("Your Piece - Mana: " + this.GetComponent<Stats>().mana + "/" + this.GetComponent<Stats>().manaMax);
        }
        else if (faction == 1)
        {
            Debug.Log("Enemy Piece - Health: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
            Debug.Log("Enemy Piece - Mana: " + this.GetComponent<Stats>().mana + "/" + this.GetComponent<Stats>().manaMax);
        }
        else if (faction == 2)
        {
            Debug.Log("Resource - Value: " + this.GetComponent<Stats>().health + "/" + this.GetComponent<Stats>().maxHealth);
        }
    }

    void OnMouseExit()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<UI>().hoverUsed = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        collOccur = true;
    }

    void OnCollisionStay(Collision collision)
    {
        collOccur = true;
    }

    void OnCollisionExit(Collision collision)
    {
        collOccur = false;
    }
}
