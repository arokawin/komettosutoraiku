using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneGame : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeSpeed = 1.0f;
    public async void Restart_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        StartCoroutine(FadeIn());
        await Task.Delay(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        await Task.Yield();
    }
    public async void ChangeTitle_button()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        StartCoroutine(FadeIn());
        await Task.Delay(1000);
        SoundManager.Instance.PlayBgm(BGMType.BGM3);
        SceneManager.LoadScene("Title");
        await Task.Yield();
    }

    public async void ChangeGame()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        StartCoroutine(FadeIn());
        await Task.Delay(1000);
        SceneManager.LoadScene("GameMain");
    }

    public IEnumerator FadeIn()
    {
        fadePanel.enabled = true;
        float elapsedtime = 0.0f;
        Color startcolor = fadePanel.color;
        Color endcolor = new Color(startcolor.r, startcolor.g, startcolor.b, 1.0f);
        
        while (elapsedtime < fadeSpeed)
        {
            elapsedtime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedtime / fadeSpeed);
            fadePanel.color = Color.Lerp(startcolor, endcolor, t);
             yield return null;
        }
        fadePanel.color = endcolor;
        //SceneManager.LoadScene("GameMain");
    }

    public IEnumerator FadeOut()
    {
        float elapsedtime = 0.0f;
        Color endcolor = fadePanel.color;
        Color startcolor = new Color(endcolor.r, endcolor.g, endcolor.b, 1.0f);

        while (elapsedtime < fadeSpeed)
        {
            elapsedtime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedtime / fadeSpeed);
            fadePanel.color = Color.Lerp(startcolor, endcolor, t);
            yield return null;
        }
        fadePanel.color = startcolor;
        fadePanel.enabled = false;
    }

    public async void ReloadScene()
    {
        SoundManager.Instance.PlaySe(SEType.SE1);
        // 500ƒ~ƒŠ•b‘Ò‚Á‚Ä‚©‚çˆ—‚ğ‘±‚¯‚é
        await Task.Delay(500);
        StartCoroutine(FadeIn());
        await Task.Delay(1000);
        SceneManager.LoadScene("GameMain");
        await Task.Yield();
    }
    public void Credit()
    {
        
    }
}
