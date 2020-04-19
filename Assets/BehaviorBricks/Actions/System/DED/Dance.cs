using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBUnity.Actions;
using Pada1.BBCore;
using UnityEngine;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;


namespace BBunity.Actions
{
    [Action("DED/Dance")]
    public class Dance : GOAction
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("AI")]
        public SwankAI AI;

        public override TaskStatus OnUpdate()
        {
            AI = Entity.GetComponent<SwankAI>();

            if (AI is null)
            {
                Debug.LogError($"{nameof(Entity)} is not a SwankAI");
                return TaskStatus.FAILED;
            }

            var IsDancing = AI.IsDancing;

            if (IsDancing == false)
            {
                IsDancing = AI.Dance();
            }

            return IsDancing ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        }
    }
}
