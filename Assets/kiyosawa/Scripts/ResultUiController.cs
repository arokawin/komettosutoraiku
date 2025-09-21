using UnityEngine;
using UnityEngine.UI;

public class ResultUiController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    
    private int nowSelectButtonNum = 0;

    /// <summary>
    /// UI が有効化されたら０番目のボタンが選択される
    /// </summary>
    private void OnEnable()
    {
        buttons[nowSelectButtonNum].Select();
    }
}
