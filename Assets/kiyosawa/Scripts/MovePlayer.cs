        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePlayer : MonoBehaviour
{
    public float speed = 3f;
    private float playerSpeed;
    Rigidbody2D newrigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        newrigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // ���L�[���������獶�����֐i��
        if (Input.GetKey(KeyCode.A)) playerSpeed = -speed;
        // �E�L�[����������E�����֐i��
        else if (Input.GetKey(KeyCode.D)) playerSpeed = speed;
        // ���������Ȃ�������~�܂�
        else playerSpeed = 0;
        newrigidbody2D.velocity = new Vector2(playerSpeed, newrigidbody2D.velocity.y);
    }
}