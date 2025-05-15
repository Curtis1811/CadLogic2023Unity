using System.Collections.Generic;
using UnityEngine;
using MathFunctions;
using VisualRerendering;

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
        Debug.Log("GenerateMesh called");
        // Handle the event here
        // Create a mesh from the points
        List<Vector3> pointsToVector3 = new List<Vector3>();

        points = new List<Vector2>();

        // here we need to rotate our points in the height and width direciton then scale them by that amount
        // and then we can create the mesh
        points[0] = new Vector2(0, 0);

        Vector2 pointB = new Vector2(6, 15);
        //MathHelper.JoistWidth2D(points[0], points[1], data.width, out List<Vector2> points);
        List<Vector3> points3D = new List<Vector3>();
        //visualHelper.CreateMesh(points);
    }

}
