using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Presets;

public class Bot : MonoBehaviour
{
    public float speed = 1f;
    public int botLevel = 1;

    
    // private bool alive = true;
    private SpriteRenderer spriteR;
    public Vector3 anchoirPoint;
    public float killDistance;

    public Sprite firstLevelSprite;
    public Sprite secondLevelSprite;
    public Sprite thirdLevelSprite;

    public Preset level1Collider;
    public Preset level2Collider;
    public Preset level3Collider;

    public Preset level1Trigger;
    public Preset level2Trigger;
    public Preset level3Trigger;

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
        moveDirection = (anchoirPoint - transform.position).normalized;
        if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        pointValue = 100 * botLevel;

        switch (botLevel)
        {
            case 1:
                spriteR.sprite = firstLevelSprite;
                level1Collider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                level1Trigger.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 2:
                spriteR.sprite = secondLevelSprite;
                level2Collider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                level2Trigger.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            case 3:
                spriteR.sprite = thirdLevelSprite;
                level3Collider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                level3Trigger.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
            default:
                spriteR.sprite = firstLevelSprite;
                level1Collider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                level1Collider.ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
                break;
        }
        Debug.Log(pointValue);
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
