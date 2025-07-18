using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region　へんすう

    [SerializeField]
    private GameManager gameManager;
    public Transform groundCheck; // 足元に設置するEmptyオブジェクト
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private Rigidbody2D rb2d; // Rigidbody2Dコンポーネントへの参照

    [SerializeField]
    private GameObject aimSpPrefab;
    private GameObject aimSpInstance;
    [SerializeField]
    private GameObject star; 
    [SerializeField] private Transform stars; 
    private List<GameObject> starList = new List<GameObject>(); 
    [SerializeField] 
    private GaugeController gaugeController;
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
    public float ctTime = 0f;
    private Vector2 shootDirection;
    private bool isGrounded;
    public Transform[] hanten;
    public Transform[] modoru;
    public Vector2 move;
    private bool jump;

    public float flipTriggerDistance = 0.2f; //近づいたら反応

    public bool isFlipped = false; // 反転状態を管理

    private SpriteRenderer spriteRenderer;
    private Kometto input;

    private Animator anim;

    private Vector3 FirstPos;
    private Vector3 FirstScale;
    private bool FirstFlipX;
    private bool FirstFlipY;
    private float FirstGravity;
    private Vector3 FirstGra;
    private bool FirstFlipped;
    private float MaxHP = 1;
    private float FirstCtTime = 0f;

    private PlayerInput PlInput;

    #endregion

    public float HP;
    void Start()
    {
        int maxLength = Mathf.Max(hanten.Length, modoru.Length);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlInput = GetComponent<PlayerInput>(); 
        input = new Kometto();
        input?.Enable();

        aimSpInstance = Instantiate(aimSpPrefab, transform.position, Quaternion.identity);
        aimSpInstance.SetActive(false);
        anim = GetComponent<Animator>();

        FirstPos = transform.position;
        FirstFlipX = spriteRenderer.flipX;
        FirstFlipY = spriteRenderer.flipY;
        FirstGravity = rb2d.gravityScale;
        FirstGra = rb2d.velocity;
        FirstScale = transform.localScale;
        FirstFlipped = isFlipped;
    }

    private void OnDestroy()
    {
        input?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().GameEnd == true) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, GroundLayer);

        // transform.position += new Vector3(move.x, 0f, 0f) * xSpeed * Time.deltaTime;

        
        anim.SetBool("Move", move.x != 0);
        if (move.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (move.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        AmmoCount();

        if (shootDirection.magnitude < 0.1f)
        {
            aimSpInstance.SetActive(false);
            return;
        }

        aimSpInstance.SetActive(true);

        Vector3 pos = transform.position + (Vector3)(shootDirection.normalized * 1f);
        pos.z = -1f;
        aimSpInstance.transform.position = pos;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        aimSpInstance.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        var aim = aimSpInstance.GetComponent<BulletController>();
        if (aim != null)
        {
            aim.SetDirection(shootDirection);
        }

    }

    public void ResetPlayer()
    {
        transform.position = FirstPos;
        transform.localScale = FirstScale;
        rb2d.gravityScale = FirstGravity;
        rb2d.velocity = FirstGra;
        spriteRenderer.flipX = FirstFlipX;
        spriteRenderer.flipY = FirstFlipY;
        HP = MaxHP;
        ctTime = FirstCtTime;
        ammo = 0;
        isFlipped = FirstFlipped;
        //Vector2 velocity = rb2d.velocity;
        //velocity.x = 0f;
        
        move.x = 0;
        anim.SetBool("Move", move.x != 0);
        // 子オブジェクトの全削除
        foreach (Transform n in stars.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
        starList.Clear();

    }
    public void AmmoCount()
    {
        if (ammo < maxammo)
        {
            ctTime += Time.deltaTime;
            gaugeController.UpdateGauge(ctTime, ammoCt);

            if (ctTime >= ammoCt)
            {
                ammo++;
                ctTime = 0f;
                gaugeController.UpdateGauge(0, ammoCt);
                AmmoUI();
            }
        }
    }

    public void AmmoUI()
    {
        if (starList.Count >= maxammo) return;
        GameObject newStar = Instantiate(star, stars);
        starList.Add(newStar);

    }



    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started) return;
        if (gameManager.GetComponent<GameManager>().GameEnd == true) return;
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
                if (gameManager.GetComponent<GameManager>().GameEnd == true) return;
                GameObject bullet = Instantiate(_bullet, firepoint.position, Quaternion.identity);
                bullet.GetComponent<BulletController>().SetDirection(shootDirection);
                ammo--;
                if (starList.Count > 0)
                {
                    GameObject lastStar = starList[starList.Count - 1];
                    starList.RemoveAt(starList.Count - 1);
                    Destroy(lastStar);
                }
                SoundManager.Instance.PlaySe(SEType.SE5);
            }

        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {   
            if (gameManager.GetComponent<GameManager>().GameEnd == true) return;
           
            rb2d.velocity = new Vector2(rb2d.velocity.x, isFlipped ? -jumpPower : jumpPower); //今の重力方向にジャンプ
            SoundManager.Instance.PlaySe(SEType.SE4);
        }
    }

    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("inseki")|| collider.gameObject.CompareTag("bullet1")|| collider.gameObject.CompareTag("bullet2"))
        {
            //SoundManager.Instance.PlayBgm(BGMType.BGM2);
            GameManager.Instance.SudLifeCount(PlInput.user.index);
            await GameManager.Instance.NextRound();
            anim.SetBool("Move", move.x != 0);
            //Destroy(collider.gameObject);
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
        if (gameManager.GetComponent<GameManager>().GameEnd == true) return;
        Vector2 velocity = rb2d.velocity;
        velocity.x = move.x * xSpeed;
        rb2d.velocity = velocity;
    }


}
