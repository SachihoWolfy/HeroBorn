using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Selector : MonoBehaviour
{
    string sceneName;
    Scene targetScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Level_Road");
            Time.timeScale = 1.0f;
        }
    }
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
