using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWall : MonoBehaviour
{
    [SerializeField]
    private float coolTime;
    private float time;
    private bool flipCheck;
    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > coolTime) flipCheck = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {  
            var rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (!flipCheck) return;
            Flip(collision.transform, rb2d, player);
        }
    }

    private void Flip(Transform transform,Rigidbody2D rb2d,PlayerController PlayerCTL)
    {
        // Xé≤Ç∆Yé≤ÇÃóºï˚ÇîΩì]
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x; // Xé≤îΩì]
        localScale.y = -localScale.y; // Yé≤îΩì]Åiè„â∫îΩì]Åj
        transform.localScale = localScale;

        //à íuÇîΩì]
        Vector3 newPosition = transform.position;
        newPosition.x = -newPosition.x - 0.2f; 
        newPosition.y = -newPosition.y; // Yç¿ïWîΩì]
        transform.position = newPosition;

        // èdóÕÇîΩì]
        rb2d.gravityScale *= -1;

        PlayerCTL.isFlipped = !PlayerCTL.isFlipped;

        flipCheck = false;

        time = 0f;
    }

}

