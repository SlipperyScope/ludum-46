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
    private int[] enemiesChance = {100,0,0};


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
        //TODO: get current score and use it in the calculation for bot level
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
        this.SetEnemyChance();
        Vector3 botSpawnPoint = RandomPointOnCircleDiameter(center, radiusForSpawningEnemies);
        this.getAnchorPoint(center, radiusForAnchorSpawns);
        GameObject bot = Instantiate(botPreFab, botSpawnPoint, Quaternion.identity);
        bot.GetComponent<Bot>().anchoirPoint = anchoirPoint;
        bot.GetComponent<Bot>().killDistance = killDistance;
        bot.GetComponent<Bot>().botLevel = this.PickBotLevel();
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

    int PickBotLevel()
    {
        Debug.Log($"first, {enemiesChance[0]}, second {enemiesChance[1]}, third {enemiesChance[2]}");
        int random = Random.Range(0, 100); // draw a number between 0 and 99
        int lowLim;    // lowLim and hiLim are automatically set for each enemy
        int hiLim = 0;
        for (int i = 0; i < enemiesChance.Length; i++)
        {
            lowLim = hiLim; // set low limit...
            hiLim += enemiesChance[i]; // and high limit
            if (random >= lowLim && random < hiLim)
            { // instantiate it!
                Debug.Log($"Bot Level to spawn {i+1}");
                return i + 1;
            }
        }
        return Random.Range(1,enemiesChance.Length+1);
    }

    private void SetEnemyChance()
    {
        int Score = GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().getScore();
        if (this.IsBetween(Score, 0, 500)) enemiesChance= new int[] { 100, 0, 0 };
        if (this.IsBetween(Score, 500, 1000)) enemiesChance = new int[] { 80, 20, 5 };
        if (this.IsBetween(Score, 1000, 1500)) enemiesChance = new int[] { 50, 50, 15 };
        if (this.IsBetween(Score, 1500, 2000)) enemiesChance = new int[] { 40, 60, 30 };
        if (this.IsBetween(Score, 2000, 2500)) enemiesChance = new int[] { 20, 50, 50 };
        if (this.IsBetween(Score, 2000, 3000)) enemiesChance = new int[] { 10, 50, 60 };

    } 

    private bool IsBetween(int numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck < top);
    }


}
