using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public SpriteRenderer sadSprite;
    public float moveSpeed = 4;
    public float punchRange = 1.5f;
    public float punchForce = 100;
    public bool punchEnabled = true;
    public bool canMove;
    private float rightAxis;
    private float upAxis;
    private Vector2 velocity;


    protected void Awake()
    {
        velocity = Vector2.zero;
    }

    private void Start()
    {
        sadSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            rightAxis = Input.GetAxis("Horizontal");
            upAxis = Input.GetAxis("Vertical") * 9/16;
            UpdateMove();

            if (punchEnabled && Input.GetMouseButtonDown(0))
            {
                //set animation trigger
                this.Punch();
            }
        }
    }

    private void UpdateMove()
    {
        Vector2 direction = new Vector2(rightAxis, upAxis);

        if (Mathf.Abs(rightAxis) > 0)
        {
            sadSprite.flipX = rightAxis < 0;
        }

        var position = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    void Punch()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward) * Vector2.up;

        var hits = new HashSet<RaycastHit2D>();
        foreach (var hit in Physics2D.RaycastAll(transform.position, rot, punchRange))
        {
            if (hit.transform != transform)
            {
                var rb = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.AddForce(new Vector2(rot.x, rot.y) * punchForce, ForceMode2D.Impulse);
                }
                    hits.Add(hit);
                }
        }

        StartCoroutine(DisablePunch());
    }

    IEnumerator DisablePunch()
    {
        punchEnabled = false;
        yield return new WaitForSeconds(2);
        punchEnabled = true;
    }
}
