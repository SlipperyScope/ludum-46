using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwankCollision : MonoBehaviour
{
    public int ScoreNeededToKillMediumBot = 1500;

    private int Score;
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
        int pointValue = collision.gameObject.GetComponent<Bot>().GetPointValue();
        GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().AdjustScore(pointValue);
        Destroy(collision.gameObject);
    }

    public bool ifSwankKillsMediumBot()
    {
        Debug.Log($"Score{Score}, ScoreNeededToKillMediumBot {ScoreNeededToKillMediumBot}, condition {Score > ScoreNeededToKillMediumBot}");
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




