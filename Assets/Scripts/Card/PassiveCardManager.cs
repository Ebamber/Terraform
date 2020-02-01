using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassiveCardManager : MonoBehaviour
{
    private string[] randomCardArray;

    public void ActivateEffect(CardType card){
        switch(card)
        {
            case CardType.ARCHAEOLOGIST:
                {
                    ArchaeologistEffect();
                    break;
                }
                
            case CardType.ASSASSIN:
                {
                    AssassinEffect();
                    break;
                }
                
                
            case CardType.DEFENDER:
                {
                    DefenderEffect();
                    break;
                }
                
            case CardType.DEVELOPER:
                {
                    DeveloperEffect();
                    break;
                }
                
            case CardType.EXPLORER:
                {
                    ExplorerEffect();
                    break;
                }
                
            case CardType.LANDMAGNATE:
                {
                    LandMagnateEffect();
                    break;
                }
                
            case CardType.MINER:
                {
                    MinerEffect();
                    break;
                }
                
            case CardType.POLITICIAN:
                {
                    PoliticianEffect();
                    break;
                }
                
            case CardType.REBEL:
                {
                    RebelEffect();
                    break;
                }
                
            case CardType.SCIENTIST:
                {
                    ScientistEffect();
                    break;
                }
                
            default:
                break;
        }
    }


    /// <summary>
    /// Assigns a random card to each player.
    /// </summary>
    /// <param name="playerNumber"></param>
    public CardType AssignRandomCard(int playerNumber)
    {
        string card = randomCardArray.ElementAt(playerNumber);
        Debug.Log("Assigning "+card+" to player "+ playerNumber);
        return (CardType)System.Enum.Parse(typeof(CardType), card);
    }

    private void ArchaeologistEffect() {
    }

    private void AssassinEffect()
    {
    }

    private void DefenderEffect()
    {
    }

    private void DeveloperEffect()
    {
    }

    private void ExplorerEffect()
    {
    }

    private void LandMagnateEffect()
    {
    }

    private void MinerEffect()
    {
    }

    private void PoliticianEffect()
    {
    }

    private void RebelEffect()
    {
    }

    private void ScientistEffect()
    {
    }

    public void Awake()
    {
        randomCardArray = System.Enum.GetNames(typeof(CardType)).OrderBy(x => Random.value).ToArray();
    }
}
