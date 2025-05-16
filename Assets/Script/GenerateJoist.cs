using System.Collections.Generic;
using UnityEngine;
using MathFunctions;
using VisualRerendering;
using System;
using Unity.VisualScripting;

public class GenerateJoist
{
    VisualHelper visualHelper;
    JoistEvents _joistEventDispatcher;

    public struct Data
    {
        public void SetData(Vector2 pointA, Vector2 pointB, float width)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.width = width;
        }

        public Vector2 pointA { private set; get; }
        public Vector2 pointB { private set; get; }
        public float width { private set; get; }
    }

    public GenerateJoist(JoistEvents joistEvents)
    {
        _joistEventDispatcher = joistEvents;
        _joistEventDispatcher.OnGenerateMesh += GenerateMesh;


        visualHelper = new VisualHelper();
        //Data staticData = new Data();
        //data.SetData(new Vector2(0, 0), new Vector2(6, 15), 2);

        // MathHelper.JoistWidth2D(staticData.pointA, staticData.pointB, staticData.width, out List<Vector2> points);
        //visualHelper.DrawPointsToScreen(staticData.pointA);
        //visualHelper.DrawPointsToScreen(staticData.pointB);
        // visualHelper.DrawLineToScreen(staticData.pointA, staticData.pointB);

        // First we are going to get the corss product of the two vectors

        // foreach (var point in points)
        // {
        //     // We are going to draw the points to the screen
        //     visualHelper.DrawPointsToScreen(point, color: Color.red);
        // }

    }


    private void GenerateMesh(List<Vector2> points, float width, float height)
    {
        Debug.Log("GenerateMesh called PointCount:" + points.Count + " L:" + width + " H:" + height);

        MathHelper.JoistWidth2D(points[0], points[1], width, out List<Vector2> points2D);
        visualHelper.DrawPointsToScreen(points[0], Color.red);
        visualHelper.DrawPointsToScreen(points[1], Color.red);
        visualHelper.DrawLineToScreen(points[0], points[1], Color.red);

        foreach (var point in points2D)
        {
            // We are going to draw the points to the screen
            visualHelper.DrawPointsToScreen(point, color: Color.red);
        }

        List<Vector3> pointsToVector3 = new List<Vector3>();

        foreach (var point in points2D)
        {
            //MathHelper.CrossProduct(point, points[1], out Vector2 crossProduct);
            MathHelper.RotateVectorAboutPoint(point, Vector3.zero, new Vector3(0, 1, 0), 1.57f, out Vector3 rotatedVector);
            rotatedVector *= height / 2;
            visualHelper.DrawPointsToScreen(new Vector3(rotatedVector.x, rotatedVector.y, rotatedVector.z), Color.red);
            visualHelper.DrawLineToScreen(new Vector3(rotatedVector.x, rotatedVector.y, rotatedVector.z), new Vector3(rotatedVector.x, rotatedVector.y, 0) + new Vector3(0, 0, height), Color.red);

            MathHelper.RotateVector(point, -1.57f, out Vector2 rotatedVector2);
            rotatedVector2 *= height / 2;
            pointsToVector3.Add(new Vector3(rotatedVector.x, rotatedVector.y, 0));
            visualHelper.DrawPointsToScreen(new Vector3(rotatedVector2.x, rotatedVector2.y, rotatedVector.z), Color.red);
            visualHelper.DrawLineToScreen(new Vector3(rotatedVector2.x, rotatedVector2.y, 0), new Vector3(rotatedVector2.x, rotatedVector2.y, 0) + new Vector3(0, 0, height), Color.red);
        }

        // Create a mesh from the points

        Vector2 pointB = new Vector2(6, 15);
        //MathHelper.JoistWidth2D(points[0], points[1], data.width, out List<Vector2> points);
        List<Vector3> points3D = new List<Vector3>();
        //visualHelper.CreateMesh(points);
    }

}
