using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Meteorile
{
    inseki1,
    inseki2,
    inseki3,
    inseki4,
    Null
}

[System.Serializable]
struct Meteo
{
    public float Speed;
    public float Direction;
    public GameObject inseki;
}
public class RandomUp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject inseki1;
    [SerializeField] private GameObject inseki2;
    [SerializeField] private GameObject inseki3;
    [SerializeField] private GameObject inseki4;
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;
    [SerializeField] private List<Meteo> meteorilesList = new List<Meteo>();

    

    void Start()
    {
        // ŒJ‚è•Ô‚µˆ—
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UpRight", 2f, 4f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UpLeft", 3f, 3f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UnderLeft", 2f, 4f);
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("Randomkougeki_UnderRight", 3f, 3f);
    }
    private void Update()
    {
        // è¦Î‚Ì•ûŒü‚Æ‘¬‚³‚Ìİ’è
        //transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        //transform.Translate(0f, _direction * Time.deltaTime, 0f);
    }

    void Randomkougeki_UpRight()
    {
        if (gameManager.GetComponent<GameManager>().gameEnd == true) return;
        Instantiate(inseki1,new Vector2(Random.Range(-9.4f,13f),10f),
            transform.rotation);
    }
    void Randomkougeki_UpLeft()
    {
        if (gameManager.GetComponent<GameManager>().gameEnd == true) return;
        Instantiate(inseki2, new Vector2(Random.Range(-9.4f, 13f),10f),
            transform.rotation);
    }
    void Randomkougeki_UnderLeft()
    {
        if (gameManager.GetComponent<GameManager>().gameEnd == true) return;
        Instantiate(inseki3, new Vector2(Random.Range(9.4f, -13f), -10f),
            transform.rotation);
    }
    void Randomkougeki_UnderRight()
    {
        if (gameManager.GetComponent<GameManager>().gameEnd == true) return;
        Instantiate(inseki4, new Vector2(Random.Range(9.4f, -13f), -10f),
            transform.rotation);
    }

}
