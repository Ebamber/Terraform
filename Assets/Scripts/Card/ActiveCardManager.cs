using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class ActiveCardManager : MonoBehaviour
{
    public GameManager manager;

    public Button bushfireButton;
    public Button seedbombButton;
    public Button sabotageButton;

    public void ActivateEffect(ActiveCard card)
    {
        if (!card.used)
        {
            switch (card.cardType)
            {
                case ActiveCardType.BUSHFIRE:
                    {
                        BushfireEffect();
                        break;
                    }
                case ActiveCardType.SEEDBOMB:
                    {
                        SeedbombEffect();
                        break;
                    }
                case ActiveCardType.SABOTAGE:
                    {
                        SabotageEffect();
                        break;
                    }
                default:
                    break;
            }
            card.used = true;
        }
    }

    public void BushfireEffect() {
        Debug.Log("bushfire");
    }
    public void SeedbombEffect() {
        Debug.Log("seedbomb");
    }
    public void SabotageEffect() {
        Debug.Log("sabotage");
    }

    public void ShowActiveCardUI(ActiveCard[] cards)
    {
        foreach (ActiveCard card in cards)
        {
            switch (card.cardType)
            {
                case ActiveCardType.BUSHFIRE:
                    {
                        bushfireButton.interactable = card.used;
                        break;                      
                    }
                case ActiveCardType.SEEDBOMB:
                    {
                        seedbombButton.interactable = card.used;
                        break;
                    }
                case ActiveCardType.SABOTAGE:
                    {
                        sabotageButton.interactable = card.used;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
