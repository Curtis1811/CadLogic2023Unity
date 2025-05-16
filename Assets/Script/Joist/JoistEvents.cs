using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoistEvents
{
    public Action<List<Vector2>, float, float> OnGenerateMesh;
    public Action OnClear;
}
