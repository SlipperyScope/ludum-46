using Math;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankMovementComponent : MonoBehaviour
{
    public Single MoveSpeed = 2f;

    public Single YFactor = 9f / 16f;

    private Vector2 _Destination;
    public Vector2 Destination
    {
        get => _Destination;
        set
        {
            _Destination = value;
        }
    }

    public Int32 StepFrames = 7;
    public  Int32 PauseFrames = 11;
    public Int32 FramesSinceStartWalk;

    public SpriteRenderer SwankySprite;

    public Boolean _IsWalking;
    public Boolean IsWalking
    {
        get => _IsWalking;
        set
        {
            if (value != _IsWalking)
            {
                _IsWalking = value;
                if (IsWalking == true)
                {
                    FramesSinceStartWalk = 0;
                }
            }
        }
    }

    private void Start()
    {
        SwankySprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    

    private void FixedUpdate()
    {
        var position = (Vector2)transform.position;
        var direction = (Destination - position).normalized;
        direction.y *= YFactor;

        if (Mathf.Abs(direction.x) > 0)
        {
            SwankySprite.flipX = direction.x < 0;
        }

        if (position.DistanceTo(Destination) > 0f)
        {
            if (IsWalking == false)
            {
                IsWalking = true;
            }


            if (FramesSinceStartWalk > PauseFrames)
            {
                position += direction * MoveSpeed * Time.deltaTime;
                gameObject.transform.position = position;
            }

            if (FramesSinceStartWalk >= StepFrames + PauseFrames)
            {
                FramesSinceStartWalk = 0;
            } else
            {
                FramesSinceStartWalk++;
            }

        } else
        {
            IsWalking = false;
        }

    }

    public void StopMoving()
    {
        transform.position = Destination;
        Debug.Log("STOP: " + Destination + " :: " + transform.position);
    }
}
