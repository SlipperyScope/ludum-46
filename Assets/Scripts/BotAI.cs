using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BBUnity
{
    [RequireComponent(typeof(BehaviorExecutor))]
    public class BotAI : MonoBehaviour
    {
        [Tooltip("Behavior to use")]
        public BehaviorExecutor Behavior;
        protected UnityBlackboard Blackboard;
        public GameArea GameArea;


        void Awake()
        {
            if (Behavior is null)
            {
                throw new ArgumentNullException("Set the behvaior in the inspector, fool");
            }

            Blackboard = Behavior.blackboard;
            Blackboard.SetBehaviorParam("Area", GameArea.PlayArea);

            Debug.Log($"BT: {Behavior} BB: {Blackboard}");
        }
        private void Start()
        {
        }
    }
}
