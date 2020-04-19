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

namespace BBUnity.Actions
{
    [Action("DED/ChooseDestination")]
    [Help("Picks a random location in a rectangle")]
    public class ChooseDestination : GOAction
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("Area")]
        public Rect Area;

        [OutParam("RandomLocation")]
        public Vector2 RandomLocation;

        public override void OnStart()
        {
            var X = Random.Range(Area.xMin, Area.xMax);
            var Y = Random.Range(Area.yMin, Area.yMax);
            
            RandomLocation = new Vector2(X, Y);

            var Swank = Entity.GetComponent<SwankMovementComponent>();
            if (Swank != null)
            {
                Swank.Destination = RandomLocation;
            }
        }

        /// <summary>
        /// OnUpdate
        /// </summary>
        public override TaskStatus OnUpdate()
        {
            if (Area.size == Vector2.zero)
            {
                Debug.LogWarning($"{nameof(Area)} is zero");
                return TaskStatus.FAILED;
            }

            return TaskStatus.COMPLETED;
        }
    }
}
