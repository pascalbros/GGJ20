using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    NOT_STARTED,
    STARTED,
    ENDED
}

public class MainGameManager : MonoBehaviour
{
    public static TeamChoice[] controllers = new TeamChoice[4];
    public DamManager dam1;
    public DamManager dam2;
    public GameState status = GameState.NOT_STARTED;

    public static MainGameManager current;
    // Start is called before the first frame update
    void Start()
    {
        MainGameManager.current = this;
    }

    public static bool allApproved()
    {
        int team1Players = 0;
        int team2Players = 0;
        foreach(TeamChoice p in MainGameManager.controllers) {
            if (p.team == 0) {
                team1Players += 1;
            } else if (p.team == 2) {
                team2Players += 1;
            } else { 
                return false;
            }
            if (!p.confirmed) { return false; };
        }
        // if (team1Players != 2 || team2Players != 2) {
        //     return false;
        // }
        return true;
    }

    public void StartGame() {
        if (this.status != GameState.NOT_STARTED) { return; }
        this.status = GameState.STARTED;
        this.SetupDams();
        this.SetupPlayers();
        this.SetupObjectsSpawner();
    }

    void SetupDams() {
        if (this.dam1 == null || this.dam2 == null) {
            Debug.LogError("Unable to find dams");
            return;
        }
        dam1=FindObjectsOfType<DamManager>()[0];
        dam2 = FindObjectsOfType<DamManager>()[1];
        this.dam1.scoreManager.teamIndex = 0;
        this.dam1.scoreManager.status = DamState.STARTED;
        this.dam1.onDamDestroyed = this.OnDam1Destroyed;

        this.dam2.scoreManager.teamIndex = 1;
        this.dam2.scoreManager.status = DamState.STARTED;
        this.dam2.onDamDestroyed = this.OnDam2Destroyed;
    }

    void SetupPlayers() {

    }

    void SetupObjectsSpawner() {

    }

    void OnGameEnd() {
        this.status = GameState.ENDED;
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }

    void OnDam1Destroyed() {
        this.dam2.scoreManager.status = DamState.WINNER;
        Debug.Log("Dam 1 destroyed");
        EndManager.winnerTeamIndex = 2;
        this.OnGameEnd();
    }

    void OnDam2Destroyed() {
        this.dam1.scoreManager.status = DamState.WINNER;
        Debug.Log("Dam 2 destroyed");
        EndManager.winnerTeamIndex = 1;
        this.OnGameEnd();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
