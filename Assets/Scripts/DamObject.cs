using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamObject : MonoBehaviour
{
    public AudioClip onCollectedClip;
    public float speed = 1.0f;
    public float speedRange = 0.5f;
    public float minY = -3.5f;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = speed + Random.Range(-this.speedRange, this.speedRange);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transform.position + new Vector3(0, -this.speed * Time.deltaTime, 0);
        if (this.transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    public void OnObjectCollected() {
        this.GetComponent<AudioSource>().PlayOneShot(onCollectedClip, 1.0f);
        Destroy(this);
    }
}
