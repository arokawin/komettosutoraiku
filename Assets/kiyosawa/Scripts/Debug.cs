using UnityEngine;
using UnityEngine.InputSystem;

public class GetDevicesExample : MonoBehaviour
{
    private void Start()
    {
        // デバイス一覧を取得
        foreach (var device in InputSystem.devices)
        {
            // デバイス名をログ出力
            Debug.Log(device.name);
        }
    }
}