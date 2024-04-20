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
        new public Collider2D collider;

        LightSource lightSource;
        AudioSource audioSource;

        protected virtual void Awake()
        {
            cachedTransform = transform;
            lightSource = GetComponent<LightSource>();
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void GetCollected(CharacterBase collector)
        {
            isCollectable = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;

            if(lightSource != null)
            {
                lightSource.TurnOff();
            }

            if(collector is Player)
            {
                audioSource.pitch = Random.Range(0.99f, 1.01f);
                audioSource.Play();
            }
        }

        public float GetDistance(Vector3 point)
        {
            return Vector2.Distance(Transform.position, point);
        }

        public void Spawn(Vector3 position)
        {
            isCollectable = true;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            transform.position = position;
            gameObject.SetActive(true);

            if (lightSource != null)
            {
                lightSource.TurnOn(willStartFromZero: true);
            }
        }
    }
}

