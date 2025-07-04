using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;
    private GameObject greenSlimePrefab;
    public float spawnBuffer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        greenSlimePrefab = enemyArray[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerSpawn()
    {
        StartCoroutine(SpawnAfterBuffer());
    }

    private IEnumerator SpawnAfterBuffer()
    {
        yield return new WaitForSeconds(spawnBuffer);
        GameObject enemy = Instantiate(greenSlimePrefab, transform.position, Quaternion.identity);
        GameManager.Instance.RegisterEnemy();
    }
}