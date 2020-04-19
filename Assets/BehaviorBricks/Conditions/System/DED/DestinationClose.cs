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
    [Condition("DED/DestinationClose")]
    public class DestinationClose : ConditionBase
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("Destination")]
        public Vector2 Destination;
        
        [InParam("Threshold")]
        public Single Threshold;

        [InParam("YRatio")]
        public Single YRatio;

        /// <summary>
        /// Check
        /// </summary>
        public override bool Check()
        {
            var DeltaLocation = Destination - (Vector2)Entity.transform.position;

            var YThreshold = Threshold * YRatio;
            var XThreshold = Threshold;

            var Ellipse = new Ellipse(XThreshold * 2f, YThreshold * 2f * YRatio);

            return Ellipse.Contains(DeltaLocation);
        }
    }
}
