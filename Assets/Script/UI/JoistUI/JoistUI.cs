
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JoistUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    #region VisualElements
    private VisualElement _canvas;
    private VisualElement _menuBar;
    private List<VisualElement> _generatedVE = new List<VisualElement>();
    private List<VisualElement> _points = new List<VisualElement>();
    #endregion

    #region Buttons
    private Toggle _AddPointButton;
    private Toggle _Align;
    private Button _showHideUI;
    private Button _GenerateMeshButton;
    private Button _clearButton;
    #endregion

    #region Data
    private FloatField _width;
    private FloatField _height;
    private FloatField _length;
    #endregion

    public UIEvents uiEvents;

    // TODO: Needs to be changed to use Local Position of Points but requires a change to listen to Canvas update.
    private List<Vector2> _dataPoints = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[JoistUI][Start]");
        _uiDocument = GetComponent<UIDocument>();

        _canvas = _uiDocument.rootVisualElement.Q("Interaction");

        _menuBar = _uiDocument.rootVisualElement.Q("MenuBar");

        _GenerateMeshButton = _uiDocument.rootVisualElement.Q<Button>("Generate");
        _GenerateMeshButton?.RegisterCallback<ClickEvent>(GenerateMesh);

        _clearButton = _uiDocument.rootVisualElement.Q<Button>("Clear");
        _clearButton?.RegisterCallback<ClickEvent>(ClearPoints);

        _AddPointButton = _uiDocument.rootVisualElement.Q<Toggle>("PlacePoints");
        _AddPointButton?.RegisterCallback<ClickEvent>(AddPointsToggle);

        _Align = _uiDocument.rootVisualElement.Q<Toggle>("AlignPoints");

        _showHideUI = _uiDocument.rootVisualElement.Q<Button>("ShowHide");
        _showHideUI?.RegisterCallback<ClickEvent>(ShowHideUi);

        _width = _uiDocument.rootVisualElement.Q<FloatField>("Width");
        _height = _uiDocument.rootVisualElement.Q<FloatField>("Height");
        _length = _uiDocument.rootVisualElement.Q<FloatField>("Length");
    }

    void Update()
    {
        if (_points.Count >= 2)
        {
            _AddPointButton.value = false;
            return;
        }
    }

    private void AddPointsToggle(ClickEvent evt)
    {
        if (_points.Count >= 2)
        {
            _AddPointButton.value = false;
            return;
        }

        if (_AddPointButton.value)
        {
            _canvas?.RegisterCallback<ClickEvent>(AddPointToCanvas);
        }
        else
        {
            _canvas?.UnregisterCallback<ClickEvent>(AddPointToCanvas);
        }
    }

    private void AddPointToCanvas(ClickEvent evt)
    {
        // Create a new point at the mouse position
        if (_points.Count >= 2)
        {
            _AddPointButton.value = false;
            return;
        }

        float radius = 15f;
        var point = new VisualElement();
        point.style.position = Position.Absolute;
        point.style.width = radius;
        point.style.height = radius;

        point.style.borderBottomLeftRadius = radius;
        point.style.borderBottomRightRadius = radius;
        point.style.borderTopLeftRadius = radius;
        point.style.borderTopRightRadius = radius;

        point.style.backgroundColor = Color.red;
        // We need to get the size of the menu bar and offset the style left and top points

        point.style.left = evt.position.x - _menuBar.resolvedStyle.width - (radius / 2);
        point.style.top = evt.position.y - (radius / 2);

        _dataPoints.Add(new Vector2(evt.position.x, evt.position.y));
        _points.Add(point);
        _canvas.Add(point);

        SetWidth();
    }

    private void SetWidth()
    {
        if (_points.Count >= 2)
        {
            _length.value = ConvertToUnitys(Vector2.Distance(_dataPoints[0], _dataPoints[1]));
            DrawJoistOutline(_width.value);
        }
    }

    private float ConvertToUnitys(float pixels)
    {
        // Convert pixels to Unity units
        //float unityUnits = pixels / 100f; // Assuming 100 pixels = 1 unit in Unity
        float unityUnits = pixels / 100f;
        return unityUnits;
    }

    private void GenerateMesh(ClickEvent evt)
    {
        List<Vector2> convertedPoint = new List<Vector2>();

        foreach (var point in _dataPoints)
        {
            var x = ConvertToUnitys(point.x);
            var y = ConvertToUnitys(point.y);

            convertedPoint.Add(new Vector2(x, y));
        }

        uiEvents?.OnGenerateMesh?.Invoke(convertedPoint, _width.value, _height.value);
    }

    private void ShowHideUi(ClickEvent evt)
    {
        if (_showHideUI.text == "Hide UI")
        {
            _canvas.style.display = DisplayStyle.None;
            _showHideUI.text = "Show UI";
        }
        else
        {
            _canvas.style.display = DisplayStyle.Flex;
            _showHideUI.text = "Hide UI";
        }
    }

    private void ClearPoints(ClickEvent evt)
    {
        foreach (var point in _points)
        {
            _canvas.Remove(point);
        }
        foreach (var ve in _generatedVE)
        {
            if (ve.parent != _canvas)
                continue;

            _canvas.Remove(ve);
        }
        _points.Clear();
        _dataPoints.Clear();
        uiEvents?.OnClear?.Invoke();
    }

    private void DrawJoistOutline(float width)
    {
        VisualElement line = new VisualElement();
        line.style.position = Position.Absolute;

        line.generateVisualContent += (context) =>
        {
            // Draw the line here
            // You can use a LineRenderer or any other method to draw the line
            context.painter2D.strokeColor = Color.red;
            context.painter2D.lineWidth = 9;
            context.painter2D.fillColor = Color.red;
            context.painter2D.strokeColor = Color.red;
            context.painter2D.lineJoin = LineJoin.Round;
            context.painter2D.lineCap = LineCap.Round;

            context.painter2D.BeginPath();
            context.painter2D.MoveTo(_dataPoints[0]);
            context.painter2D.LineTo(_dataPoints[1]);
            context.painter2D.Stroke();
        };

        float widthOfVE = _points[0].resolvedStyle.left - _points[1].resolvedStyle.left;
        float heightOfVE = _points[0].resolvedStyle.top - _points[1].resolvedStyle.top;

        if (heightOfVE < 0)
            heightOfVE *= -1;

        if (widthOfVE < 0)
            widthOfVE *= -1;

        line.style.height = widthOfVE;
        line.style.width = heightOfVE;
        //line.style.backgroundColor = Color.red;
        // line.style.left = _points[0].style.left;
        // line.style.top = _points[0].style.top;
        _canvas.Add(line);
        _generatedVE.Add(line);
    }
}
