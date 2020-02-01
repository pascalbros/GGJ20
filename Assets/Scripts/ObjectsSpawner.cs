using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public float spawnTime = 5.0f;
    public float spawnTimeRange = 1.0f;

    public float spawnWidthRange = 1.0f;

    public GameObject[] collectables;

    private float currentSpawnTime = 0.0f;
    private float nextSpawnTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.nextSpawnTime = this.spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= this.nextSpawnTime) {
            this.currentSpawnTime = 0.0f;
            this.nextSpawnTime = spawnTime + Random.Range(-spawnTimeRange, spawnTimeRange);
            this.JustSpawnStuff();
        }
    }

    void JustSpawnStuff() {
        Debug.Log("Spawned!");
        float x = Random.Range(-this.spawnWidthRange, this.spawnWidthRange);
        GameObject collectable = this.collectables[Random.Range(0,this.collectables.Length)];
        Instantiate(collectable, new Vector3(x, this.transform.position.y, 0), Quaternion.identity);
    }
}
