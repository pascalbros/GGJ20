using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    public bool owned;
    Transform owner;
    int ownerID;
    int dirX, dirY;

    public int teamOwner = 0;
    public bool stealing;
    public bool throwing;
    // Start is called before the first frame update
    void Start()
    {
        teamOwner = 0;
        owned = false;
        stealing = false;
        throwing = false;
    }

    // Update is called once per frame

    void Update()
    {
        if (owned)
        {
            dirX = Mathf.RoundToInt(Input.GetAxis("Horizontal"+ ownerID));
            dirY = Mathf.RoundToInt(Input.GetAxis("Vertical"+ ownerID));
            Debug.Log(dirY);
            if (dirY>=0) GetComponent<SpriteRenderer>().sortingOrder = -1;
            else GetComponent<SpriteRenderer>().sortingOrder = 1;
            if(dirX!=0||dirY!=0) transform.position = Vector2.Lerp(transform.position, new Vector2(owner.position.x, owner.position.y) + new Vector2(dirX/1.3f, dirY/2f) / (1 + Mathf.Abs(dirX) + Mathf.Abs(dirY)), Time.deltaTime / (Vector2.Distance(transform.position, owner.position) + .1f));
        }
    }

    public void throwObject()
    {
        owned = false;
        throwing = true;
        StartCoroutine(stopThrowing());
    }

    public void grabObject(Transform player, int team)
    {
        if (owned)
        {
            stealing = true;
            StartCoroutine(stopStealing());
        }
        owned = true;
        owner = player;
        teamOwner = team;
        ownerID = owner.GetComponent<Character>().playerId;
    }
    public void releaseObject()
    {
        owned = false;
        teamOwner = 0;
        ownerID = 0;
    }
    public bool canBeGrabbed()
    {
        return !stealing&&!throwing;
    }
    IEnumerator stopStealing()
    {
        yield return new WaitForSeconds(1);
        stealing = false;
    }
    IEnumerator stopThrowing()
    {
        yield return new WaitForSeconds(1);
        throwing = false;
        teamOwner = 0;
        ownerID = 0;
    }
}
