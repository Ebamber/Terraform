using UnityEngine;
using System.Collections;

public class ActiveCard
{

    public ActiveCardType cardType;
    public bool used;
    public Player owner;

    public ActiveCard(ActiveCardType cardType, Player owner) {
        this.cardType = cardType;
        used = false;
        this.owner = owner;
    }

}
