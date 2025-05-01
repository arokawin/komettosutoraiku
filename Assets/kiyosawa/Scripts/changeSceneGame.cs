using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneGame : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("game main kari");
    }
}
