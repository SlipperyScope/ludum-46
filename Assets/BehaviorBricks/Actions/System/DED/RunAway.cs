using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;
using Random = UnityEngine.Random;
using Math;

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
            var EatMedium = Entity.GetComponent<SwankCollision>().ifSwankKillsMediumBot();

            var SmallBots = GameObject.FindGameObjectsWithTag("SmallBot").ToList();
            var MediumBots = GameObject.FindGameObjectsWithTag("MediumBot").ToList();
            var LargeBots = GameObject.FindGameObjectsWithTag("LargeBot").ToList();
            var AllBots = SmallBots.Concat(MediumBots.Concat(LargeBots));
            
            var Nearby = AllBots.Where(b => Distance(Entity, b) <= PerceptionDistance).OrderBy(b => Distance(Entity, b));
            var NearbyBads = Nearby.Where(b => QueryIsBad(b, EatMedium));

            if (Nearby.Count() <= 0)
            {
                return TaskStatus.COMPLETED;
            }

            var Closest = NearbyBads.Count() > 0 ? NearbyBads.First() : Nearby.First();
            var ClosestIsFood = QueryIsBad(Closest, EatMedium) == false;

            var Rotation = Random.Range(-60, 60);
            var Direction = ((Vector2)Entity.transform.position - (Vector2)Closest.transform.position).normalized;
            if (ClosestIsFood == true)
            {
                Debug.Log("mmm");
                Direction *= -1f;
            }

            var Destination = (Vector2)Entity.transform.position + (Direction.Rotate(Rotation) * (PerceptionDistance + Threshold));

            Debug.DrawLine(Entity.transform.position, Destination, new Color(255, 0, 255), 5f);

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
        private Boolean QueryIsBad(GameObject Bot, Boolean CanEatMedium) => Bot.CompareTag("LargeBot") || (CanEatMedium == false && Bot.CompareTag("MediumBot"));
    }
}
