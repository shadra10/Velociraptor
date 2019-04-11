using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSpellCheck(int foeList, int allyList, int allyHealth)
    {
        if (allyList > 0 && allyHealth > 0 && foeList > 0){
            if (GetComponent<Stats>().mana >= 80)
            {
                Debug.Log("Cast Bloodburst");
                GetComponent<Bloodburst>().CastSpell();
            }
        } else if (foeList >= 3)
        {
            if (GetComponent<Stats>().mana >= 50)
            {
                Debug.Log("Cast Soulburst");
                GetComponent<Soulburst>().CastSpell();
            }
        }
    }
}
