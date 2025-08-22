using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteDestroy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject randomuUp;
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }

}
