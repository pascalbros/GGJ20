
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashClass : MonoBehaviour
{
    [SerializeField]
    float dashForce = 3f;

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

        RaycastHit2D collision = Physics2D.Raycast(transform.position, new Vector2(dirX, dirY), dashForce, 9);
        Debug.DrawRay(transform.position, new Vector2(transform.position.x*dirX*dashForce, transform.position.y * dirY * dashForce), Color.red, 300f);


        if(collision.collider == null)
        {
            transform.position = new Vector2(transform.position.x + dirX * dashForce, transform.position.y + dirY * dashForce);
        
        }
        else
        {
            Debug.Log("MURO");
        }

    }
}
  