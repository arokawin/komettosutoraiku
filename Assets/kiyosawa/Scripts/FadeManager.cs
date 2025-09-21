using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeSpeed = 1.0f;

    private bool isFadeOut = false;

    Image FadeImage;

    void Start()
    {
        fadePanel = GetComponent<Image>();
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <returns></returns>
     public async Task FadeIn()
    {
        if (isFadeOut) return;
        isFadeOut = true;
        float elapsedtime = 0.0f;
        Color endcolor = new Color(0f, 0f, 0f, 0);
        Color startcolor = new Color(endcolor.r, endcolor.g, endcolor.b, 1.0f);

        while (elapsedtime < fadeSpeed)
        {
            elapsedtime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedtime / fadeSpeed);
            fadePanel.color = Color.Lerp(startcolor, endcolor, t);
            await Task.Yield();
        }
        fadePanel.color = endcolor;
        fadePanel.enabled = false;
        isFadeOut = false;
        await Task.Delay(1000);
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    public async Task FadeOut()
    {
        if (isFadeOut) return;
        isFadeOut = true;
        fadePanel.enabled = true;
        float elapsedtime = 0.0f;
        Color endcolor = Color.black;
        Color startcolor = new Color(endcolor.r, endcolor.g, endcolor.b, 0f);

        while (elapsedtime < fadeSpeed)
        {
            elapsedtime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedtime / fadeSpeed);
            fadePanel.color = Color.Lerp(startcolor, endcolor, t);
            await Task.Yield();
        }
        fadePanel.color = endcolor;
        isFadeOut = false;
        await Task.Delay(1000);
    }
}
