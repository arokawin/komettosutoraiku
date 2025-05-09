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
    private float xSpeed; // X方向移動速度
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform firepoint;
    [SerializeField, Header("弾のクールタイム")]
    private float ammoCt;
    private int maxammo = 5; //最大弾数
    private int ammo = 0;  //今の弾数
    private float ctTime = 0f;
    private Vector2 shootDirection;
    private bool isGrounded;
    public Transform[] hanten;
    public Transform[] modoru;
    private Vector2 move;
    private bool jump;

    public float flipTriggerDistance = 0.2f; //近づいたら反応

    public bool isFlipped = false; // 反転状態を管理

    private SpriteRenderer spriteRenderer;
    private Kometto input;

    //public Sprite normalSprite;   // 通常の見た目
    //public Sprite flippedSprite;  // 反転時の見た目

    private float flipCooldown = 0.5f; // 反転してから再反転までの時間
    private float lastFlipTime = -999f;

    private int HantenIndex = 0; // 現在の反転地点インデックス
    private int ModoruIndex = 0; // 現在の戻る地点インデックス

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
        // Flipの判定

        // 反転ポイントに近いかチェック
        /*foreach (Transform point in hanten)
        {
            if (!isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                //Flip();
                lastFlipTime = Time.time;
                return;
            }
        }

        // 戻りポイントに近いかチェック
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

                rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //今の重力方向にジャンプ
            }
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

        // 計算した移動速度ベクトルをRigidbody2Dに反映
        //rb2d.velocity = velocity;
    }

    /*private void Flip()
    {
        // キャラクターを反転
        if (isFlipped) return;
        {
            isFlipped = true;

            // X軸とY軸の両方を反転
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x; // X軸反転
            localScale.y = -localScale.y; // Y軸反転（上下反転）
            transform.localScale = localScale;

            //位置を反転
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y座標反転
            transform.position = newPosition;

            // 重力を反転
            rb2d.gravityScale *= -1;

        }
    }

    private void Unflip()
    {
        // 反転を元に戻す
        if (!isFlipped) return;
        {
            isFlipped = false;

            // X軸とY軸の両方を元に戻す
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x; // X軸反転戻し
            localScale.y = -localScale.y; // Y軸反転戻し
            transform.localScale = localScale;

            // 重力を元に戻す
            rb2d.gravityScale *= -1;

            //位置を元に戻す
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y座標元に戻す
            transform.position = newPosition;


        }
    }*/
}
