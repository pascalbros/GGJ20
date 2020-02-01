using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamChoice : MonoBehaviour
{
    int dirX;
    public int playerId;
    public bool confirmed;
    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Mathf.RoundToInt(Input.GetAxis("Horizontal" + playerId));
        if (Input.GetButtonDown("XButton" + playerId)) confirmed = true;
        if (Input.GetButtonDown("YButton" + playerId)) confirmed = false;
    }
}
