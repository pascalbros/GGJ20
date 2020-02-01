using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamChoice : MonoBehaviour
{
    int counter;
    bool wait;
    public int playerId;
    public bool confirmed;
    public Vector2 [] position= new Vector2[3];
    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        counter = 1;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait && Input.GetAxis("Horizontal" + playerId) != 0)
        {
            counter = Mathf.Clamp(counter + Mathf.RoundToInt(Input.GetAxis("Horizontal" + playerId)), 0, 2);
            StartCoroutine(waitForNextSwap());
        }
        transform.position = position[counter];
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
