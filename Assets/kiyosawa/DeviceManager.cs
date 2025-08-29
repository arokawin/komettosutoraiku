using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class    DeviceManager : MonoBehaviour
{
    private static DeviceManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public static DeviceManager Instance => instance;

    private Gamepad[] _gamepad = new Gamepad[0];

    public Dictionary<int, Gamepad> Gamepads;

    public string[] JoystickNames = new string[0];

    //private int CurrentConnectionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Gamepads = new Dictionary<int, Gamepad>();
        UpdateConnectedGamepads();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count != _gamepad.Length)
        {
            UpdateConnectedGamepads();
        }
    }

    /// <summary>
    /// ゲームパッドのと番号を紐づけ
    /// </summary>
    void UpdateConnectedGamepads()
    {
        // 位置初期化させる
        Array.Clear(_gamepad, _gamepad.Length, _gamepad.Length);
        // _gamepad の数だけサイズを調整する
        Array.Resize(ref _gamepad, 0);
        // 配列に変更
        _gamepad = Gamepad.all.ToArray();
        // 要素の数だけ走る
        for (int i = 0; i < _gamepad.Length; i++)
        {
            // 配列に要素を追加
            Gamepads.Add(i + 1, _gamepad[i]);
            Debug.Log(Gamepads[i + 1]);
        }
    }
}
