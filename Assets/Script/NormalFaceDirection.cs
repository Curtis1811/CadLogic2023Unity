using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathFunctions;
using VisualRerendering;

public class NormalFaceDirection : MonoBehaviour
{
    VisualHelper visualHelper;

    struct Data
    {
        public void SetData(List<Vector3> verticies)
        {
            this.verticies = verticies;
        }

        public List<Vector3> verticies { private set; get; }

    }

    // Start is called before the first frame update
    void Start()
    {
        visualHelper = new VisualHelper();
        List<Vector3> verticies = new List<Vector3>
        {
            new Vector3(1, 1, 5),
            new Vector3(1, 2, 5),
            new Vector3(2, 2, 3),
            new Vector3(2 ,1, 3),
        };

        Data data = new Data();
        data.SetData(verticies);

        CreateMesh(verticies);

        Vector3 FaceDirection = MathHelper.GetNormalFaceDirection(verticies[0], verticies[1], verticies[2]);
        Vector3 centre = MathHelper.GetCentre(verticies);
        // Draw the points to the screen
        visualHelper.DrawLineToScreen(centre, centre + FaceDirection * 1, Color.blue);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateMesh(List<Vector3> verticies)
    {
        Mesh mesh = new Mesh();
        if (this.gameObject.GetComponent<MeshFilter>() == null)
        {
            this.gameObject.AddComponent<MeshFilter>();
        }
        if (this.gameObject.GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.AddComponent<MeshRenderer>();
        }

        GetComponent<MeshFilter>().mesh = mesh;

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

    /// <summary>
    /// For second part of the assignment
    /* 
            int tris = 0;
            for (int x = 0; x <= verticies.Count; x++)
                {
                if (triangles.Count >= verticies.Count)
                {
                    return;
                }

                if (2 + x > verticies.Count)
                {
                    triangles.Add(verticies.Count - 1);
                    triangles.Add(verticies.Count - 2);
                    triangles.Add(0);
                }

                triangles.Add(0 + x);
                triangles.Add(1 + x);
                triangles.Add(2 + x);

                tris += 3;

                Debug.Log(tris);
                Debug.Log(triangles.Count);
            }

            mesh.triangles = triangles.ToArray();
             mesh.triangles = new int[]
            {
                0, 1, 2,
                0, 2, 3
            };

            */
    /// </summary>


}
