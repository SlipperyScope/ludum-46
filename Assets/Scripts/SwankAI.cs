using BBUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwankAI : MonoBehaviour
{
    public BehaviorExecutor Behavior;
    protected UnityBlackboard Blackboard;

    public Rect PlayArea;

    void Start()
    {
        Blackboard = Behavior.blackboard;
        Blackboard.SetBehaviorParam("Entity", gameObject);
        Blackboard.SetBehaviorParam("Destination", (Vector2)transform.position);
        Blackboard.SetBehaviorParam("Bounds", PlayArea);
    }
}
