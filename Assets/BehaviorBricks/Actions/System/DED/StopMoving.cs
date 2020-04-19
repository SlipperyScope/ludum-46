using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace BBUnity.Actions
{
    [Action("DED/StopMoving")]
    public class StopMoving : GOAction
    {
        [InParam("Entity")]
        public GameObject Entity;

        [OutParam("RandomLocation")]
        public Vector2 RandomLocation;

        public override TaskStatus OnUpdate()
        {
            Entity.GetComponent<SwankMovementComponent>().Destination = Entity.transform.position;
            return TaskStatus.COMPLETED;
        }
    }
}
