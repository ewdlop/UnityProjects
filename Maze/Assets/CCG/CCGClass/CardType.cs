using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpellTypeList { None,
    SingleTargetDamage,
    SingleTargetDamageNonPlayer,
    AoEDamage,
    RandomSingleTargetDamage,
    SingleTargetHealing,
    HeartOfTheCard,
    SingleTargetRandomDamage
}
public enum CardTypeList { Unit, Spell }

[Serializable]
public class CardType{

    public CardTypeList cardTypeList;
    public SpellTypeList spellTypeList;
}
