using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClass : MonoBehaviour
{
    Character[] positions = new Character[3];
    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        foreach (Character c in FindObjectsOfType<Character>())
        {
            if(c!=GetComponent<Character>() && c.team!= GetComponent<Character>().team) positions[counter] = c;
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
            if (c != GetComponent<Character>() && Vector3.Distance(c.transform.position, transform.position) < 1) c.stunnPlayer();
        }
    }
}
