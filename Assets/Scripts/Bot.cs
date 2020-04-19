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

    public Sprite firstLevelSprite;
    public Sprite secondLevelSprite;
    public Sprite thirdLevelSprite;

    private Vector3 moveDirection;


    void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        this.ChangeScale(botLevel * .5f);

    }

    // Start is called before the first frame update
    void Start()
    {
        this.SetBotSprite(botLevel);
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveBotForward(speed, botLevel);
    }

    private void ChangeScale(float scaler)
    {
        transform.localScale = new Vector2(scaler * .5f, scaler);
    }

    private void MoveBotForward(float speed, int speedScaler)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, anchoirPoint, step);

        // transform.position += (anchoirPoint - transform.position).normalized *.0001f * Time.deltaTime;
        //transform.position += moveDirection * speed * Time.deltaTime;

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
}
