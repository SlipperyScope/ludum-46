using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
using Math;

namespace BBCore.Conditions
{
    [Condition("DED/IsNotDancing")]
    public class IsNotDancing : ConditionBase
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("AI")]
        public SwankAI AI;

        public override bool Check()
        {
            AI = Entity.GetComponent<SwankAI>();

            if (AI is null)
            {
                Debug.LogError($"{nameof(AI)} does not have a SwankAI");
                return true;
            }

            return AI.IsDancing == false;
        }
    }
}
