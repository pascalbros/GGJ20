using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DamState
{
    NOT_STARTED,
    STARTED,
    DEAD,
    WINNER
}
public class DamScoreManager : MonoBehaviour
{
    public Action OnDamDestroyed;
    public DamState status = DamState.NOT_STARTED;
    public int teamIndex = 1;
    public float lifeLostPerSecond = 0.01f;
    float life = 0.5f;
    public string percentage {
        get { 
            int intValue = (int)(life*100.0f);
            return intValue.ToString()+"%"; 
        }
    }

    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    private float nextLifeCheckTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.status = DamState.STARTED;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status == DamState.STARTED) {
            this.life -= this.lifeLostPerSecond * Time.deltaTime;
            this.life = Mathf.Clamp(this.life, 0.0f, 1.0f);
            if (this.life == 0.0f) {
                OnDestroyed();
            }
        }

        nextLifeCheckTime += Time.deltaTime;
        if (nextLifeCheckTime >= 1.0f ) {
            nextLifeCheckTime = 0.0f;
            this.CheckTexture();
            Debug.Log(percentage);

        }
    }

    //Value from 0.0 to 1.0
    public void AddLife(float value) {
        this.life = Mathf.Clamp(life+value, 0.0f, 1.0f);
    }

    public void SetLife(float value) {
        this.life = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    private void CheckTexture() {
        float value = 1.0f/this.sprites.Length;
        int index = (this.sprites.Length - 1) - (int)(this.life / value);
        if (this.spriteRenderer.sprite == this.sprites[index]) { return; }
        this.spriteRenderer.sprite = this.sprites[index];
    }

    void OnDestroyed() {
        this.status = DamState.DEAD;
        if (this.OnDamDestroyed != null) {
            this.OnDamDestroyed();
        }
    }

    void OnWinner() {
        this.status = DamState.WINNER;
        Debug.Log("Dam "+this.teamIndex+" won!");
    }
}
