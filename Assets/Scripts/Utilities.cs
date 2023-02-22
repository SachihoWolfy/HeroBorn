using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Utilities
{
    public static int PlayerDeaths = 0;

    public static string UpdateDeathCounter(ref int countReference)
    {
        countReference += 1;
        return "Next time you'll be at " + countReference;
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public static bool RestartLevel(int sceneIndex)
    {
        Debug.Log("Player Deaths: " + PlayerDeaths);
        string message = UpdateDeathCounter(ref PlayerDeaths);
        Debug.Log("Player Deaths: " + PlayerDeaths);
        Debug.Log(message);
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
        return true;
    }
}
