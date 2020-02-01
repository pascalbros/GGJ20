using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    public bool owned;
    Transform owner;
    public int ownerID;
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
            if (dirY>=0) GetComponent<SpriteRenderer>().sortingOrder = -1;
            else GetComponent<SpriteRenderer>().sortingOrder = 1;
            if(dirX!=0||dirY!=0) transform.position = Vector2.Lerp(transform.position, new Vector2(owner.position.x, owner.position.y) + new Vector2(dirX/1.3f, dirY/2f) / (1 + Mathf.Abs(dirX) + Mathf.Abs(dirY)), Time.deltaTime / (Vector2.Distance(transform.position, owner.position) + .01f));
        }
    }

    public void throwObject()
    {
        owned = false;
        throwing = true;
        StartCoroutine(stopThrowing());
    }

    public void grabObject(Transform player, int team, int playerId)
    {
        if (GetComponent<DamObject>()) GetComponent<DamObject>().OnObjectCollected();
        if (owned && ownerID!=playerId)
        {
            //owner.GetComponent<Character>().ReleaseObject();
            stealing = true;
            StartCoroutine(stopStealing());
        }
        owned = true;
        owner = player;
        teamOwner = team;
        ownerID = playerId;
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log(ownerID);
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
        ownerID = 0;
        yield return new WaitForSeconds(1);
        throwing = false;
        teamOwner = 0;
    }
}
