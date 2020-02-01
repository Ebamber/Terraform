using UnityEngine;
using System.Collections;

public class ActiveCardEffects: MonoBehaviour
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
                case ActiveCardType.RESISTANCE:
                    {
                        ResistanceEffect();
                        break;
                    }
                case ActiveCardType.TAKEOVER:
                    {
                        TakeoverEffect();
                        break;
                    }
                case ActiveCardType.TELEPORT:
                    {
                        TeleportEffect();
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
    public void ResistanceEffect() { }
    public void TakeoverEffect() { }
    public void TeleportEffect() { }
}
