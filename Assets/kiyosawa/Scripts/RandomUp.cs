using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUp : MonoBehaviour
{
    [SerializeField] private GameObject inseki;
    [SerializeField] private GameObject _inseki;

    void Start()
    {
        // åJÇËï‘Çµèàóù
        InvokeRepeating("Randomkougeki_Right", 2f, 4f);
        InvokeRepeating("Randomkougeki_Left", 3f, 3f);
    }

    void Randomkougeki_Right()
    {
        Instantiate(inseki,new Vector2(Random.Range(-9.4f,13f),transform.position.y),
            transform.rotation);
    }
    void Randomkougeki_Left()
    {
        Instantiate(_inseki, new Vector2(Random.Range(-9.4f, 13f), transform.position.y),
            transform.rotation);
    }


}
