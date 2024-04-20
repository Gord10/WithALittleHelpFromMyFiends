using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtIndicator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AudioSource hurtAudio;
    public Color hurtColor = Color.red;

    public float duration = 0.05f;
    float counter = 0;

    public void OnHurt(bool isDead = false)
    {
        counter = (isDead) ? 1000 : duration;
        spriteRenderer.color = hurtColor;
        enabled = true;
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if(hurtAudio!=null && !hurtAudio.isPlaying && Time.timeSinceLevelLoad > 0.1f)
        {
            hurtAudio.pitch = Random.Range(0.95f, 1.05f);
            hurtAudio.Play();
        }

        if(counter <= 0)
        {
            spriteRenderer.color = Color.white;
            enabled = false;
        }
    }
}
