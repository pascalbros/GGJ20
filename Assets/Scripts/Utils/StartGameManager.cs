using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip matchBackground;
    public AudioClip beep;
    public AudioClip start;
    public TextMesh text;
    private int remainingSeconds = 4;
    private float elapsedTime = 4.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.elapsedTime -= Time.deltaTime;
        if (this.elapsedTime <= 0.0f) {
            StartGame();
            return;
        } else if (this.elapsedTime <= 1.0f) {
            if (remainingSeconds != 0) {
                remainingSeconds = 0;
                PlayStart();
            }
            this.text.text = "Battle!";
        } else if (this.elapsedTime <= 2.0f) {
            if (remainingSeconds != 1) {
                remainingSeconds = 1;
                PlayBeep();
            }
            this.text.text = "1";
        } else if (this.elapsedTime <= 3.0f) {
            if (remainingSeconds != 2) {
                remainingSeconds = 2;
                PlayBeep();
            }
            this.text.text = "2";
        } else {
            if (remainingSeconds != 3) {
                remainingSeconds = 3;
                PlayBeep();
            }
            this.text.text = "3";
        }
    }

    void StartGame() {
        MainGameManager.current.StartGame();
        MainGameManager.current.gameObject.GetComponent<AudioSource>().clip = this.matchBackground;
        MainGameManager.current.gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject);

    }

    void PlayBeep() {
        this.GetComponent<AudioSource>().PlayOneShot(beep, 1.0f);
    }

    void PlayStart() {
        this.GetComponent<AudioSource>().PlayOneShot(start, 1.0f);
    }
}
