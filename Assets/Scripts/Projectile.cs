using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    public float damage = 3;
    public float lifeTime = 4;

    Rigidbody2D rigidbody;
    Vector2 direction;
    float launchTime;
    LightSource lightSource;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lightSource = GetComponent<LightSource>();
    }

    public void Launch(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
        launchTime = Time.time;
        gameObject.SetActive(true);
        lightSource?.TurnOn(true);
    }

    private void FixedUpdate()
    {
        if(Time.time - launchTime >= lifeTime)
        {
            gameObject.SetActive(false);
            return;
        }
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
