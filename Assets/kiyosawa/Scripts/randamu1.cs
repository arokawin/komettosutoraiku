using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randamu1 : MonoBehaviour
{
    public GameObject inseki;
    // Start is called before the first frame update
    void Start()
    {
        // åJÇËï‘Çµèàóù
        InvokeRepeating("Randamukougeki", 2f, 4f);
        InvokeRepeating("Randamukougeki", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Randamukougeki()
    {
        Instantiate(inseki,new Vector2(Random.Range(9.4f, -13f),transform.position.y),
            transform.rotation);
    }

}
