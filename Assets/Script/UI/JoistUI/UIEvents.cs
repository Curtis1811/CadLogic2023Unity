using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public Action<List<Vector2>, float, float> OnGenerateMesh;
    public Action OnClear;
}
