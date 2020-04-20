using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwankCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var score = GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().getScore();

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
        //if(killSwank condition met){
        //    this.killSwank();
        //}
        this.KillBot(collision);

    }
    
    void killSwank()
    {
        SceneManager.LoadScene("Gameover");
    }

    void KillBot(Collider2D collision)
    {
        int pointValue = collision.gameObject.GetComponent<Bot>().GetPointValue();
        GameObject.FindGameObjectWithTag("SwankyMcDancepants").GetComponent<PlayerStat>().AdjustScore(pointValue);
        Destroy(collision.gameObject);
    }

}




