using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from: https://www.youtube.com/watch?v=SELTWo1XZ0c
// Video created by Kaupenjoe
// Uploaded August 14, 2021
public class FruitSpawner : MonoBehaviour
{
    public float initialSpawnInterval = 10f;
    public float minSpawnInterval = 2f;
    public float spawnRadius = 5f;

    [SerializeField] private GameObject grapePrefab;
    [SerializeField] private GameObject pearPrefab;
    [SerializeField] private GameObject cherryPrefab;
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject pomegranatePrefab;
    [SerializeField] private GameObject strawberryPrefab;
    [SerializeField] private GameObject goldenApplePrefab;
    [SerializeField] private GameObject peachPrefab;
    [SerializeField] private GameObject coconutPrefab;
    [SerializeField] private GameObject kiwiPrefab;
    [SerializeField] private GameObject lemonPrefab;
    [SerializeField] private GameObject greenApplePrefab;

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFruits());
    }

    private IEnumerator spawnFruits()
    {
        while (true)
        {
            SpawnFruit();

            elapsedTime += Time.deltaTime;
            float difficultyScale = Mathf.Clamp01(elapsedTime / 300f);
            float currentInterval = Mathf.Lerp(initialSpawnInterval, minSpawnInterval, difficultyScale);

            yield return new WaitForSeconds(currentInterval);
        }
    }
    private void SpawnFruit()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject fruitPrefab = GetRandomFruitPrefab();
        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

        ProximityTrigger2D proximity = spawnedFruit.GetComponent<ProximityTrigger2D>();

        if (proximity != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                proximity.player = player.transform;
            }
        }
    }

    private GameObject GetRandomFruitPrefab()
    {
        GameObject[] fruitPrefabs = new GameObject[]
        {
            grapePrefab,
            pearPrefab,
            cherryPrefab,
            bananaPrefab,
            applePrefab,
            pomegranatePrefab,
            strawberryPrefab,
            goldenApplePrefab,
            peachPrefab,
            coconutPrefab,
            kiwiPrefab,
            lemonPrefab,
            greenApplePrefab
        };

        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        return fruitPrefabs[randomIndex];
    }
}