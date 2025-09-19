using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleComet : MonoBehaviour
{
    [SerializeField] Vector2 _endPos;
    [SerializeField] private GameObject inseki;
    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Comet", 1f, 1f);
    }

    private void Comet()
    {
        // 隕石の生成、生成する場所のランダム、オブジェクトの回転
        var meteo = Instantiate(inseki, new Vector2(Random.Range(rangeA.position.x, rangeB.position.x), Random.Range(rangeA.position.y, rangeB.position.y)), Quaternion.Euler(0, 0, 90f));
        var rb = meteo.GetComponent<Rigidbody2D>();
        var Xvec = -5f;
        var Yvec = -5f;
        var radY = Mathf.Acos(Xvec / Mathf.Sqrt(Mathf.Pow(Xvec, 2) + Mathf.Pow(Yvec, 2))) * Mathf.Rad2Deg;
        meteo.transform.localEulerAngles = new Vector3(0, 0, meteo.transform.localEulerAngles.z - radY);
        rb.velocity = new Vector3(Xvec, Yvec, 0);
    }
}
