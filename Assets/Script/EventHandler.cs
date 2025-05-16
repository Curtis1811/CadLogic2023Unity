using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler
{
    UIEvents _uiEvents;
    JoistEvents _joistEventDispatcher;

    public EventHandler(UIEvents uiEvents, JoistEvents joistEventDispatcher)
    {
        _uiEvents = uiEvents;
        _joistEventDispatcher = joistEventDispatcher;

        #region UI Events
        _uiEvents.OnGenerateMesh += OnUIGenerateMesh;
        _uiEvents.OnClear += OnClear;
        #endregion
    }


    public void OnUIGenerateMesh(List<Vector2> points, float width, float height)
    {
        _joistEventDispatcher.OnGenerateMesh?.Invoke(points, width, height);
    }

    public void OnClear()
    {
        _joistEventDispatcher.OnClear?.Invoke();
    }

    public void OnDestroy()
    {
        #region UI Events
        _uiEvents.OnGenerateMesh -= OnUIGenerateMesh;
        _uiEvents.OnClear -= OnClear;
        #endregion
    }

}
