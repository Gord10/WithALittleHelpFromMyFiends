using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && Time.timeSinceLevelLoad > 1)
        {
            SceneManager.LoadScene("Story");
        }
    }
}
