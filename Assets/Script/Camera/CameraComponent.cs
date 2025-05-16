using UnityEngine;


public class CameraComponenet
{
    Camera _camera;

    InputEvents _inputEvents;
    private Vector3 _cameraDirection;
    private Vector3 _cameraRotation;

    public CameraComponenet(InputEvents inputEvents, Camera camera)
    {
        if (camera == null)
        {
            Debug.LogError("[CameraControls][CameraControls] Camera is null");
        }

        _camera = camera;
        _inputEvents = inputEvents;
        _inputEvents.MoveCamera += MoveCamera;
        _inputEvents.RotateCamera += RotateCameraWorldSpace;
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateCameraPositionAndRotation();
    }

    private void MoveCamera(Vector3 direction)
    {
        _cameraDirection = direction;
    }

    private void UpdateCameraPositionAndRotation()
    {
        _camera.transform.position += _cameraDirection * 4 * Time.deltaTime;
        _camera.transform.Rotate(Vector3.up, _cameraRotation.x * Time.deltaTime * 5);
        _camera.transform.Rotate(Vector3.left, _cameraRotation.y * Time.deltaTime * 5);
    }

    private void RotateCameraWorldSpace(Vector2 direction)
    {
        _cameraRotation = new Vector3(direction.x, direction.y, 0);
        //_camera.transform.Rotate(Vector3.up, direction.x);
    }

}
