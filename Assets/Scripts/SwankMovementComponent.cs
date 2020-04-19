using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankMovementComponent : MonoBehaviour
{
    public Single MoveSpeed = 10f;

    public Single YFactor = 9f/16f;

    public Vector2 Destination { get; set; }

    private void Update()
    {
        Debug.Log($"Pos: {(Vector2)transform.position} :: des: {Destination}");

        var position = (Vector2)transform.position;
        var direction = (Destination - position).normalized;
        direction.y *= YFactor;

        position += direction * MoveSpeed * Time.deltaTime;
        gameObject.transform.position = position;
    }
}
