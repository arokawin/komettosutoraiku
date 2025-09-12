using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteDestroy : MonoBehaviour
{
    [SerializeField] private GameObject randomuUp;
    // ƒJƒƒ‰ŠO‚És‚Á‚½‚Æ‚«íœ
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
