using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamColliderManager : MonoBehaviour
{
    public AudioClip onDamLifeUp;
    public DamScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.gameObject.GetComponent<Character>();
        if (character != null) {
            bool isTeamMate = character.team == scoreManager.teamIndex;
            if (isTeamMate) {
                OnTeamMateEntered(character);
            } else {
                //OnEnemyEntered(col.gameObject);
            }
        }
    }

    // void OnCollisionEnter2D(Collision2D col) {
    //     Debug.Log(col.gameObject);
    //     if (col.gameObject.GetComponent<Throwable>() != null) {
    //         Throwable throwable = col.gameObject.GetComponent<Throwable>();
    //         if (throwable.teamOwner != 0) { return; }
    //         Destroy(col.gameObject);
    //         scoreManager.AddLife(0.05f);
    //     }
    // }

    private void OnTeamMateEntered(Character teamMate) {
        bool hasObject = teamMate.hasObject();
        if (hasObject) { //if team mate has got an object
            scoreManager.AddLife(0.05f);
            teamMate.DestroyObject();
            this.GetComponent<AudioSource>().PlayOneShot(this.onDamLifeUp, 1.0f);
        }
    }

    private void OnEnemyEntered(GameObject enemy) {
        bool hasObject = false;
        if (hasObject) { return; }
        scoreManager.AddLife(-0.05f);
        //player get an object
    }
}
