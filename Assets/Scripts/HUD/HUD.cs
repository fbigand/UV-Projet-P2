using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Scores.reset();
    }
}
