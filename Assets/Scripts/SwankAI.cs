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

    #region Dancing stuff
    public Boolean IsDancing { get; private set; }
    public Boolean Dance()
    {
        //IsDancing = true;
        return true;
    } 
    #endregion

    void Start()
    {
        //Debug.Log($"{nameof(Behavior.blackboard)}: {Behavior.blackboard}");
        Blackboard = Behavior.blackboard;
        Blackboard.SetBehaviorParam("Entity", gameObject);
        Blackboard.SetBehaviorParam("AI", this);
        Blackboard.SetBehaviorParam("Destination", (Vector2)transform.position);
        Blackboard.SetBehaviorParam("Bounds", PlayArea);
    }
}
