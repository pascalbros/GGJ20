using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int winnerTeamIndex = 1;
    public TextMesh winnerText;
    void Start()
    {
        this.winnerText.text = "The winner is team " + winnerTeamIndex + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
