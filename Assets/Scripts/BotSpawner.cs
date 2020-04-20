using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public int maxBots = 1;
    private int currentBots = 0;
    public GameObject[] botPreFabs;
    
    public float spawnDelay = .5f;
    private int[] enemiesChance = {100,0,0};

    public int ScoreNeededToKillMediumBot = 2000;
    public int[] botMass = { 30, 60, 100 };
    public float[] botSpeed = { 1, 2, 3 };
    public int[] BotPointValues = { 50, 150, 200 };

    //{FromLowPoint,ToHighPointVal,smallChancetoSpawn,MedChanceTospawn,HighChancetoSpawn}
    //[# of diffrent dificulty settings, leave as 5]
    private int[,] SpawnProb = new int[6, 5]
    {
        {0, 500, 100, 0, 0 },
        {500, 1000, 80, 20, 5 },
        {1000, 1500, 50, 50, 15 },
        {1500, 2000, 40, 60, 30 },
        {2000, 2500, 20, 50, 50 },
        {2500, 3000, 10, 50, 60 }
    };


    private Vector3 center = Vector3.zero;
    private Vector3 anchoirPoint;
    private float killDistance;
    private float radiusForSpawningEnemies;
    private float radiusForAnchorSpawns = 4.0f;

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
        var SmallBots = GameObject.FindGameObjectsWithTag("SmallBot").Length;
        var MediumBots = GameObject.FindGameObjectsWithTag("MediumBot").Length;
        var LargeBots = GameObject.FindGameObjectsWithTag("LargeBot").Length;
        currentBots = SmallBots + MediumBots + LargeBots;
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
        int botLvl = this.GetBotLevel();
        this.setEnemiesChance();
        Vector3 botSpawnPoint = RandomPointOnCircleDiameter(center, radiusForSpawningEnemies);
        this.getAnchorPoint(center, radiusForAnchorSpawns);
        GameObject bot = Instantiate(botPreFabs[botLvl-1], botSpawnPoint, Quaternion.identity);

        bot.GetComponent<Bot>().setAnchoirPoint(anchoirPoint);
        bot.GetComponent<Bot>().setDeathDistance(killDistance);
        bot.GetComponent<Bot>().setBotLevel(botLvl);
        bot.GetComponent<Bot>().SetBotSpeed(botSpeed);
        bot.GetComponent<Bot>().setBotMass(botMass);
        bot.GetComponent<Bot>().setBotPointValues(BotPointValues);
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

    int GetBotLevel()
    {
        int random = Random.Range(0, 100);
        int lowLim;
        int hiLim = 0;
        for (int i = 0; i < enemiesChance.Length; i++)
        {
            lowLim = hiLim;
            hiLim += enemiesChance[i];
            if (random >= lowLim && random < hiLim)
            {
                return i + 1;
            }
        }
        return Random.Range(1,enemiesChance.Length+1);
    }

    private void setEnemiesChance()
    {
        int Score = GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().getScore();
        for (int i = 0; i < SpawnProb.GetLength(0); i++)
        {
            if (this.IsBetween(Score, SpawnProb[i,0], SpawnProb[i, 1])) enemiesChance = new int[] { SpawnProb[i, 2], SpawnProb[i, 3], SpawnProb[i, 4] };
        }
    }

    private bool IsBetween(int numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck < top);
    }
}