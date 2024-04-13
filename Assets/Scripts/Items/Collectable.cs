using UnityEngine;

namespace CollectableItem
{
    public class CollectableBase : MonoBehaviour
    {
        public int value = 1;

        public Transform Transform => cachedTransform;
        Transform cachedTransform;

        public bool IsCollectable => isCollectable;
        bool isCollectable = true;

        public SpriteRenderer spriteRenderer;
        public Collider2D collider;

        protected virtual void Awake()
        {
            cachedTransform = transform;
        }

        public virtual void GetCollected(CharacterBase collector)
        {
            isCollectable = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            //gameObject.SetActive(false);
        }

        public float GetDistance(Vector3 point)
        {
            return Vector2.Distance(Transform.position, point);
        }
    }
}

