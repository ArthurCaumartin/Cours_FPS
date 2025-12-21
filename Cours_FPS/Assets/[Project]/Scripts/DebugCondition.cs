using System;
using UnityEngine;

[Serializable]
public class DebugCondition
{
    public bool enable = true;
    public Color color1 = new Color(1, 0, 0, .5f);
    public Color color2 = new Color(0, 1, 0, .5f);
    public Color color3 = new Color(0, 0, 1, .5f);
}