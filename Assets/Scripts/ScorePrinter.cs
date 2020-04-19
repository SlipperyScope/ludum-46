using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrinter : MonoBehaviour
{
    public Text scoreText;
    private PlayerStat ps;

    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>();
        scoreText.text = "" + ps.getScore();
    }

    void Update()
    {
        scoreText.text = "" + ps.getScore();
    }
}
