using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

// We can name this joistHelper
namespace MathFunctions
{
    public static class MathHelper
    {
        /// <summary>
        /// Write a function which, given the position of the
        /// centre of both ends of a rectangular joist, and the
        /// joist’s width, calculates the position of each of the joist’s four corners.
        /// Next, write a test harness which defines some
        /// test data and uses it to test the above function
        /// and confirm that it appears to be working correctly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="width"></param>
        public static void JoistWidth2D(Vector2 a, Vector2 b, float width, out List<Vector2> JoistPoints)
        {
            // what we can do here is 1/2 the width get the right angle of the point and scale in that direction
            // to give us the the new points
            float scaledAmount = width / 2;

            VectorDirection(a, b, out Vector2 direction);
            direction = direction.normalized;
            RotateVector(direction, 1.57f, out Vector2 rotatedVector);

            Vector2 newAPoint = a + rotatedVector * scaledAmount;
            Vector2 newApoint2 = a - rotatedVector * scaledAmount;
            Vector2 newBPoint = b + rotatedVector * scaledAmount;
            Vector2 newBPoint2 = b - rotatedVector * scaledAmount;

            JoistPoints = new List<Vector2>
            {
                newAPoint,
                newApoint2,
                newBPoint,
                newBPoint2
            };

        }

        public static Vector3 RotateVector(Vector3 vector, Vector3 axis, float angle)
        {
            Vector3 vxp = Vector3.Cross(axis, vector);
            Vector3 vxvxp = Vector3.Cross(axis, vxp);
            return vector + Mathf.Sin(angle) * vxp + (1 - Mathf.Cos(angle)) * vxvxp;
        }

        public static void RotateVectorAboutPoint(Vector3 vector, Vector3 pivot, Vector3 axis, float angle, out Vector3 rotatedVector)
        {
            rotatedVector = RotateVector(vector - pivot, axis, angle);
        }

        public static float GetLength(Vector2 a, Vector2 b)
        {
            // We can use the pythagorean theorem to get the length of the line
            float x = b.x - a.x;
            float y = b.y - a.y;

            return Mathf.Sqrt(x * x + y * y);
        }

        public static Vector2 CrossProduct(Vector2 a, Vector2 b)
        {
            // We can use the cross product to get the angle between the two vectors
            return new Vector2(
                a.x * b.y - a.y * b.x,
                a.x * b.x + a.y * b.y
            );
        }

        public static Vector3 CrossProduct(Vector3 A, Vector3 B)
        {
            // We can use the cross product to get the angle between the two vectors
            return new Vector3(
                A.y * B.z - A.z * B.y,
                A.z * B.x - A.x * B.z,
                A.x * B.y - A.y * B.x
            );
        }

        public static float ScaleVector(Vector2 a, float scale)
        {
            // We can use the scale to get the new vector
            return a.x * scale;
        }

        public static void RotateVector(Vector2 a, float radian, out Vector2 dir)
        {
            // We can use the angle to get the new vector
            float x = a.x * Mathf.Cos(radian) - a.y * Mathf.Sin(radian);
            float y = a.x * Mathf.Sin(radian) + a.y * Mathf.Cos(radian);

            dir = new Vector2(x, y);

            Debug.Log("RotatedVector: " + x);
            Debug.Log("RotatedVector: " + y);
        }

        public static void RotateVectorAroundAxis(Vector3 a, float radian, Vector3 Axis, out Vector3 dir)
        {
            // We can use the angle to get the new vector
            float x = a.x * Mathf.Cos(radian) - a.y * Mathf.Sin(radian);
            float y = a.x * Mathf.Sin(radian) + a.y * Mathf.Cos(radian);

            dir = new Vector3(x, y, a.z);

            Debug.Log("RotatedVector: " + x);
            Debug.Log("RotatedVector: " + y);
        }

        public static void VectorDirection(Vector2 a, Vector2 b, out Vector2 dir)
        {
            // We can use the direction to get the new vector
            float x = b.x - a.x;
            float y = b.y - a.y;

            dir = new Vector2(x, y);
            Debug.Log("Direction X: " + x + " Direction Y: " + y);
        }

        public static void VectorDirection(Vector3 a, Vector3 b, out Vector3 dir)
        {
            // We can use the direction to get the new vector
            float x = b.x - a.x;
            float y = b.y - a.y;
            float z = b.z - a.z;

            dir = new Vector3(x, y, z);
            Debug.Log("Direction X: " + x + " Direction Y: " + y);
        }
        // When looking at Direciton of faces we generally use the right hand rule if we want to contitantly get the outward facing normal
        // we can use the cross product of the two vectors to get the normal
        public static Vector3 GetNormal(Vector3 a, Vector3 b)
        {
            // We can use the cross product to get the normal
            Vector3 normal = Vector3.Cross(a, b);
            return normal.normalized;
        }

        public static Vector3 GetNormalFaceDirection(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 u = b - a;
            Vector3 v = c - a;
            Vector3 crossProduct = CrossProduct(u, v);
            return crossProduct;
        }

        public static Vector3 GetCentre(List<Vector3> points)
        {
            // We can use the center to get the new vector
            Vector3 center = Vector3.zero;
            foreach (var point in points)
            {
                center += point;
            }
            return center / points.Count;
        }
    }
}

