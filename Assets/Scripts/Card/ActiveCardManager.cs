using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(GameManager))]
public class ActiveCardManager : MonoBehaviour
{
    public GameManager manager;

    public Button bushfireButton;
    public Button seedbombButton;
    public Button sabotageButton;

    public bool bushfire;
    public bool seedbomb;
    public bool sabotage;

    private void Start()
    {
        manager = GetComponent<GameManager>();
        SetAllFalse();
    }

    public void SetAllFalse()
    {
        bushfire = false;
        seedbomb = false;
        sabotage = false;
    }

    public void ActivateEffect(ActiveCard card)
    {
        if (!card.used)
        {
            switch (card.cardType)
            {
                case ActiveCardType.BUSHFIRE:
                    {
                        bushfire = true;
                        break;
                    }
                case ActiveCardType.SEEDBOMB:
                    {
                        seedbomb = true;
                        break;
                    }
                case ActiveCardType.SABOTAGE:
                    {
                        sabotage = true;
                        break;
                    }
                default:
                    break;
            }
            card.used = true;
        }
    }


    private ActiveCard GetCard(List<ActiveCard> cards, ActiveCardType ac)
    {
        foreach (ActiveCard c in cards)
        {
            if (c.cardType.Equals(ac))
            {
                return c;
            }
        }
        return null;
    }

    public void BushfireEffect() {
        Debug.Log("bushfire");
        ActiveCard selectedCard = GetCard(manager.currentPlayer.cards, ActiveCardType.BUSHFIRE);
        ActivateEffect(selectedCard);
    }

    public void SeedbombEffect() {
        Debug.Log("seedbomb");
        ActiveCard selectedCard = GetCard(manager.currentPlayer.cards, ActiveCardType.SEEDBOMB);
        ActivateEffect(selectedCard);
    }
    public void SabotageEffect() {
        Debug.Log("sabotage");
        ActiveCard selectedCard = GetCard(manager.currentPlayer.cards, ActiveCardType.SABOTAGE);
        ActivateEffect(selectedCard);
    }

    public void ShowActiveCardUI(List<ActiveCard> cards)
    {
        foreach (ActiveCard card in cards)
        {
            switch (card.cardType)
            {
                case ActiveCardType.BUSHFIRE:
                    {  
                        bushfireButton.interactable = !card.used;
                        break;                      
                    }
                case ActiveCardType.SEEDBOMB:
                    {
                        seedbombButton.interactable = !card.used;
                        break;
                    }
                case ActiveCardType.SABOTAGE:
                    {
                        sabotageButton.interactable = !card.used;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
