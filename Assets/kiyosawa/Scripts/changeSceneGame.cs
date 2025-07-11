using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneGame : MonoBehaviour
{
    [SerializeField] private FadeManager fadePanel;

    private void Start()
    {
        
    }

    public async void Restart_button()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        await fadePanel.FadeIn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public async void ChangeTitle_button()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        await fadePanel.FadeIn();
        SoundManager.Instance.PlayBgm(BGMType.BGM3);
        SceneManager.LoadScene("Title");
    }

    public async void ChangeGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.Instance.PlaySe(SEType.SE1);
        await fadePanel.FadeIn();
        SceneManager.LoadScene("GameMain");
    }

    //async void FadeIn()
    //{

    //}


    public async void ReloadScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        //FadeIn();
        await Task.Delay(1000);
        SceneManager.LoadScene("GameMain");
        await Task.Yield();
    }
    public void Credit()
    {
    }

    async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        FadeManager fade = FindObjectOfType<FadeManager>();
        await fade.FadeOut();
    }
}
