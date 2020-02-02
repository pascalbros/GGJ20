using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	float dirX, dirY, rotateAngle;
    public int team=1;
    public int playerId = 1;
    bool objectHolded;
    bool switchChar = true;
    public float speedMalus = 1.4f;
    [SerializeField]
	float moveSpeed = 2f;

    [SerializeField]
    GameObject stunPrefab;

    [SerializeField]
    float throwForce = 120f;
    Animator anim;
    
    //[SerializeField]
    //Transform gun;
    //[SerializeField]
    //Rigidbody2D bullet;

    Throwable damObject;
    public CharacterState state;
    public CharacterAction action;
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
        objectHolded = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerId > 0)
        {
            Move();
            Action();
        }
        
    }

    void Move()
    {
        if (state != CharacterState.Stunned)
        {
            if (state == CharacterState.Walking)
            {
                anim.SetBool("OnGround", true);

            }
            else if (state == CharacterState.Swimming)
            {
                anim.SetBool("OnGround", false);
            }


            dirX = Mathf.RoundToInt(Input.GetAxis("Horizontal"+playerId));
            dirY = Mathf.RoundToInt(Input.GetAxis("Vertical"+playerId));

            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y) + new Vector2(dirX , dirY ).normalized, Time.deltaTime * moveSpeed);

            Rotate();
        }
    }
    

    void Action() {
        if (action == CharacterAction.WaitingForAction)
        {
            if (Input.GetButtonDown("XButton" + playerId)) SpecialAction();
        }
        if (action == CharacterAction.BringingObject)
        {
            if (Input.GetButtonUp("XButton" + playerId)) ThrowObject();
        }
    }

    void GrabObject ()
	{
        if (damObject != null && damObject.canBeGrabbed()&&objectHolded==false)
        {
            moveSpeed /= speedMalus;
            damObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            damObject.transform.parent = this.gameObject.transform;
            action = CharacterAction.BringingObject;
            objectHolded = true;
            damObject.grabObject(transform, team, playerId);
        }
		
	}

    void SpecialAction()
    {
        action = CharacterAction.ActionCoolDown;
        StartCoroutine(waitForAction(5f, CharacterAction.WaitingForAction));
        //SpecialAbilityScript
        if (gameObject.tag.Equals("Collector"))
        {
            DashClass dash = gameObject.GetComponent<DashClass>();
            dash.Dash(dirX, dirY);
            Debug.Log("Collector");
        }
        else if (gameObject.tag.Equals("Defender"))
        {
            DefendClass defend = gameObject.GetComponent<DefendClass>();
            defend.Defend();
            Debug.Log("Defender");
        }
        else if (gameObject.tag.Equals("Fighter"))
        {
            FightClass fight = gameObject.GetComponent<FightClass>();
            fight.Fight();
            Debug.Log("Fighter");
        }
    }

    IEnumerator waitForAction(float time, CharacterAction newAction)
    {
        yield return new WaitForSeconds(time);
        action = newAction;
        
    }
    IEnumerator waitForState(float time, CharacterState newState)
    {
        Animator stunAnimator = stunPrefab.GetComponent<Animator>();
        
        yield return new WaitForSeconds(time);
        
        if (state == CharacterState.Stunned) {
            stunAnimator.SetBool("isStunned", false);
        }
        state = newState;

        
        //Destroy(stunPrefab);

    }
    public void ReleaseObject()
    {
        if (damObject != null)
        {
            damObject.transform.parent = null;
            damObject.releaseObject();
            damObject = null;
            action = CharacterAction.WaitingForAction;
            objectHolded = false;
            moveSpeed *= speedMalus;
        }
    }

    public void DestroyObject()
    {
        if (damObject != null)
        {
            damObject.transform.parent = null;
            Destroy(damObject.gameObject);
            damObject = null;
            action = CharacterAction.WaitingForAction;
            objectHolded = false;
            moveSpeed *= speedMalus;
        }
    }

    public bool hasObject() {
        return this.damObject != null;
    }
    
    
    void ThrowObject()
    {
        if (damObject != null && damObject.ownerID == playerId)
        {
            damObject.transform.parent = null;
            damObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dirX, dirY) * throwForce);
            damObject.throwObject();
            damObject = null;
            action = CharacterAction.WaitingForAction;
            objectHolded = false;
            moveSpeed *= speedMalus;
        }
        else
            damObject = null;
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
            if (collision.GetComponent<Throwable>().throwing&& collision.GetComponent<Throwable>().teamOwner != team)
            {

                stunnPlayer();
                
            }
            else if (!collision.GetComponent<Throwable>().stealing)
            {
                damObject = collision.GetComponent<Throwable>();
                GrabObject();
            }
        }
    }
    public void stunnPlayer()
    {
        CharacterState newState = state;
        state = CharacterState.Stunned;
        stunnPrefab sp = null;
        
        sp = Instantiate(stunPrefab, new Vector2(transform.position.x, transform.position.y + 0.1f), Quaternion.identity).GetComponent<stunnPrefab>();
        sp.character = this.transform;
        Animator stunAnimator = sp.GetComponent<Animator>();

        //stunPrefab.transform.parent = transform;
        stunAnimator.SetBool("isStunned", true);
        
        StartCoroutine(waitForState(3, newState));

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            state = CharacterState.Walking;
        }
        else if (collision.gameObject.layer == 8 && action!=CharacterAction.BringingObject)
        {
            if (collision.GetComponent<Throwable>().throwing) collision.GetComponent<BoxCollider2D>().enabled = true;
            damObject = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Character>() != null)
        {
            if(switchChar && collision.gameObject.GetComponent<Character>().playerId == 0 && collision.gameObject.GetComponent<Character>().team == team)
            {
               // Debug.Log();
                StartCoroutine(ChangeChar(collision.gameObject));
            }
           
        }
    }

    IEnumerator ChangeChar(GameObject nextBeaver)
    {
        switchChar = false;
        nextBeaver.GetComponent<Character>().switchChar = false;

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        nextBeaver.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        nextBeaver.GetComponent<Character>().playerId = playerId;
        nextBeaver.GetComponent<Character>().action = CharacterAction.WaitingForAction;
        this.playerId = 0;
        yield return new WaitForSeconds(1);
        switchChar = true;
        nextBeaver.GetComponent<Character>().switchChar = true;

    }
}
