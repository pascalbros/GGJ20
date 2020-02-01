using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	float dirX, dirY, rotateAngle;

	[SerializeField]
	float moveSpeed = 2f;

    Animator anim;

	//[SerializeField]
	//Transform gun;
	//[SerializeField]
	//Rigidbody2D bullet;

    CharacterState state;

    public enum CharacterState
    {
        Swimming,
        Walking
        
    }

    
    // Use this for initialization
    void Start () {
		rotateAngle = 0f;
		//anim = GetComponent<Animator> ();
		//anim.speed = 1;
        state = CharacterState.Walking;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        //if (state == CharacterState.Swimming) RotateSwim();
        //else if(state == CharacterState.Walking) RotateWalk();
        Debug.Log(state);

    }

    void Move()
	{
		dirX = Mathf.RoundToInt(Input.GetAxis ("Horizontal"));
		dirY = Mathf.RoundToInt(Input.GetAxis ("Vertical"));

		transform.position = new Vector2 (dirX * moveSpeed * Time.deltaTime + transform.position.x,
			dirY * moveSpeed * Time.deltaTime + transform.position.y);
	}

    
	void Fire ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			
		}
		
	}

    void RotateSwim()
    {
        if (dirX == 0 && dirY == 1)
        {
            rotateAngle = 0;
            anim.speed = 1;
            anim.SetInteger("Direction", 1);
        }

        if (dirX == 1 && dirY == 1)
        {
            rotateAngle = -45f;
            anim.speed = 1;
            anim.SetInteger("Direction", 2);
        }

        if (dirX == 1 && dirY == 0)
        {
            rotateAngle = -90f;
            anim.speed = 1;
            anim.SetInteger("Direction", 3);
        }

        if (dirX == 1 && dirY == -1)
        {
            rotateAngle = -135f;
            anim.speed = 1;
            anim.SetInteger("Direction", 4);
        }

        if (dirX == 0 && dirY == -1)
        {
            rotateAngle = -180f;
            anim.speed = 1;
            anim.SetInteger("Direction", 5);
        }

        if (dirX == -1 && dirY == -1)
        {
            rotateAngle = -225f;
            anim.speed = 1;
            anim.SetInteger("Direction", 6);
        }

        if (dirX == -1 && dirY == 0)
        {
            rotateAngle = -270f;
            anim.speed = 1;
            anim.SetInteger("Direction", 7);
        }

        if (dirX == -1 && dirY == 1)
        {
            rotateAngle = -315f;
            anim.speed = 1;
            anim.SetInteger("Direction", 8);
        }

        if (dirX == 0 && dirY == 0)
        {
            anim.speed = 0;
        }

        //gun.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

    }void RotateWalk()
	{
		if (dirX == 0 && dirY == 1) {
			rotateAngle = 0;
			anim.speed = 1;
			anim.SetInteger ("Direction", 1);
		}

		if (dirX == 1 && dirY == 1) {
			rotateAngle = -45f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 2);
		}

		if (dirX == 1 && dirY == 0) {
			rotateAngle = -90f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 3);
		}

		if (dirX == 1 && dirY == -1) {
			rotateAngle = -135f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 4);
		}

		if (dirX == 0 && dirY == -1) {
			rotateAngle = -180f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 5);
		}

		if (dirX == -1 && dirY == -1) {
			rotateAngle = -225f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 6);
		}

		if (dirX == -1 && dirY == 0) {
			rotateAngle = -270f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 7);
		}

		if (dirX == -1 && dirY == 1) {
			rotateAngle = -315f;
			anim.speed = 1;
			anim.SetInteger ("Direction", 8);
		}

		if (dirX == 0 && dirY == 0) {
			anim.speed = 0;
		}

		//gun.rotation = Quaternion.Euler (0f, 0f, rotateAngle);

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 4)
        {
            state = CharacterState.Swimming;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            state = CharacterState.Walking;
        }
    }
}
