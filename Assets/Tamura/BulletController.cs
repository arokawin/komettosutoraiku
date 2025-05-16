using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField,Header("�e�̑��x")]
    private float speed;
    private Vector2 direction = Vector2.right;

    // �e�̌������O������ݒ�
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ��ʊO�ɏo����j��
    }
}
