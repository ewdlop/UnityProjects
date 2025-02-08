using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealingType {none,Normal}
[Serializable]
public class Healing{
    public HealingType healingType;
    public float increase;
    public float value;
}
