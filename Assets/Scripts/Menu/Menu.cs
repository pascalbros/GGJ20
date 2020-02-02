using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioClip gong;
    public AudioClip backgroundMusic;
    public GameObject beaver1;
    public GameObject beaver2;

    public GameObject sunglasses1;
    public GameObject sunglasses2;
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveTo(beaver1, iTween.Hash("x", -6.29, "time", 2.2, "easetype", iTween.EaseType.linear, "oncomplete", "OnGong", "oncompletetarget" , this.gameObject));
        iTween.MoveTo(beaver2, iTween.Hash("x", 6.29, "time", 2.2, "easetype", iTween.EaseType.linear));
    }

    public void OnGong() {
        this.GetComponent<AudioSource>().PlayOneShot(gong, 1.0f);
        this.GetComponent<AudioSource>().clip = this.backgroundMusic;
        this.GetComponent<AudioSource>().Play();

        iTween.MoveTo(sunglasses1, iTween.Hash("y", 1.34, "time", 2, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(sunglasses2, iTween.Hash("y", 1.34, "time", 2, "easetype", iTween.EaseType.linear));

    }

    // Update is called once per frame
    void Update () {
    }
}
