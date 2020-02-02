using UnityEngine;
using System.Collections;

public class ActiveCardManager: MonoBehaviour
{
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
                case ActiveCardType.MASS_PRODUCTION:
                    {
                        MassProductionEffect();
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

    public void BushfireEffect() { }
    public void MassProductionEffect() { }
    public void SabotageEffect() { }
}
