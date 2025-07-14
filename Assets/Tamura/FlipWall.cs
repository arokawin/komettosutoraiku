using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWall : MonoBehaviour
{
    [SerializeField]
    private float coolTime;
    private float time;
    private bool flipCheck;
    [SerializeField]
    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flipCheck)
        {
            time += Time.deltaTime;
            if (time > coolTime)
            {
                flipCheck = true;
                time = 0f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            var player = collision.gameObject.GetComponent<PlayerController>();
            var flipX = collision.gameObject.GetComponent<SpriteRenderer>();
            if (!flipCheck) return;
            Flip(collision.transform, rb2d, player, flipX);
        }
    }

    private void Flip(Transform transform, Rigidbody2D rb2d, PlayerController PlayerCTL, SpriteRenderer sprite)
    {
        //sprite.flipX = sprite.flipX;
        //sprite.flipY = !sprite.flipY;

        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        localScale.y = -localScale.y;
        transform.localScale = localScale;
        sprite.flipX = !sprite.flipX;


        Vector3 newPosition = transform.position;
        newPosition.x = -newPosition.x;
        newPosition.y = -newPosition.y;

        float xDir = Mathf.Sign(transform.position.x); 
        float yDir = Mathf.Sign(rb2d.gravityScale);    
        newPosition += new Vector3(_offset.x * xDir, _offset.y * yDir, 0);
        transform.position = newPosition;

        rb2d.gravityScale *= -1;

        PlayerCTL.isFlipped = !PlayerCTL.isFlipped;

        flipCheck = false;
        time = 0f;
    }
}
   

