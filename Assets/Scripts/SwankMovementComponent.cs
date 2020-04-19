using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankMovementComponent : MonoBehaviour
{
    public Single MoveSpeed = 3f;

    public Single YFactor = 9f/16f;

    public Vector2 Destination { get; set; }

    public SpriteRenderer SwankySprite;

    private void Start()
    {
        SwankySprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var position = (Vector2)transform.position;
        var direction = (Destination - position).normalized;
        direction.y *= YFactor;

        if (Mathf.Abs(direction.x) > 0)
        {
            SwankySprite.flipX = direction.x < 0;
        }

        position += direction * MoveSpeed * Time.deltaTime;
        gameObject.transform.position = position;
    }

    public void StopMoving()
    {
        transform.position = Destination;
        Debug.Log("STOP: " + Destination + " :: " + transform.position);
    }
}
