using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClass : MonoBehaviour
{


    Character[] positions = new Character[3];
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        int counter = 0;

        foreach (Character c in FindObjectsOfType<Character>())
        {
            if (c.team != GetComponent<Character>().team)
            {
                positions[counter] = c;
                counter++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fight()
    {
        
        foreach (Character c in positions)
        {

            Debug.Log("special");
            anim.SetTrigger("Special");
            
            if (Vector3.Distance(c.transform.position, transform.position) < .7f) c.stunnPlayer();

        }
    }
}
