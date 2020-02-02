using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBlink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().enabled = Time.fixedTime%1.0f<.5;
        if(Input.anyKey) {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
