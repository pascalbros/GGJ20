﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamChoiceData {
    public int team;
    public int playerId;
    public bool confirmed;
}

public class TeamChoice : MonoBehaviour
{
    public int team;
    public int playerId;
    public bool confirmed;
    public Vector2 [] position= new Vector2[3];

    public TeamChoiceData GetData() {
        TeamChoiceData data = new TeamChoiceData();
        data.team = this.team;
        data.playerId = this.playerId;
        data.confirmed = this.confirmed;
        return data;
    }
    private float wait = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        team = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait > 0.0f) {
            wait -= Time.deltaTime;
            CheckButtons();
            return;
        }
        CheckButtons();
        if (Input.GetAxis("Horizontal" + playerId) != 0 && !confirmed)
        {
            wait = 0.1f;
            team = Mathf.Clamp(team + Mathf.RoundToInt(Input.GetAxis("Horizontal" + playerId)), 0, 2);
            transform.position = position[team];
        }
    }

    private void CheckButtons() {
        if (Input.GetButtonDown("XButton" + playerId) && team != 1) {
            confirmed = true;
            UpdateConfirmed();
        }
        if (Input.GetButtonDown("YButton" + playerId) && team != 1) {
            confirmed = false;
            UpdateConfirmed();
        }
    }

    void UpdateConfirmed() {
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this.confirmed ? 0.5f : 1.0f);
    }
}
