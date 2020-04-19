using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BBUnity
{
    [RequireComponent(typeof(BehaviorExecutor))]
    public class BotAI : MonoBehaviour
    {
        protected const String BoundsBB = "Bounds";
        protected const String EntityBB = "Entity";
        protected const String ThresholdBB = "Threshold";
        protected const String YRatioBB = "YRatio";
        protected const String DestinationBB = "Destination";

        [Tooltip("Behavior to use")]
        public BehaviorExecutor Behavior;
        protected UnityBlackboard Blackboard;
        
        [Tooltip("Game area to move within")]
        //public GameArea GameArea;

        public Rect GameArea;

        [Tooltip("How fast the bot moves")]
        public Single MoveSpeed = 10f;

        public Vector2 HeyMoveHere { get; set; }

        /// <summary>
        /// SetBBValues
        /// </summary>
        protected void SetBBValues()
        {
            Blackboard.SetBehaviorParam(BoundsBB, GameArea);
        }

        /// <summary>
        /// Awake
        /// </summary>
        void Awake()
        {
            if (Behavior is null)
            {
                throw new ArgumentNullException("Set the behvaior in the inspector, fool");
            } 
            else 
            {
                Blackboard = Behavior.blackboard;
            }

            SetBBValues();
            HeyMoveHere = (Vector2)transform.position;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            var CurrentPosition = (Vector2)transform.position;
            var Direction = (HeyMoveHere - CurrentPosition).normalized;
            Direction.y *= 0.5625f;
            CurrentPosition += Direction * MoveSpeed * Time.deltaTime;

            transform.position = CurrentPosition;
        }
    }
}
