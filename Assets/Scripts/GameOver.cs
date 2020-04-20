using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    private PlayerStat ps;

    void Start()
    {
        ps = GameObject.FindObjectOfType<PlayerStat>();
        scoreText.text = "" + ps.getScore();
        Destroy(ps.gameObject);
    }
}
