using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houkou : MonoBehaviour
{
    // Start is called before the first frame update

    float speed;
    void Start()
    {
        speed = -5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }
}
