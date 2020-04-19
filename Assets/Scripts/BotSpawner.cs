using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public int maxBots = 100;
    private int currentBots = 0;
    public GameObject botPreFab;

    public float radiusForAnchorSpawns = 4.0f;
    public float spawnDelay = .5f;
    public int botLevel = 1;


    private Vector3 center = Vector3.zero;
    private Vector3 anchoirPoint;
    private float killDistance;
    private float radiusForSpawningEnemies;

    void Start()
    {
        StartCoroutine(spawnEnemies());

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        radiusForSpawningEnemies = halfWidth + 5f;
        killDistance = radiusForSpawningEnemies + .01f;
    }

    void Update()
    {
        currentBots = GameObject.FindGameObjectsWithTag("bot").Length;
    }

    IEnumerator spawnEnemies()
    {
        while (true)
        {
            if (currentBots < maxBots) this.SpawnBot();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBot()
    {
        Vector3 botSpawnPoint = RandomPointOnCircleDiameter(center, radiusForSpawningEnemies);
        this.getAnchorPoint(center, radiusForAnchorSpawns);
        GameObject bot = Instantiate(botPreFab, botSpawnPoint, Quaternion.identity);
        bot.GetComponent<Bot>().anchoirPoint = anchoirPoint;
        bot.GetComponent<Bot>().killDistance = killDistance;
        bot.GetComponent<Bot>().botLevel = botLevel;
    }

    void getAnchorPoint(Vector3 center, float radius)
    {
       anchoirPoint = this.RandomPointOnCircleDiameter(center, radius);
    }

    Vector3 RandomPointOnCircleDiameter(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }


}
