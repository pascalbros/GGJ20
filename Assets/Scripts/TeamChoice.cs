using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamChoice : MonoBehaviour
{
    public int team;
    bool wait;
    public int playerId;
    public bool confirmed;
    public Vector2 [] position= new Vector2[3];
    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        team = 1;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait && Input.GetAxis("Horizontal" + playerId) != 0)
        {
            team = Mathf.Clamp(team + Mathf.RoundToInt(Input.GetAxis("Horizontal" + playerId)), 0, 2);
            StartCoroutine(waitForNextSwap());
        }
        transform.position = position[team];
        if (Input.GetButtonDown("XButton" + playerId)) confirmed = true;
        if (Input.GetButtonDown("YButton" + playerId)) confirmed = false;
    }
    IEnumerator waitForNextSwap()
    {
        wait = false;
        yield return new WaitForSeconds(.1f);
        wait = true;
    }
}
