using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardFactory
{
   public PlayerCard factory(CardType cardType) {
        switch (cardType) {
            case CardType.ARCHAEOLOGIST: {
                return new ArchaeologistCard();
            }
            case CardType.ASSASSIN: {
                    return new AssassinCard();
                }
            case CardType.DEFENDER: {
                    return new DefenderCard();
                }
            case CardType.DEVELOPER: {
                    return new DeveloperCard();
                }
            case CardType.EXPLORER: {
                    return new ExplorerCard();
                }
            case CardType.LANDMAGNATE: {
                    return new LandMagnate();
                }
            case CardType.MINER: {
                    return new MinerCard();
                }
            case CardType.POLITICIAN: {
                    return new PoliticianCard();
                }
            case CardType.REBEL: {
                    return new RebelCard();
                }
            case CardType.SCIENTIST: {
                    return new ScientistCard();
                }
            default: {
                    return null;
                }
        }
   }

}
