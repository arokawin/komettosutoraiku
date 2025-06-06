using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGame : MonoBehaviour
{
    // GetComponent �ł̎Q�Ƃ��鎞
    // ��� GetComponent �ŎQ�Ƃ������X�N���v�g�����Q��
    //private SoundManager soundManager;

    public async void Restart_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500�~���b�҂��Ă��珈���𑱂���
        Debug.Log("se");
        await Task.Delay(500);
        Debug.Log("seni");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        await Task.Yield();
    }
    public async void changeTitle_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500�~���b�҂��Ă��珈���𑱂���
        await Task.Delay(500);
        SceneManager.LoadScene("Title");
        await Task.Yield();
    }
    public async void changeGame_button(bool deleteOldScene = false)
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500�~���b�҂��Ă��珈���𑱂���
        await Task.Delay(500);
        SceneManager.LoadScene("GameMain");
        await Task.Yield();
    }

    public async void ReloadScene()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500�~���b�҂��Ă��珈���𑱂���
        await Task.Delay(500);
        changeGame_button(true);
        await Task.Yield();
    }
}
