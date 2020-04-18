using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 1f;
    public int badGuyLevel = 1;
    private int pointValue = 100;
    private bool alive = true;

    void Awake()
    {
        this.changeScale(badGuyLevel);
        
        // TODO: Add code to get the sprite baded on badGuyLevel
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.moveBotForward(speed, badGuyLevel);
    }

    private void changeScale(int scaler)
    {
        transform.localScale = new Vector2(scaler * .5f, scaler);
    }

    private void moveBotForward(float speed, int speedScaler)
    {
        transform.position += transform.up * (speed * speedScaler) * Time.deltaTime;
    }
}
