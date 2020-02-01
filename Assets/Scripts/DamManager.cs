using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamManager : MonoBehaviour
{
    public Action onDamDestroyed;
    public DamScoreManager scoreManager;
    public DamColliderManager colliderManager;
    // Start is called before the first frame update
    void Start()
    {
        this.scoreManager.OnDamDestroyed = OnDamDestroyedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDamDestroyedCallback() {
        if (this.onDamDestroyed == null) { return; }
        this.onDamDestroyed();
    }
}
