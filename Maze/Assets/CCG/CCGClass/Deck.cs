using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck{

    public List<Card> cards;
    public List<Card> graveYard;
    public string nickName;

    public Card GetRandomCard()
    {
        return cards[Random.Range(0, cards.Count)];
    }
}
