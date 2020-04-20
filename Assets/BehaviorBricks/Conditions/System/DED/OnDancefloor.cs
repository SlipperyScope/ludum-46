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
    [Condition("DED/OnDancefloor")]
public    class OnDancefloor : GOCondition
    {
        [InParam("Entity")]
        public GameObject Entity;

        [InParam("DanceRect")]
        public Rect DanceRect;

        [InParam("Invert")]
        public Boolean Invert;

        public override bool Check()
        {
            var OnDancefloor = DanceRect.Contains(Entity.transform.position);
            return Invert ? !OnDancefloor : OnDancefloor;
        }
    }
}
