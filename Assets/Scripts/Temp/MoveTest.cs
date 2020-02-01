using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed = 1.0f;
    Rigidbody2D rb;
 
    void Start () {
        rb = GetComponent <Rigidbody2D> ();
    }
 
    void FixedUpdate () {
        float x = Input.GetAxis ("Horizontal");
        float y = Input.GetAxis ("Vertical");
        rb.velocity = new Vector3 (x * speed, y * speed, 0);
    }
}
