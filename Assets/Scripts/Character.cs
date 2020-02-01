﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	float dirX, dirY, rotateAngle;

	[SerializeField]
	float moveSpeed = 2f;

    [SerializeField]
    float throwForce = 120f;
    Animator anim;

    //[SerializeField]
    //Transform gun;
    //[SerializeField]
    //Rigidbody2D bullet;

    GameObject damObject;
    CharacterState state;
    CharacterAction action;
    public enum CharacterState
    {
        Swimming,
        Walking,
        Stunned
    }
    public enum CharacterAction
    {
        WaitingForAction,
        BringingObject,
        ActionCoolDown
    }
    
    // Use this for initialization
    void Start () {
		rotateAngle = 0f;
		anim = GetComponent<Animator> ();
		anim.speed = 1;
        state = CharacterState.Walking;
        action = CharacterAction.WaitingForAction;
	}

    // Update is called once per frame
    void Update()
    {
        Move();
        
        Debug.Log(state);

        Action();
    }

    void Move()
	{
        if (state == CharacterState.Walking)
        {
            anim.SetBool("OnGround", true);

        }
        else if (state == CharacterState.Swimming)
        {
            anim.SetBool("OnGround", false);
        }

		dirX = Mathf.RoundToInt(Input.GetAxis ("Horizontal"));
		dirY = Mathf.RoundToInt(Input.GetAxis ("Vertical"));

		transform.position = Vector2.Lerp(transform.position, new Vector2 (dirX  + transform.position.x, dirY  + transform.position.y), Time.deltaTime * moveSpeed);

        Rotate();
    }

    void Action() {
        if (action == CharacterAction.WaitingForAction)
        {
            if (Input.GetButtonDown("Fire1")) GrabObject();
            if (Input.GetButtonDown("Fire2")) SpecialAction();
        }
        if (action == CharacterAction.BringingObject)
        {
            damObject.transform.position = Vector2.Lerp(damObject.transform.position, new Vector2(transform.position.x, transform.position.y) + new Vector2(dirX, dirY)/(1+Mathf.Abs(dirX)+Mathf.Abs(dirY)), Time.deltaTime/(Vector2.Distance(transform.position, damObject.transform.position)+.1f));
            if (Input.GetButtonUp("Fire1")) ReleaseObject();
            if (Input.GetButtonDown("Fire2")) ThrowObject();
        }
    }

    void GrabObject ()
	{
        if (damObject != null)
        {
            Debug.Log("aaa");
            damObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            damObject.transform.parent = this.gameObject.transform;
            action = CharacterAction.BringingObject;
        }
		
	}

    void SpecialAction()
    {
        action = CharacterAction.ActionCoolDown;
        StartCoroutine(waitForAction(1f));
        //SpecialAbilityScript
    }

    IEnumerator waitForAction(float time)
    {
        yield return new WaitForSeconds(time);
        action = CharacterAction.WaitingForAction;
    }

    void ReleaseObject()
    {
        if (damObject != null)
        {
            damObject.transform.parent = null;
            damObject = null;
            action = CharacterAction.WaitingForAction;
        }
    }

    void ThrowObject()
    {
        if (damObject != null)
        {
            damObject.transform.parent = null;
            damObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dirX, dirY)*throwForce);
            damObject = null;
            action = CharacterAction.WaitingForAction;
        }
    }

    void Rotate()
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
            anim.speed = 1;
            anim.SetInteger("Direction", 0);
        }

        //gun.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 4)
        {
            state = CharacterState.Swimming;
        }
        else if (collision.gameObject.layer == 8)
        {
            Debug.Log("bbb");
            damObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            state = CharacterState.Walking;
        }
        else if (collision.gameObject.layer == 8 && action!=CharacterAction.BringingObject)
        {
            damObject = null;
        }
    }
}