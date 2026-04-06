using System;
using System.Collections.Generic;

[Serializable]
public class BIMData
{
    public string guid;
    public string name;
    public string type;
    public string storey;
    public string elementId;
    public Dictionary<string, object> properties;
}