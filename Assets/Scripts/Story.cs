using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI text;

    int index = 0;

    private void Awake()
    {
        text.text = lines[index];
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            index++;
            if(index == lines.Length)
            {
                SceneManager.LoadScene("Game");
                return;
            }

            text.text = lines[index];
        }
    }
}
