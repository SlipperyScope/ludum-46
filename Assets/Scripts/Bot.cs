using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 1f;
    public int badGuyLevel = 1;
    private int pointValue = 100;

    void Awake()
    {
        transform.localScale = new Vector2(badGuyLevel * .5f, badGuyLevel);
        
        // TODO: Add code to get the sprite baded on badGuyLevel
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (speed * badGuyLevel) * Time.deltaTime;
    }
}
