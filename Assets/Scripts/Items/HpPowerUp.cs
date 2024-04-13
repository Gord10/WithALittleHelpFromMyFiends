using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectableItem
{
    public class HpPowerUp : CollectableBase
    {
        public override void GetCollected(CharacterBase collector)
        {
            base.GetCollected(collector);

            if (collector is Player player)
            {
                player.IncreaseHealth(value);
            }
        }
    }

}
