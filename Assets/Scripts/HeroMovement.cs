using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public SpriteRenderer sadSprite;
    public float moveSpeed = 2;
    public float thrust = .5f;
    public float punchRange = 1;
    public float punchForce = .1f;
    public bool canMove;
    private float rightAxis;
    private float upAxis;
    private float changeSpeed;
    private Vector2 velocity;


    protected void Awake()
    {
        velocity = Vector2.zero;
        changeSpeed = moveSpeed / thrust;
    }

    void Start()
    {
        sadSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rightAxis = Input.GetAxis("Horizontal");
            upAxis = Input.GetAxis("Vertical") * .75f;
            UpdateMove();

            if (Input.GetMouseButtonDown(0))
            {
                //set animation trigger
                this.Punch();
            }
        }
    }

    void UpdateMove()
    {
        Vector2 axisDirection   = new Vector2(rightAxis, upAxis);
        Vector2 currentVelocity = velocity;
        Vector2 desiredVelocity = axisDirection * moveSpeed;

        if (axisDirection == Vector2.zero)
        {
            desiredVelocity = Vector2.zero;
        }

        if (Mathf.Abs(rightAxis) > 0)
        {
            sadSprite.flipX = rightAxis < 0;
        }

        float velociyBoost = 2f - Vector2.Dot(currentVelocity.normalized, desiredVelocity.normalized);
        currentVelocity = Vector2.MoveTowards(currentVelocity, desiredVelocity, changeSpeed);

        Vector2 velocityChange = desiredVelocity * Time.deltaTime;
        transform.position += new Vector3(velocityChange.x, velocityChange.y, 0);
    }

    void Punch()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward) * Vector2.up;

        var hits = new HashSet<RaycastHit2D>();
        foreach (var i in new[] { -1, 0, 1 })
        {
            var offset = transform.right * 1.13f * i;
            foreach (var hit in Physics2D.RaycastAll(transform.position + offset, rot, punchRange))
            {
                if (hit.transform != transform)
                {
                    hits.Add(hit);
                }
            }

            foreach (var hit in hits)
            {
                var rb = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.AddForce(new Vector2(rot.x, rot.y) * punchForce, ForceMode2D.Impulse);
                }
            }
        }

    }
}
