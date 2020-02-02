using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActiveCard
{
    public ActiveCardType cardType;
    public bool used;
    public Player owner;
    public Button ui;

    public ActiveCard(ActiveCardType cardType, Player owner) {
        this.cardType = cardType;
        used = false;
        this.owner = owner;
    }

}
