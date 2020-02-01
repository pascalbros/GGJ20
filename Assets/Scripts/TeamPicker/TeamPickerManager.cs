using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamPickerManager : MonoBehaviour
{
    public TeamChoice[] controllers;
    // Start is called before the first frame update
    void Start()
    {
        MainGameManager.controllers = this.controllers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (MainGameManager.allApproved()) {
            SceneManager.LoadScene("MainScene");
        }
    }
}
