using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneGame : MonoBehaviour
{
    public void Restart_button()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void changeTitle_button()
    {
        SceneManager.LoadScene("Title");
    }
    public void changeGame_button()
    {
        SceneManager.LoadScene("GameMain");
    }
}
