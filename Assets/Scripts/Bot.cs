using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 1f;
    public int botLevel = 1;

    // private int pointValue = 100;
    // private bool alive = true;
    private SpriteRenderer spriteR;
    public Vector3 anchoirPoint;
    public float killDistance;

    public Sprite firstLevelSprite;
    public Sprite secondLevelSprite;
    public Sprite thirdLevelSprite;

    private Vector3 moveDirection;


    void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        this.SetBotSprite(botLevel);
        moveDirection = (anchoirPoint - transform.position).normalized;
        if (moveDirection.x < 0) spriteR.flipX = true; 
    }

    void Update()
    {
        this.MoveBot();
        this.ControlBotDeath();
    }

    private void MoveBot()
    {
        float step = speed * botLevel * Time.deltaTime;
        transform.position += moveDirection * step;
    }

    private void SetBotSprite(int botLevel)
    {
        switch (botLevel)
        {
            case 1:
                spriteR.sprite = firstLevelSprite;
                break;
            case 2:
                spriteR.sprite = secondLevelSprite;
                break;
            case 3:
                spriteR.sprite = thirdLevelSprite;
                break;
            default:
                spriteR.sprite = firstLevelSprite;
                break;
        }
    }

    private void ControlBotDeath()
    {
        if(Vector3.Distance(Vector3.zero,transform.position) > killDistance)
        {
            Destroy(gameObject);
        }

        // TODO: Handle collision with swank
    }
}
