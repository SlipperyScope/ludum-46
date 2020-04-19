using BBUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankAI : MonoBehaviour
{
    public BehaviorExecutor Behavior;
    protected UnityBlackboard Blackboard;

    public Rect PlayArea;

    public Boolean IsDancing { get; }
    public Boolean Dance()
    {
        return true;
    }

    void Start()
    {
        Blackboard = Behavior.blackboard;
        Blackboard.SetBehaviorParam("Entity", gameObject);
        Blackboard.SetBehaviorParam("Destination", (Vector2)transform.position);
        Blackboard.SetBehaviorParam("Bounds", PlayArea);
    }
}
