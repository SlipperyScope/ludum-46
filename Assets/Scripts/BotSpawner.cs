using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public int maxBots = 1;
    private int currentBots = 0;
    public GameObject botPreFab;

    
    public float spawnDelay = .5f;
    private int[] enemiesChance = {100,0,0};

    public int ScoreNeededToKillMediumBot = 2000;
    public float BaseBotSpeed = 1f;
    public int[] botMass = { 30, 60, 100 };
    public int[] BotPointValues = { 50,150,200 };

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
        this.SetEnemyChance();
        Vector3 botSpawnPoint = RandomPointOnCircleDiameter(center, radiusForSpawningEnemies);
        this.getAnchorPoint(center, radiusForAnchorSpawns);
        GameObject bot = Instantiate(botPreFab, botSpawnPoint, Quaternion.identity);

        bot.GetComponent<Bot>().setAnchoirPoint(anchoirPoint);
        bot.GetComponent<Bot>().setDeathDistance(killDistance);
        bot.GetComponent<Bot>().setBotLevel(this.GetBotLevel());
        bot.GetComponent<Bot>().SetBotSpeed(BaseBotSpeed);
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
