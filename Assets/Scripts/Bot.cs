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
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveBot(speed, botLevel);
        this.ControlBotDeath();
    }

    private void MoveBot(float speed, int levelModifier)
    {
        float step = speed * levelModifier * Time.deltaTime;
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
    }
}
