﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private AudioSource chomp;
    private Animator sadAnimator;
    private AudioSource HornySource;


    protected void Awake()
    {
        velocity = Vector2.zero;
    }

    private void Start()
    {
        sadAnimator = gameObject.GetComponentInChildren<Animator>();
        sadSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        SetupAudioSources();
        canMove = true;
    }

    private void SetupAudioSources()
    {
        var Sources = gameObject.GetComponentsInChildren<AudioSource>();
        foreach (var Source in Sources)
        {
            Debug.Log(Source.clip.name);
            if (Source.clip.name == "HornSound")
            {
                HornySource = Source;
            }

            if (Source.clip.name == "chomp")
            {
                chomp = Source;
            }
        }
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
                this.Punch();
                this.PlayChomp();
            }


            if (Input.GetKeyDown(KeyCode.H))
            {
                HornySource.Play();
            }
        }
    }

    private void UpdateMove()
    {
        Vector2 direction = new Vector2(rightAxis, upAxis);
        var animatorState = sadAnimator.GetCurrentAnimatorStateInfo(0);
        if (!sadAnimator.IsInTransition(0) && !animatorState.IsName("HeroPunch"))
        {
            var walking = animatorState.IsName("HeroWalk");
            var moving = direction.magnitude > 0;
            if (!walking && moving)
            {
                sadAnimator.SetTrigger("Walk");
            } else if (walking && !moving)
            {
                sadAnimator.SetTrigger("Idle");
            }
        }

        if (Mathf.Abs(rightAxis) > 0)
        {
            sadSprite.flipX = rightAxis < 0;
        }

        var position = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    void Punch()
    {
        sadAnimator.SetTrigger("Punch");
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward) * Vector2.up;

        var hits = new HashSet<RaycastHit2D>();
        foreach (var offset in new[] { transform.right * 0f, transform.right * .5f, transform.up * .8f })
        {
            foreach (var hit in Physics2D.RaycastAll((Vector3)((Vector2)transform.position + new Vector2(0, -1)) + offset, rot, punchRange))
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
        }

        StartCoroutine(DisablePunch());
    }

    IEnumerator DisablePunch()
    {
        punchEnabled = false;
        //canMove = false;
        yield return new WaitForSeconds(2);
        punchEnabled = true;
        canMove = true;
    }

    void PlayChomp()
    {
        chomp.Play();
    }
}
