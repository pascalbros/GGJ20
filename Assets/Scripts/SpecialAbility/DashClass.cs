
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashClass : MonoBehaviour
{
    [SerializeField]
    public float dashForce = 15f;

    Rigidbody2D rb; 

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
 
    }

    public void Dash(float dirX, float dirY)
    {

        GetComponent<Rigidbody2D>().AddForce(new Vector2(dirX, dirY)*dashForce, ForceMode2D.Impulse);
        

    }
}
  