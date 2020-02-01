using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamChoice : MonoBehaviour
{
    public int team;
    public int playerId;
    public bool confirmed;
    public Vector2 [] position= new Vector2[3];

    private float wait = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        team = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wait > 0.0f) {
            wait -= Time.deltaTime;
            CheckButtons();
            return;
        }
        if (Input.GetAxis("Horizontal" + playerId) != 0)
        {
            wait = 0.05f;
            team = Mathf.Clamp(team + Mathf.RoundToInt(Input.GetAxis("Horizontal" + playerId)), 0, 2);
            transform.position = position[team];
        }
        CheckButtons();
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
