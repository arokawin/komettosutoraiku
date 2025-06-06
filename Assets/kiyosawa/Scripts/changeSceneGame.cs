using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGame : MonoBehaviour
{
    // GetComponent �ł̎Q�Ƃ��鎞
    // ��� GetComponent �ŎQ�Ƃ������X�N���v�g�����Q��
    //private SoundManager soundManager;

    public void Restart_button()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void changeTitle_button()
    {
        SceneManager.LoadScene("Title");
    }
    public void changeGame_button(bool deleteOldScene = false)
    {
        SceneManager.LoadScene("GameMain");
    }

    public void ReloadScene()
    {
        changeGame_button(true);
    }
}
