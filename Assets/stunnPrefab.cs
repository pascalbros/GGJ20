using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunnPrefab : MonoBehaviour
{
    public Transform character;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector3(character.position.x, character.position.y+.5f, 0), Time.deltaTime*5);
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
