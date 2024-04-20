using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyQuit : MonoBehaviour
{
#if UNITY_STANDALONE
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
#endif
}
