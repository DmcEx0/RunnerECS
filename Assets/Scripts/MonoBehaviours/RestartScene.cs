using UnityEngine;

public class RestartScene : MonoBehaviour
{
    public void RestartGame()
    {
        SceneService.ReloadScene();
    }
}
