using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Presets;

public class Bot : MonoBehaviour
{
    public float speed = 1f;
    public int botLevel = 1;
    public int[] botMass = { 30, 60, 100 };

    // private bool alive = true;

    public Vector3 anchoirPoint;
    public float killDistance;

    public Sprite firstLevelSprite;
    public Sprite secondLevelSprite;
    public Sprite thirdLevelSprite;

    public Preset SmallBotCollider;
    public Preset MediumBotCollider;
    public Preset LargeBotCollider;

    public Preset SmallBotFeetCollider;
    public Preset MediumBotFeetCollider;
    public Preset LargeBotFeetCollider;

    private SpriteRenderer spriteR;
    private Vector3 moveDirection;
    private int pointValue;

    void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        this.ConfigureBot(botLevel);

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

    private void ConfigureBot(int botLevel)
    {
        Destroy(GetComponent<PolygonCollider2D>());
        moveDirection = (anchoirPoint - transform.position).normalized;
        if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        pointValue = 100 * botLevel;
        this.SetBotMass(botLevel);

        switch (botLevel)
        {
            case 1:
                spriteR.sprite = firstLevelSprite;
                SmallBotCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                SmallBotFeetCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 2:
                spriteR.sprite = secondLevelSprite;
                MediumBotCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                MediumBotFeetCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 3:
                spriteR.sprite = thirdLevelSprite;
                LargeBotCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                LargeBotFeetCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            default:
                spriteR.sprite = firstLevelSprite;
                SmallBotCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                SmallBotFeetCollider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
        }
        Debug.Log(pointValue);
    }

    private void SetBotMass(int botLevel)
    {

        GetComponent<Rigidbody2D>().mass = botMass[botLevel-1];
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
