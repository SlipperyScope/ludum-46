using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public int maxBots = 100;
    private int currentBots = 0;
    public GameObject botPreFab;
    public GameObject circle;

    private Vector3 anchoirPoint;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 center = Vector3.zero;
        Vector3[] testVectors = new[] { new Vector3(3f, 0f, 0f), new Vector3(0f, 3f, 0f) };//, new Vector3(-3f, 0f, 0f), new Vector3(0f, -3f, 0f)};

        for (int i = currentBots; currentBots < maxBots; currentBots++)
        //foreach (Vector3 botSpawnPoint in testVectors)
        {
            Vector3 botSpawnPoint = RandomPointOnCircleDiameter(center, 10.0f);
            anchoirPoint = this.getAnchorPoint(center, 2f);

            Instantiate(circle, anchoirPoint, Quaternion.identity);

            this.SpawnBot(botSpawnPoint, anchoirPoint);
            Debug.DrawLine(botSpawnPoint, anchoirPoint, Color.red, 5f);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBot(Vector3 positionOfBot, Vector3 destination)
    {
        GameObject bot = Instantiate(botPreFab, positionOfBot, Quaternion.identity);
        bot.GetComponent<Bot>().anchoirPoint = anchoirPoint;
    }

    Vector3 getAnchorPoint(Vector3 center, float radius)
    {
       return this.RandomPointOnCircleDiameter(center, radius);
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
