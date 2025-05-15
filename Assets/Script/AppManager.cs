using UnityEngine;

public class AppManager : MonoBehaviour
{
    private UserInputManager _userInputManager;
    private CameraManager _cameraManager;
    private InputEvents _inputEvents;

    // Start is called before the first frame update
    void Start()
    {
        _inputEvents = new InputEvents();
        _userInputManager = new UserInputManager(_inputEvents);
        _cameraManager = new CameraManager(_inputEvents);
    }

    // Update is called once per frame
    void Update()
    {
        _cameraManager.Update();
    }

}
