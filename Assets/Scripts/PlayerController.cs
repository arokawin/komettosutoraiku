using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck; // 足元に設置するEmptyオブジェクト
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D rigidbody2D; // Rigidbody2Dコンポーネントへの参照

    private float xSpeed; // X方向移動速度
    private bool isGrounded;
    public Transform[] hanten;
    public Transform[] modoru;

    public float flipTriggerDistance = 0.2f; //近づいたら反応

    private bool isFlipped = false; // 反転状態を管理

    private SpriteRenderer spriteRenderer;

    public Sprite normalSprite;   // 通常の見た目
    public Sprite flippedSprite;  // 反転時の見た目

    private float flipCooldown = 1.0f; // 反転してから再反転までの時間
    private float lastFlipTime = -999f;

    private int HantenIndex = 0; // 現在の反転地点インデックス
    private int ModoruIndex = 0; // 現在の戻る地点インデックス

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
        // Flipの判定

        // 反転ポイントに近いかチェック
        foreach (Transform point in hanten)
        {
            if (!isFlipped && Vector2.Distance(transform.position, point.position) < flipTriggerDistance)
            {
                Flip();
                lastFlipTime = Time.time;
                return;
            }
        }

        // 戻りポイントに近いかチェック
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
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, isFlipped ? -jumpPower : jumpPower); //今の重力方向にジャンプ
        }
    }


    private void FixedUpdate()
    {
        // 移動速度ベクトルを現在値から取得
        Vector2 velocity = rigidbody2D.velocity;
        // X方向の速度を入力から決定
        velocity.x = xSpeed;

        // 計算した移動速度ベクトルをRigidbody2Dに反映
        rigidbody2D.velocity = velocity;
    }

    private void Flip()
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
            rigidbody2D.gravityScale *= -1;

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
            rigidbody2D.gravityScale *= -1;

            //位置を元に戻す
            Vector3 newPosition = transform.position;
            newPosition.y = -newPosition.y; // Y座標元に戻す
            transform.position = newPosition;


        }
    }
}
