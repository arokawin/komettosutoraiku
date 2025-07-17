using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField,Header("�e�̑��x")]
    private float speed;

    private Vector2 direction;


    private static readonly Vector2 SpriteDir = new Vector2(-1f, -1f).normalized;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Vector2.SignedAngle(SpriteDir, direction);

        transform.rotation = Quaternion.Euler(0f, 0f, angle + 45);
    }

    void Update()
    {

        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
            Destroy(gameObject);
    }
}