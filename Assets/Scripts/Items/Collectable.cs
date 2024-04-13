using UnityEngine;

namespace CollectableItem
{
    public class Collectable : MonoBehaviour
    {
        public int value = 1;

        public virtual void GetCollected(CharacterBase collector)
        {
            gameObject.SetActive(false);
        }
    }
}

