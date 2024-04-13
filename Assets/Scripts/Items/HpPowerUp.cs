using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollectableItem
{
    public class HpPowerUp : CollectableBase
    {
        public float respawnTime = 1;

        public override void GetCollected(CharacterBase collector)
        {
            base.GetCollected(collector);

            if (collector is Player player)
            {
                player.IncreaseHealth(value);
            }

            StartCoroutine(SpawnAfterWait());
        }

        IEnumerator SpawnAfterWait()
        {
            yield return new WaitForSeconds(respawnTime);
            Vector3 pos = GameManager.Instance.GetRandomPointInWorld();
            Spawn(pos);
            print("Food respawned");
        }
    }

}
