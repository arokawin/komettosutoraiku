using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUnder : MonoBehaviour
{
    [SerializeField] private GameObject inseki;
    [SerializeField] private GameObject _inseki;

    void Start()
    {
        // åJÇËï‘Çµèàóù
        InvokeRepeating("Randomkougeki_Left", 2f, 4f);
        InvokeRepeating("Randomkougeki_Right", 3f, 3f);
    }

    void Randomkougeki_Left()
    {
        Instantiate(inseki,new Vector2(Random.Range(9.4f, -13f),transform.position.y),
            transform.rotation);
    }
    void Randomkougeki_Right()
    {
        Instantiate(_inseki, new Vector2(Random.Range(9.4f, -13f), transform.position.y),
            transform.rotation);
    }

}
