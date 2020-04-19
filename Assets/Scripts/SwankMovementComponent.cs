using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankMovementComponent : MonoBehaviour
{
    public Single MoveSpeed = 10f;

    public Vector2 Destination;

    public Single YFactor;

    private void Update()
    {
        var position = (Vector2)transform.position;
        var direction = (Destination - position).normalized;
        direction.y *= YFactor;

        position += direction * MoveSpeed * Time.deltaTime;
    }
}
