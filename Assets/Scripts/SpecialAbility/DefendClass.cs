using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendClass : MonoBehaviour
{
    Character[] positions = new Character[2];
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();

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
    public void Defend()
    {
        
        foreach (Character c in positions)
        {

            Debug.Log("special");
            //anim.SetTrigger("Special");
            Debug.Log("uaaaaaaaaaaaaa");

            if ((c.transform.position - transform.position).magnitude < 1.4f)
            {
                c.GetComponent<Rigidbody2D>().AddForce((c.transform.position - transform.position).normalized * 250);
            }

        }
    }
}
