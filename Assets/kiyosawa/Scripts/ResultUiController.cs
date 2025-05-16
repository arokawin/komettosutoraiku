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

    private void OnEnable()
    {
        _buttons[_nowSelectButtonNum].Select();
    }

}
