using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TODO: Change this to whatever the main scene is
            SceneManager.LoadScene("Adam");
        }
    }
}
