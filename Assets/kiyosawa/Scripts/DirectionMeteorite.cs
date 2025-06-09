using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMeteorite : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        // if (gameManager.GetComponent<GameManager>().gameEnd == true) return;
        // è¦Î‚Ì•ûŒü‚Æ‘¬‚³‚Ìİ’è
        transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        transform.Translate(0f, _direction * Time.deltaTime, 0f);
    }
}
