using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Math
{
    public struct Ellipse
    {
        public Single Width;
        public Single Height;

        public Ellipse(Single Width, Single Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public Single XRadius => Width / 2f;
        public Single YRadius => Height / 2f;

        public Boolean IsZero => Width <= 0f && Height <= 0f;

        /// <summary>
        /// Checks if <paramref name="RelativeLocation"/> is on or within the ellipse
        /// </summary>
        /// <param name="RelativeLocation">Location relative to the ellipse</param>
        /// <returns>True if the location is on or within</returns>
        public Boolean Contains(Vector2 RelativeLocation)
        {
            if (IsZero == true)
            {
                return false;
            }

            return Square(RelativeLocation.x) / Square(XRadius) + Square(RelativeLocation.y) / Square(YRadius) <= 1f;
        }

        private static Single Square(Single n) => Mathf.Pow(n, 2f);


    }

    public static class Extensions
    {
        public static Vector2 Rotate(this Vector2 Vector, float Degrees)
        {
            var Rad = Degrees * Mathf.Deg2Rad;
            return new Vector2(
                Vector.x * Mathf.Cos(Rad) - Vector.y * Mathf.Sin(Rad),
                Vector.x * Mathf.Sin(Rad) + Vector.y * Mathf.Cos(Rad)
            );
        }

        public static Single DistanceTo(this Vector2 This, Vector2 Vector) => (This - Vector).magnitude;

        public static Boolean Between(this Single This, Single Lower, Single Higher) => This >= Lower && This < Higher;
    }
}
