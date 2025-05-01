using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck; // �����ɐݒu����Empty�I�u�W�F�N�g
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D rigidbody2D; // Rigidbody2D�R���|�[�l���g�ւ̎Q��

    private float xSpeed; // X�����ړ����x
    private bool isGrounded;
    public Transform[] hanten;
    public Transform[] modoru;

    public float flipTriggerDistance = 0.2f; //�߂Â����甽��

    private bool isFlipped = false; // ���]��Ԃ��Ǘ�

    private SpriteRenderer spriteRenderer;

    public Sprite normalSprite;   // �ʏ�̌�����
    public Sprite flippedSprite;  // ���]���̌�����

    private float flipCooldown = 1.0f; // ���]���Ă���Ĕ��]�܂ł̎���
    private float lastFlipTime = -999f;

    private int HantenIndex = 0; // ���݂̔��]�n�_�C���f�b�N�X
    private int ModoruIndex = 0; // ���݂̖߂�n�_�C���f�b�N�X

    // Start is called before the first frame update
    void Start()
    {
        int maxLength = Mathf.Max(hanten.Length, modoru.Length);
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        JumpUpdate();
        if (Time.time - lastFlipTime < flipCooldown)
            return;
        // Flip�̔���

        // ���]�|�C���g�ɋ߂����`�F�b�N
        foreach (Transform point in hanten)
        {
            if (!isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                Flip();
                lastFlipTime = Time.time;
                return;
            }
        }

        // �߂�|�C���g�ɋ߂����`�F�b�N
        foreach (Transform point in modoru)
        {
            if (isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                Unflip();
                lastFlipTime = Time.time;
                return;
            }
        }
    }

    private void MoveUpdate()
    {
        float inputX = 0.0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputX = 1.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputX = -1.0f;
        }

        // ���]��ԂȂ���͕������t�ɂ���
        if (isFlipped)
        {
            inputX *= -1.0f;
        }

        xSpeed = inputX * 6.0f;
    }

    private void JumpUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, GroundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpPower = 5.0f;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, isFlipped ? -jumpPower : jumpPower); //���̏d�͕����ɃW�����v
        }
    }


    private void FixedUpdate()
    {
        // �ړ����x�x�N�g�������ݒl����擾
        Vector2 velocity = rigidbody2D.velocity;
        // X�����̑��x����͂��猈��
        velocity.x = xSpeed;

        // �v�Z�����ړ����x�x�N�g����Rigidbody2D�ɔ��f
        rigidbody2D.velocity = velocity;
    }

    private void Flip()
    {
        // �L�����N�^�[�𔽓]
        if (isFlipped) return;
        {
            isFlipped = true;

            // X����Y���̗����𔽓]
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x; // X�����]
            localScale.y = -localScale.y; // Y�����]�i�㉺���]�j
            transform.localScale = localScale;

            //�ʒu�𔽓]
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y���W���]
            transform.position = newPosition;

            // �d�͂𔽓]
            rigidbody2D.gravityScale *= -1;

        }
    }

    private void Unflip()
    {
        // ���]�����ɖ߂�
        if (!isFlipped) return;
        {
            isFlipped = false;

            // X����Y���̗��������ɖ߂�
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x; // X�����]�߂�
            localScale.y = -localScale.y; // Y�����]�߂�
            transform.localScale = localScale;

            // �d�͂����ɖ߂�
            rigidbody2D.gravityScale *= -1;

            //�ʒu�����ɖ߂�
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y���W���ɖ߂�
            transform.position = newPosition;


        }
    }
}
