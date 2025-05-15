using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoistManager
{
    // Start is called before the first frame update
    JoistEvents _joistEvents;
    GenerateJoist _generateJoist;

    public JoistManager()
    {
        _joistEvents = new JoistEvents();
        _generateJoist = new GenerateJoist(_joistEvents);
    }

    public JoistEvents joistEvents()
    {
        return _joistEvents;
    }


}
