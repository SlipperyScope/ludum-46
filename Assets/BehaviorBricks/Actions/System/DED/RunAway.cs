﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;
using Random = UnityEngine.Random;

namespace BBUnity.Actions
{
    [Action("BB/RunAway")]
    public class RunAway : GOAction
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("PerceptionDistance")]
        public Single PerceptionDistance;

        [InParam("Threshold")]
        public Single Threshold;

        [OutParam("RandomLocation")]
        public Vector2 RandomLocation;

        public override TaskStatus OnUpdate()
        {
            //Debug.Log("Running...");

            var SwankMove = Entity.GetComponent<SwankMovementComponent>();

            var Bots = GameObject.FindGameObjectsWithTag("bot").ToList();
            var Nearby = Bots.Where(b => Distance(Entity, b) <= PerceptionDistance);

            if (Nearby.Count() <= 0)
            {
                return TaskStatus.COMPLETED;
            }

            var Closest = Nearby.First();
            var Direction = ((Vector2)Entity.transform.position - (Vector2)Closest.transform.position).normalized;
            var Destination = (Vector2)Entity.transform.position + (Direction * (1.1f * PerceptionDistance + Threshold));

            if (SwankMove != null)
            {
                RandomLocation = Destination;
                SwankMove.Destination = Destination;
            }

            var AI = Entity.GetComponent<SwankAI>();
            var isWalking = AI.IsWalking;
            if (isWalking == false)
            {
                AI.Walk();
            }

            return TaskStatus.COMPLETED;
        }

        private Single Distance(GameObject A, GameObject B) => (A.transform.position - B.transform.position).magnitude;
    }
}
