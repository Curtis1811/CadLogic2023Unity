using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathFunctions;
using System;
using VisualRerendering;
using Unity.VisualScripting;
public class JoistPoints : MonoBehaviour
{

    struct Data
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

    // Start is called before the first frame update
    void Start()
    {
        Data data = new Data();
        data.SetData(new Vector2(0, 0), new Vector2(6, 15), 2);

        MathHelper.JoistWidth2D(data.pointA, data.pointB, data.width, out List<Vector2> points);

        VisualHelper.DrawPointsToScreen(data.pointA);
        VisualHelper.DrawPointsToScreen(data.pointB);
        VisualHelper.DrawLineToScreen(data.pointA, data.pointB);

        // First we are going to get the corss product of the two vectors

        foreach (var point in points)
        {
            // We are going to draw the points to the screen
            VisualHelper.DrawPointsToScreen(point, color: Color.red);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
