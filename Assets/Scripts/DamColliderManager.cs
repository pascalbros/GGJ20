using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamColliderManager : MonoBehaviour
{
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
        if (col.gameObject.tag == "Player") {
            bool isTeamMate = true;
            if (isTeamMate) {
                OnTeamMateEntered(col.gameObject);
            } else {
                OnEnemyEntered(col.gameObject);
            }
        }
    }

    private void OnTeamMateEntered(GameObject teamMate) {
        bool hasObject = true;
        if (hasObject) { //if team mate has got an object
            scoreManager.AddLife(0.05f);
            //player release the object
        }
    }

    private void OnEnemyEntered(GameObject enemy) {
        bool hasObject = false;
        if (hasObject) { return; }
        scoreManager.AddLife(-0.05f);
        //player get an object
    }
}
