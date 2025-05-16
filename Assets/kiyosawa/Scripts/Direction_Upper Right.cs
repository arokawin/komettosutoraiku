using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houkou : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _speed;
    [SerializeField] private float _direction;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        transform.Translate(0f, _direction * Time.deltaTime, 0f);
    }
}
