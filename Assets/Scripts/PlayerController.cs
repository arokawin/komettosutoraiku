using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region�@�ւ񂷂�

    
    public Transform groundCheck; // �����ɐݒu����Empty�I�u�W�F�N�g
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D rb2d; // Rigidbody2D�R���|�[�l���g�ւ̎Q��

    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private float xSpeed; // X�����ړ����x
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform firepoint;
    [SerializeField, Header("�e�̃N�[���^�C��")]
    private float ammoCt;
    private int maxammo = 5; //�ő�e��
    private int ammo = 0;  //���̒e��
    private float ctTime = 0f;
    private Vector2 shootDirection;
    private bool isGrounded;
    public Transform[] hanten;
    public Transform[] modoru;
    private Vector2 move;
    private bool jump;

    public float flipTriggerDistance = 0.2f; //�߂Â����甽��

    public bool isFlipped = false; // ���]��Ԃ��Ǘ�

    private SpriteRenderer spriteRenderer;
    private Kometto input;

    //public Sprite normalSprite;   // �ʏ�̌�����
    //public Sprite flippedSprite;  // ���]���̌�����

    private float flipCooldown = 0.5f; // ���]���Ă���Ĕ��]�܂ł̎���
    private float lastFlipTime = -999f;

    private int HantenIndex = 0; // ���݂̔��]�n�_�C���f�b�N�X
    private int ModoruIndex = 0; // ���݂̖߂�n�_�C���f�b�N�X

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        int maxLength = Mathf.Max(hanten.Length, modoru.Length);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = new Kometto();
        input?.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveUpdate();
        //JumpUpdate();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, GroundLayer);
        /*if (Time.time - lastFlipTime < flipCooldown)
            return;*/
        // Flip�̔���

        // ���]�|�C���g�ɋ߂����`�F�b�N
        /*foreach (Transform point in hanten)
        {
            if (!isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                //Flip();
                lastFlipTime = Time.time;
                return;
            }
        }

        // �߂�|�C���g�ɋ߂����`�F�b�N
        foreach (Transform point in modoru)
        {
            if (isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                //Unflip();
                lastFlipTime = Time.time;
                return;
            }
        }*/

      transform.position += new Vector3(move.x, 0f, 0f) * xSpeed * Time.deltaTime;
        //else transform.position += new Vector3(move.x, 0f, 0f) * xSpeed * Time.deltaTime;

        //if (isFlipped)
        //{
        //    xSpeed = -xSpeed;
        //}
        AmmoCount();
        AmmoUI();
    }


    public void AmmoCount()
    {
        if (ammo < maxammo)
        {
            ctTime += Time.deltaTime;

            if (ctTime >= ammoCt)
            {
                ammo++;
                ctTime = 0f;
            }
        }
    }

    public void AmmoUI()
    {
        ammoText.text = $"AMMO:{ammo}";
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started) return;
        if (ctx.performed)
        {
            move = ctx.ReadValue<Vector2>();
        }
        if (ctx.canceled)
        {
            move = Vector2.zero;
            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        shootDirection = ctx.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && shootDirection != Vector2.zero)
        {
            if (ammo > 0)
            {
                GameObject bullet = Instantiate(_bullet, firepoint.position, Quaternion.identity);
                bullet.GetComponent<BulletController>().SetDirection(shootDirection);
                ammo--;
            }

        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {   
            if (isGrounded && ctx.ReadValueAsButton())
            {

                rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //���̏d�͕����ɃW�����v
            }
        }
    }

    #region ����Ղ��Ƃ܂ˁ[����[

    
    public void MoveUpdate()
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
            rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //���̏d�͕����ɃW�����v
        }
    }

    #endregion

    private void FixedUpdate()
    {
        // �ړ����x�x�N�g�������ݒl����擾
        Vector2 velocity = rb2d.velocity;
        // X�����̑��x����͂��猈��
        velocity.x = xSpeed;

        // �v�Z�����ړ����x�x�N�g����Rigidbody2D�ɔ��f
        //rb2d.velocity = velocity;
    }

    /*private void Flip()
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
            rb2d.gravityScale *= -1;

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
            rb2d.gravityScale *= -1;

            //�ʒu�����ɖ߂�
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y���W���ɖ߂�
            transform.position = newPosition;


        }
    }*/
}
