using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneService
{
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
