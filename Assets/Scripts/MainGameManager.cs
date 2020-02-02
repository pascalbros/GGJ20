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
    public GameObject Defender, Collector, Fighter;
    public static TeamChoice[] controllers = new TeamChoice[42];
    public DamManager dam1;
    public DamManager dam2;
    public Transform pool1;
    public Transform pool2;
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
        if (team1Players != 2 || team2Players != 2) {
            return false;
        }
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
        this.dam1.scoreManager.teamIndex = 1;
        this.dam1.scoreManager.status = DamState.STARTED;
        this.dam1.onDamDestroyed = this.OnDam1Destroyed;

        this.dam2.scoreManager.teamIndex = 2;
        this.dam2.scoreManager.status = DamState.STARTED;
        this.dam2.onDamDestroyed = this.OnDam2Destroyed;
    }

    void SetupPlayers() {
        Vector3 position = new Vector3(4f, 1.83f, 0f);
        List<TeamChoiceData> team1 = new List<TeamChoiceData>();
        List<TeamChoiceData> team2 = new List<TeamChoiceData>();

        foreach(TeamChoice p in MainGameManager.controllers) {
            if (p.team == 0) {
                team1.Add(p.GetData());
            } else if (p.team == 2) {
                team2.Add(p.GetData());
            }
        } 
        List<TeamChoiceData> teams = new List<TeamChoiceData>();
        teams.AddRange(team2);
        teams.AddRange(team1);
        teams.Add(SwappingTeamMate(2));
        teams.Add(SwappingTeamMate(0));
        Vector3[] positions = new Vector3[] {
            position,
            new Vector3(position.x, -position.y, position.z),
            new Vector3(-position.x, position.y, position.z),
            new Vector3(-position.x, -position.y, position.z),
            new Vector3(4.88f, -0.18f, position.z), //swap2
            new Vector3(-4.88f, -0.18f, position.z) //swap1
        };
        for (int i = 0; i < teams.Count; i++) {
            TeamChoiceData teamItem = teams[i];
            GameObject player=null;
            if (i <= 3) {
                if (i%2==1) player = Instantiate(this.Fighter,positions[i], Quaternion.identity);
                else player = Instantiate(this.Defender, positions[i], Quaternion.identity);
            } else {
                player = Instantiate(this.Collector, positions[i], Quaternion.identity);
            }

            player.GetComponent<SpriteRenderer>().color = teamItem.team == 0 ? Color.red : Color.cyan;
            Character character = player.GetComponent<Character>();
            character.team = teamItem.team == 0 ? 1 : 2;
            character.pool = character.team == 1 ? this.pool1 : this.pool2;
            character.playerId = teamItem.playerId;
        }
    }

    private TeamChoiceData SwappingTeamMate(int teamId) {
        TeamChoiceData teamMate = new TeamChoiceData();
        teamMate.playerId = 0;
        teamMate.team = teamId;
        teamMate.confirmed = true;
        return teamMate;
    }

    void SetupObjectsSpawner() {

    }

    void OnGameEnd() {
        this.status = GameState.ENDED;
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }

    void OnDam1Destroyed() {
        this.dam2.scoreManager.status = DamState.WINNER;
        EndManager.winnerTeamIndex = 2;
        this.OnGameEnd();
    }

    void OnDam2Destroyed() {
        this.dam1.scoreManager.status = DamState.WINNER;
        EndManager.winnerTeamIndex = 1;
        this.OnGameEnd();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}