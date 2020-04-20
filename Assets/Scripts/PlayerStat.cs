using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        playerScore = 0;
    }

    public void AdjustScore(int points)
    {
        playerScore += points;
    }

    public int getScore()
    {
        return playerScore;
    }
}
