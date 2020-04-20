using UnityEngine;
using UnityEditor.Presets;

public class Bot : MonoBehaviour
{
    public Sprite[] BotSprites;
    public Preset[] BotColliders;
    public Preset[] FeetColliders;

    private SpriteRenderer SpriteRenderer;
    private Vector3 moveDirection;
    private float[] speed;
    private int botLevel;
    private string[] BotTags = new string[3] { "SmallBot", "MediumBot", "LargeBot" };
    private Vector3 anchoirPoint;
    private float killDistance;
    private int[] botMass;
    private int[] pointValues;

    void Awake()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public int GetPointValue()
    {
        return pointValues[botLevel-1];
    }

    public void SetBotSpeed(float[] setSpeed)
    {
        speed = setSpeed;
    }
    public void setBotLevel(int setLevel)
    {
        botLevel = setLevel;
    }
    public void setAnchoirPoint(Vector3 setPoint)
    {
        anchoirPoint = setPoint;
    }
    public void setDeathDistance(float setDistance)
    {
        killDistance = setDistance;
    }

    public void setBotMass(int[] setMass)
    {
        botMass = setMass;
    }
    public void setBotPointValues(int[] points)
    {
        pointValues = points;
    }
    private void MoveBot()
    {
        float step = speed[botLevel-1] * botLevel * Time.deltaTime;
        transform.position += moveDirection * step;
    }

    private void ConfigureBot(int botLevel)
    {
        moveDirection = (anchoirPoint - transform.position).normalized;
        if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        switch (botLevel)
        {
            case 1:
                this.BotProps(botLevel);
                break;
            case 2:
                this.BotProps(botLevel);
                break;
            case 3:
                this.BotProps(botLevel);
                break;
            default:
                this.BotProps(1);
                break;
        }
    }

    private void BotProps(int botLevel)
    {
        SpriteRenderer.sprite = BotSprites[botLevel - 1];
        BotColliders[botLevel - 1].ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
        FeetColliders[botLevel - 1].ApplyTo(gameObject.AddComponent<PolygonCollider2D>());
        GetComponent<Rigidbody2D>().mass = botMass[botLevel - 1];
        transform.gameObject.tag = BotTags[botLevel - 1];

    }

    private void ControlBotDeath()
    {
        if(Vector3.Distance(Vector3.zero,transform.position) > killDistance)
        {
            Destroy(gameObject);
        }
    }
}
