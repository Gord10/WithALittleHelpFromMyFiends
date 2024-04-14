using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSource : MonoBehaviour
{
    public float changeTime = 1;

    Light2D light;
    float defaultLightIntensity;

    Coroutine coroutine;

    private void Awake()
    {
        light = GetComponent<Light2D>();
        defaultLightIntensity = light.intensity;
    }

    public void TurnOff()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ChangeIntensityCo(0));
    }

    public void TurnOn(bool willStartFromZero)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if(willStartFromZero)
        {
            light.intensity = 0;
        }

        light.enabled = true;
        coroutine = StartCoroutine(ChangeIntensityCo(defaultLightIntensity));
        
    }

    IEnumerator ChangeIntensityCo(float newIntensity)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        float changeSpeed = 1f / changeTime;

        while (!Mathf.Approximately(light.intensity, newIntensity))
        {
            light.intensity = Mathf.MoveTowards(light.intensity, newIntensity, changeSpeed * Time.deltaTime);
            yield return wait;
        }

        if(newIntensity == 0)
        {
            light.enabled = false;
        }
    }
}
