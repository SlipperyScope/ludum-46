using BBUnity.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore;
using UnityEngine;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace BBUnity.Actions
{
    [Action("DED/MoveTowards")]
    public class MoveTowards : GOAction
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("Location")]
        public Vector2 Location;

        [InParam("MoveSpeed")]
        public Single MoveSpeed;

        public override TaskStatus OnUpdate()
        {
            var CurrentPosition = Entity.transform.position;
            var Heading = (Vector2)CurrentPosition - Location;
            var Velocity = Heading.normalized * MoveSpeed * Time.deltaTime;
            CurrentPosition += (Vector3)Velocity;

            Entity.transform.position = CurrentPosition;
            return TaskStatus.RUNNING;
        }
    }
}
