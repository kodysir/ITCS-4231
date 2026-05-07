using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Level1"); // change this to your scene name
    }
}