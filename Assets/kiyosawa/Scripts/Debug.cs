using UnityEngine;
using UnityEngine.InputSystem;

public class GetDevicesExample : MonoBehaviour
{
    private void Start()
    {
        // �f�o�C�X�ꗗ���擾
        foreach (var device in InputSystem.devices)
        {
            // �f�o�C�X�������O�o��
            Debug.Log(device.name);
        }
    }
}