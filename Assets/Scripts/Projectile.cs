using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    public float damage = 3;
    public float lifeTime = 4;

    new Rigidbody2D rigidbody;
    Vector2 direction;
    float launchTime;
    LightSource lightSource;
    AudioSource audioSource;
    new Collider2D collider;
    SpriteRenderer spriteRenderer;
    bool isMoving = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lightSource = GetComponent<LightSource>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Launch(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
        launchTime = Time.time;
        gameObject.SetActive(true);
        lightSource?.TurnOn(true);

        collider.enabled = true;
        spriteRenderer.enabled = true;
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if(Time.time - launchTime >= lifeTime)
        {
            gameObject.SetActive(false);
            return;
        }

        if(isMoving)
        {
            rigidbody.velocity = direction * speed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Mob") || collision.collider.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent(out CharacterBase character))
            {
                
                collider.enabled = false;
                character.ReceiveDamage(damage);
                
            }
        }

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
        rigidbody.velocity = Vector2.zero;
        isMoving = false;
        StartCoroutine(FadeOff(audioSource.clip.length));
        //gameObject.SetActive(false);
    }

    IEnumerator FadeOff(float time)
    {
        lightSource.TurnOff();
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        //spriteRenderer.DOFade(0, audioSource.clip.length).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        spriteRenderer.DOKill();
    }
}
