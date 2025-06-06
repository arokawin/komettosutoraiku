using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGame : MonoBehaviour
{
    // GetComponent での参照する時
    // 上で GetComponent で参照したいスクリプト名を参照
    //private SoundManager soundManager;

    public async void Restart_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ミリ秒待ってから処理を続ける
        Debug.Log("se");
        await Task.Delay(500);
        Debug.Log("seni");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        await Task.Yield();
    }
    public async void changeTitle_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ミリ秒待ってから処理を続ける
        await Task.Delay(500);
        SceneManager.LoadScene("Title");
        await Task.Yield();
    }
    public async void changeGame_button(bool deleteOldScene = false)
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ミリ秒待ってから処理を続ける
        await Task.Delay(500);
        SceneManager.LoadScene("GameMain");
        await Task.Yield();
    }

    public async void ReloadScene()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ミリ秒待ってから処理を続ける
        await Task.Delay(500);
        changeGame_button(true);
        await Task.Yield();
    }
}
