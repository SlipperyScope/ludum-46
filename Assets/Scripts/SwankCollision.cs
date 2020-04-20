using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwankCollision : MonoBehaviour
{
    private int ScoreNeededToKillMediumBot;
    private int Score;

    public void Start()
    {
        ScoreNeededToKillMediumBot = GameObject.FindGameObjectWithTag("BotSpawner").GetComponent<BotSpawner>().ScoreNeededToKillMediumBot;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            this.HandleCollison(collision);
        }
    }

    void HandleCollison(Collider2D collision)
    {
        Score = GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().getScore();

        if (collision.gameObject.CompareTag("SmallBot")) this.KillBot(collision);
        if (collision.gameObject.CompareTag("MediumBot"))
        {
            if (this.ifSwankKillsMediumBot())
            {
                this.KillBot(collision);
            }
            else { this.killSwank(); }
        }
        if (collision.gameObject.CompareTag("LargeBot")) this.killSwank();
    }
    
    void killSwank()
    {
        SceneManager.LoadScene("Gameover");
    }

    void KillBot(Collider2D collision)
    {
        this.ChangeSwankScale(new Vector3(.01f, .01f, 0));
        var bot = collision.gameObject.GetComponent<Bot>();
        int pointValue = bot.GetPointValue();
        GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().AdjustScore(pointValue);
        bot.ExplodeAndDie();
    }

    public bool ifSwankKillsMediumBot()
    {
        if(Score > ScoreNeededToKillMediumBot)
        {
            return true;
        }
        else { return false; }
    }

    private void ChangeSwankScale(Vector3 delta)
    {
        transform.localScale += delta;
    }
}




