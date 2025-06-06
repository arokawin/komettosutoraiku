using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ResultUiController : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons;
    private int _nowSelectButtonNum = 0;

    /// <summary>
    /// UI が有効化されたら０番目のボタンが選択される
    /// </summary>
    private void OnEnable()
    {
        _buttons[_nowSelectButtonNum].Select();
    }

}
