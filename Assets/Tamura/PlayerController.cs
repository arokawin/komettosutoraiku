using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region　へんすう

    
    public Transform groundCheck; // 足元に設置するEmptyオブジェクト
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D rb2d; // Rigidbody2Dコンポーネントへの参照

    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    public Transform[] hanten;
    [SerializeField]
    public Transform[] modoru;
    [SerializeField, Header("移動速度")]
    private float xSpeed; // X方向移動速度
    [SerializeField, Header("ジャンプ力")]
    private float jumpPower;
    [SerializeField, Header("反転クールタイム")]
    public float flipTriggerDistance = 0.1f;

    [SerializeField, Header("リロード時間")]
    private float ammoCt;
    private int maxammo = 5; //最大弾数
    private int ammo = 0;  //今の弾数
    private float ctTime = 0f;
    private Vector2 shootDirection;
    private bool isGrounded;
    private Vector2 move;
    private bool jump;

    public bool isFlipped = false; // 反転状態を管理
    private Kometto input;

    private float flipCooldown = 0.5f; // 反転してから再反転までの時間
    private float lastFlipTime = -999f;

    private int HantenIndex = 0; // 現在の反転地点インデックス
    private int ModoruIndex = 0; // 現在の戻る地点インデックス

    #endregion

    public float HP;
    void Start()
    {
        int maxLength = Mathf.Max(hanten.Length, modoru.Length);
        rb2d = GetComponent<Rigidbody2D>();
        input = new Kometto();
        input?.Enable();
    }

<<<<<<< HEAD
=======
    private void OnDestroy()
    {
        input?.Disable();
    }

    // Update is called once per frame
>>>>>>> main
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, GroundLayer);
 
      transform.position += new Vector3(move.x, 0f, 0f) * xSpeed * Time.deltaTime;

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

                rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //今の重力方向にジャンプ
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("inseki")|| collision.gameObject.CompareTag("bullet1")|| collision.gameObject.CompareTag("bullet2"))
        {
            HP--;
        }

    }

    #region いんぷっとまねーじゃー


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

        // 反転状態なら入力方向を逆にする
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
            rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //今の重力方向にジャンプ
        }
    }

    #endregion

    private void FixedUpdate()
    {
        // 移動速度ベクトルを現在値から取得
        Vector2 velocity = rb2d.velocity;
        // X方向の速度を入力から決定
        velocity.x = xSpeed;

    }
}
