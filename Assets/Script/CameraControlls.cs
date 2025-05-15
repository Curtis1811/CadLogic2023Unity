using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    Camera camera;
    InputEvents inputEvents = new InputEvents();
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        inputEvents.MoveCamera += MoveCamera;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCamera(Vector3 Direction)
    {
        camera.transform.position += Direction;
    }

}
