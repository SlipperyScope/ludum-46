using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Presets;

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

    public Preset firstLevelPreset;
    public Preset secondLevelPreset;
    public Preset ThirdLevelPreset;


    private Vector3 moveDirection;


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
        moveDirection = (anchoirPoint - transform.position).normalized;
        if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        switch (botLevel)
        {
            case 1:
                spriteR.sprite = firstLevelSprite;
                firstLevelPreset.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 2:
                spriteR.sprite = secondLevelSprite;
                secondLevelPreset.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 3:
                spriteR.sprite = thirdLevelSprite;
                ThirdLevelPreset.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            default:
                spriteR.sprite = firstLevelSprite;
                firstLevelPreset.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
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
