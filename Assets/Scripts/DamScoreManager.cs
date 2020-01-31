using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamStatus
{
    NOT_STARTED,
    NORMAL,
    DEAD
}
public class DamScoreManager : MonoBehaviour
{
    public DamStatus status = DamStatus.NOT_STARTED;
    public int teamIndex = 0;
    public float lifeLostPerSecond = 0.01f;
    float life = 0.5f;
    public string percentage {
        get { 
            int intValue = (int)(life*100.0f);
            return intValue.ToString()+"%"; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.status = DamStatus.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status == DamStatus.NORMAL) {
            this.life -= this.lifeLostPerSecond * Time.deltaTime;
            this.life = Mathf.Clamp(this.life, 0.0f, 1.0f);
            Debug.Log(percentage);
            if (this.life == 0.0f) {
                OnDestroyed();
            }
        }
    }

    //Value from 0.0 to 1.0
    public void AddLife(float value) {
        this.life = Mathf.Clamp(life+value, 0.0f, 1.0f);
    }

    public void SetLife(float value) {
        this.life = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    void OnDestroyed() {
        this.status = DamStatus.DEAD;
        Debug.Log("Destroyed!");
    }
}
