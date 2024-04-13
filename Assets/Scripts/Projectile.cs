using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    public float damage = 3;

    Rigidbody2D rigidbody;
    Vector2 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Mob") || collision.collider.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent(out CharacterBase character))
            {
                character.ReceiveDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
