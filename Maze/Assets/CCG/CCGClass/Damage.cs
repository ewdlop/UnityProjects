using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Normal, IgnoreArmor}
[Serializable]
public class Damage
{
    public DamageType damageType;
    public float value;
}
