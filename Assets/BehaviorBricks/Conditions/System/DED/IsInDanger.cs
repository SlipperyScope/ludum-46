using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
using Math;
using BBUnity.Conditions;

namespace BBCore.Conditions
{
    [Condition("DED/IsInDanger")]
    public class IsInDanger : GOCondition
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("PerceptionDistance")]
        public Single PerceptionDistance;

        [InParam("Invert")]
        public Boolean Invert;

        public override bool Check()
        {
            var Bots = GameObject.FindGameObjectsWithTag("SmallBot").ToList();
            Bots.Concat(GameObject.FindGameObjectsWithTag("MediumBot").ToList());
            Bots.Concat(GameObject.FindGameObjectsWithTag("LargeBot").ToList());

            var Nearby = Bots.Where(b => Distance(Entity, b) <= PerceptionDistance);

            var HasNearby = Nearby.Count() > 0;


            if (Invert == false)
            {
                return HasNearby == true;
            }

            return HasNearby == false;
        }

        private Single Distance(GameObject A, GameObject B) => (A.transform.position - B.transform.position).magnitude;
    }
}
