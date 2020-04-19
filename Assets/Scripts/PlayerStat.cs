using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private float playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    public void AdjustScore(float points)
    {
        playerScore += points;
    }

    public float getScore()
    {
        return playerScore;
    }
}
