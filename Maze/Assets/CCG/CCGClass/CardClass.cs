using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardClassType {
    Wow, StarWars, YuGiOh, Pokemon, DragonBall,Internet, OnePiece}
[Serializable]
public class CardClass {
    public CardClassType cardClassType;
}
