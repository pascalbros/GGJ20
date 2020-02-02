using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimmingpool : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int teamId;
    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPool(GameObject beaver)
    {
        if(teamId == beaver.GetComponent<Character>().team)
        {
            

        }

        



    }
}
