using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    private UserInputManager _userInputManager;
    private CameraManager _cameraManager;
    private InputEvents _inputEvents;
    private UIManager _uiManager;
    private EventHandler _eventHandler;
    private JoistManager _joistManager;

    private
    // Start is called before the first frame update
    void Start()
    {
        _inputEvents = new InputEvents();
        _userInputManager = new UserInputManager(_inputEvents);
        _cameraManager = new CameraManager(_inputEvents);
        _uiManager = new UIManager();
        _joistManager = new JoistManager();
        _eventHandler = new EventHandler(_uiManager.GetUiEvent(), _joistManager.joistEvents());
    }

    // Update is called once per frame
    void Update()
    {
        _cameraManager.Update();
    }

}
