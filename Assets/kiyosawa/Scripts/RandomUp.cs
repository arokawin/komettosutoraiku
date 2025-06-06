using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUp : MonoBehaviour
{
    [SerializeField] private GameObject inseki1;
    [SerializeField] private GameObject inseki2;
    [SerializeField] private GameObject inseki3;
    [SerializeField] private GameObject inseki4;

    void Start()
    {
        // åJÇËï‘Çµèàóù
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UpRight", 2f, 4f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UpLeft", 3f, 3f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UnderLeft", 2f, 4f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UnderRight", 3f, 3f);
    }

    void Randomkougeki_UpRight()
    {
        Instantiate(inseki1,new Vector2(Random.Range(-9.4f,13f),10f),
            transform.rotation);
    }
    void Randomkougeki_UpLeft()
    {
        Instantiate(inseki2, new Vector2(Random.Range(-9.4f, 13f),10f),
            transform.rotation);
    }
    void Randomkougeki_UnderLeft()
    {
        Instantiate(inseki3, new Vector2(Random.Range(9.4f, -13f), -10f),
            transform.rotation);
    }
    void Randomkougeki_UnderRight()
    {
        Instantiate(inseki4, new Vector2(Random.Range(9.4f, -13f), -10f),
            transform.rotation);
    }

}
