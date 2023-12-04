using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Utilities
{
    public static class VectorUtilities
    {
        public static Vector2 GetXY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 GetXZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        public static Vector3 ToVecto3XY(this Vector2 vec, float z = 0)
        {
            return new Vector3(vec.x, vec.y, z);
        }

        public static Vector3 ToVecto3XZ(this Vector2 vec, float y = 0)
        {
            return new Vector3(vec.x, y, vec.y);
        }

        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                oneMinusT * oneMinusT * p0 +
                2f * oneMinusT * t * p1 +
                t * t * p2;
        }

        public static Vector2 RightVector(this Vector2 vector2)
        {
            return new Vector2(vector2.y, -vector2.x);
        }

        public static Vector2 LeftVector(this Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }
    }
}
