using UnityEngine;
using UnityEngine.UIElements;

public class UIManager
{
    UIEvents _uiEvents;

    public UIManager()
    {
        _uiEvents = new UIEvents();
        AddJoistUIToScene();
    }

    public void AddJoistUIToScene()
    {
        GameObject joistUIGO = new GameObject("UIDocument");
        UIDocument uiDocument = joistUIGO.AddComponent<UIDocument>();
        JoistUI joistui = joistUIGO.AddComponent<JoistUI>();
        joistui.uiEvents = _uiEvents;

        uiDocument.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("ui_joistVisualTree");
    }

    public UIEvents GetUiEvent()
    {
        return _uiEvents;
    }

}
