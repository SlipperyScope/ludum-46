using BBUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public enum Dances
{
    Sway,
    Kong
}

public class SwankAI : MonoBehaviour
{
    public BehaviorExecutor Behavior;
    protected UnityBlackboard Blackboard;



    public static Dictionary<Dances, String> DanceLookup = new Dictionary<Dances, String>
    {
        {Dances.Sway, "Sway" },
        {Dances.Kong, "Kong" }
    };

    //public Rect PlayArea;

    #region Dancing stuff
    public Boolean IsDancing { get; private set; }
    public Boolean Dance()
    {
        var Dances = Enum.GetValues(typeof(Dances));
        var Pick = (Dances)Dances.GetValue(Random.Range(0, Dances.Length));

        IsDancing = true;
        IsWalking = false;
        gameObject.GetComponentInChildren<Animator>().SetTrigger(DanceLookup[Pick]);
        return true;
    }
    #endregion

    #region Walking stuff
    public Boolean IsWalking { get; private set; }
    public Boolean Walk()
    {
        IsDancing = false;
        IsWalking = true;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Walk");
        return true;
    }
    #endregion

    void Start()
    {
        //Blackboard = Behavior.blackboard;
        //Blackboard.SetBehaviorParam("Entity", gameObject);
        //Blackboard.SetBehaviorParam("AI", this);
        //Blackboard.SetBehaviorParam("Destination", (Vector2)transform.position);
        //Blackboard.SetBehaviorParam("Bounds", PlayArea);
    }
}
