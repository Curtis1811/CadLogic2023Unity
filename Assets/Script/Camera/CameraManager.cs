using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private CameraComponenet _cameraControls;

    public CameraManager(InputEvents inputEvents)
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("[CameraManager][CameraManager] Camera is null");
        }

        _cameraControls = new CameraComponenet(inputEvents, camera);
    }

    public void Update()
    {
        _cameraControls.Update();
    }

}
