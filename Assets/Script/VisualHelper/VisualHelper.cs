using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualRerendering
{
    // This class is used to draw points and lines to the screen
    // It is used for debugging purposes
    public class VisualHelper
    {
        public void DrawPointsToScreen(Vector3 Position, Color color = default)
        {
            // We are creating a point out of a primitive sphere
            // and setting its position to the given position
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.transform.position = Position;
            point.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            point.GetComponent<Renderer>().material.color = color;
        }

        public void DrawLineToScreen(Vector3 start, Vector3 end, Color color = default)
        {
            // We are creating a line out of a primitive cube
            // and setting its position to the given position
            GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cube);
            line.transform.position = (start + end) / 2;
            line.transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(start, end));
            line.transform.LookAt(end);
            line.GetComponent<Renderer>().material.color = color;
        }

        public void CreateMesh(List<Vector3> verticies)
        {
            Mesh mesh = new Mesh();
            GameObject gameObject = new GameObject("Mesh");

            if (gameObject.GetComponent<MeshFilter>() == null)
            {
                gameObject.AddComponent<MeshFilter>();
            }
            if (gameObject.GetComponent<MeshRenderer>() == null)
            {
                gameObject.AddComponent<MeshRenderer>();
            }

            //GetComponent<MeshFilter>().mesh = mesh;

            mesh.vertices = verticies.ToArray();
            mesh.uv = new List<Vector2>(verticies.Capacity).ToArray();
            mesh.normals = new List<Vector3>(verticies.Capacity).ToArray();
            mesh.triangles = new int[]
            {
                0, 1, 2,
                0, 2, 3
            };

            List<int> triangles = new List<int>();

            for (int i = 0; i < verticies.Count - 2; i++)
            {
                triangles.Add(0);
                triangles.Add(i + 1);
                triangles.Add(i + 2);
            }
        }
    }

}
