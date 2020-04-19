using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;

namespace BBCore.Conditions
{
    
    public class NotAtLocation : ConditionBase
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("Location")]
        public Vector2 Location;

        [InParam("Threshold")]
        public Single Threshold;

        /// <summary>
        /// Check
        /// </summary>
        public override Boolean Check()
        {
            if (Entity is null)
            {
                throw new NullReferenceException($"{nameof(Entity)} is not set");
            }

            var CurrentLocation = (Vector2)Entity.transform.position;
            var ThresholdSquared = Mathf.Pow(Threshold, 2f);
            var DistanceSquared = (CurrentLocation - Location).sqrMagnitude;

            if (DistanceSquared > ThresholdSquared)
            {
                return true;
            }

            return false;
        }
    }
}
