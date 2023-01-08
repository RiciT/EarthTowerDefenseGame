using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 2;
    public float spawnRateRandomizer = 1;
    public GameObject enemyPrefab;

    bool spawning = true;

    void Update()
    {
        while (spawning)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        spawning = false;
        Instantiate(enemyPrefab, GetRandomPointInCollider(GetComponent<Collider2D>()), 
            Quaternion.identity, transform.parent);
        float modifier = Random.Range(-spawnRateRandomizer, spawnRateRandomizer);
        yield return new WaitForSeconds(spawnRate + modifier);
        spawning = true;
    }

    Vector3 GetRandomPointInCollider(Collider2D collider)
    {
        var point = new Vector2(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y));

        if (point != collider.ClosestPoint(point))
        {
            Debug.Log("Out of the collider! Looking for other point...");
            point = GetRandomPointInCollider(collider);
        }

        return point;
    }
}
