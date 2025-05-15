
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

    public UIEvents uiEvents;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[JoistUI][Start]");
        _uiDocument = GetComponent<UIDocument>();

        _canvas = _uiDocument.rootVisualElement.Q("Interaction");

        _menuBar = _uiDocument.rootVisualElement.Q("MenuBar");

        _GenerateMeshButton = _uiDocument.rootVisualElement.Q<Button>("Generate");
        _GenerateMeshButton?.RegisterCallback<ClickEvent>(ConvertToUnitys);

        _clearButton = _uiDocument.rootVisualElement.Q<Button>("Clear");
        _clearButton?.RegisterCallback<ClickEvent>(ClearPoints);

        _AddPointButton = _uiDocument.rootVisualElement.Q<Toggle>("PlacePoints");
        _AddPointButton?.RegisterCallback<ClickEvent>(AddPointsToggle);

        _Align = _uiDocument.rootVisualElement.Q<Toggle>("AlignPoints");

        _showHideUI = _uiDocument.rootVisualElement.Q<Button>("ShowHide");
        _showHideUI?.RegisterCallback<ClickEvent>(ShowHideUi);
    }

    private void AddPointsToggle(ClickEvent evt)
    {

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
        //point.transform.position = evt.position;
        _canvas.Add(point);
        _points.Add(point);
    }

    private void ConvertToUnitys(ClickEvent evt)
    {
        var point = new Vector2(_points[0].resolvedStyle.left, _points[0].resolvedStyle.top);
        var point2 = new Vector2(_points[1].resolvedStyle.left, _points[1].resolvedStyle.top);
        float distance = Vector2.Distance(point, point2);
        // Debug.Log("[JoistUI][ConvertToUnitys] Pixels : " + distance / 100f);
        // Debug.Log("[JoistUI][ConvertToUnitys] Pixels : " + distance);
        DrawJoist(distance);
        List<Vector2> points = new List<Vector2>
        {
            point,
            point2
        };

        uiEvents?.OnGenerateMesh?.Invoke(points, distance, 0);


        // Convert pixels to Unity units
        //float unityUnits = pixels / 100f; // Assuming 100 pixels = 1 unit in Unity
        //return unityUnits;
    }

    private void ShowHideUi(ClickEvent evt)
    {
        if (_showHideUI.text == "Hide UI")
        {
            _canvas.style.display = DisplayStyle.Flex;
            _showHideUI.text = "Show UI";
        }
        else
        {
            _canvas.style.display = DisplayStyle.None;
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
    }

    private void DrawJoist(float width)
    {
        VisualElement line = new VisualElement();
        line.style.position = Position.Absolute;

        line.generateVisualContent += (context) =>
        {
            // Draw the line here
            // You can use a LineRenderer or any other method to draw the line
            context.painter2D.strokeColor = Color.red;
            context.painter2D.lineWidth = 12;
            context.painter2D.fillColor = Color.red;
            context.painter2D.strokeColor = Color.red;
            context.painter2D.lineJoin = LineJoin.Round;
            context.painter2D.lineCap = LineCap.Round;

            context.painter2D.BeginPath();
            context.painter2D.MoveTo(new Vector2(0, 0));
            context.painter2D.LineTo(new Vector2(1920, 1080));
            Debug.Log("[JoistUI][DrawJoist] Point 1 : X " + _points[0].resolvedStyle.left + " Y " + _points[0].resolvedStyle.top);
            Debug.Log("[JoistUI][DrawJoist] Point 2 : X " + _points[1].resolvedStyle.left + " Y " + _points[1].resolvedStyle.top);
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
