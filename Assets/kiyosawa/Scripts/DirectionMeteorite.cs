using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMeteorite : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;

    void Update()
    {
        // 覐΂̕����Ƒ����̐ݒ�
        transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        transform.Translate(0f, _direction * Time.deltaTime, 0f);
    }
}
