using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectableItem
{
    public class Crystal : CollectableBase
    {
        public override void GetCollected(CharacterBase collector)
        {
            base.GetCollected(collector);

            if (collector is Player)
            {
                if (!GameManager.Instance)
                {
                    print("Error is here");
                }
                GameManager.Instance.OnCrystalCollection(this);
            }
        }
    }

}
