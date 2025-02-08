using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DefenseType { Normal, Invincilbe };

[Serializable]
public class Defense{

    public DefenseType defenseType;
    public int value;

}
