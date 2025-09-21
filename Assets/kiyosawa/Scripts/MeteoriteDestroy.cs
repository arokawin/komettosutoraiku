using UnityEngine;

public class MeteoriteDestroy : MonoBehaviour
{
    // ƒJƒƒ‰ŠO‚És‚Á‚½‚Æ‚«íœ
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
