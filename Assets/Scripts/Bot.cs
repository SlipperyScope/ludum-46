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

    public Sprite firstLevelSprite;
    public Sprite secondLevelSprite;
    public Sprite thirdLevelSprite;

    void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        this.ChangeScale(botLevel);
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

    private void ChangeScale(int scaler)
    {
        transform.localScale = new Vector2(scaler * .5f, scaler);
    }

    private void MoveBotForward(float speed, int speedScaler)
    {
        transform.position += transform.up * (speed * speedScaler) * Time.deltaTime;
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
