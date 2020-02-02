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

    public AudioManager audioManager;

    public bool bushfire;
    public bool seedbomb;
    public bool sabotage;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
                        Debug.Log("We are in BUSHFIRE Card effect");
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
                        if (!manager.TrySabotage())
                        {
                            //card effect failed, undo the tile usage
                            sabotage = false;
                            card.used = false;
                        }
                        else
                        {
                            Debug.Log("it worked");
                            manager.PlayerTurn();
                        }
                        break;
                    }
                default:
                    break;
            }
            card.used = true;
        }
    }


    public ActiveCard GetCard(List<ActiveCard> cards, ActiveCardType ac)
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
        audioManager.PlaySound(Sounds.BUSHFIRE);
        ActiveCard selectedCard = GetCard(manager.currentPlayer.cards, ActiveCardType.BUSHFIRE);
        ActivateEffect(selectedCard);
    }

    public void SeedbombEffect() {
        Debug.Log("seedbomb");
        audioManager.PlaySound(Sounds.SEEDBOMB);
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
