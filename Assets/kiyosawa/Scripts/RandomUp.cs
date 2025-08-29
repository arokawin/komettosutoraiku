using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class RandomUp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject inseki;

    [SerializeField]
    private Transform rangeA;
    [SerializeField] 
    private Transform rangeB;

    

   public  void Start()
    {
        // 繰り返し処理
        SoundManager.Instance.PlaySe(SEType.SE2);
        //await Task.Delay(5000);
        InvokeRepeating("Randomkougeki_UpRight", 1f, 2f);
        
    }
    private void Update()
    {
        if (GameManager.Instance.GameEnd == true)
        {
            // 子オブジェクトに生成
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

   


    async Task Randomkougeki_UpRight()
    {
        if (GameManager.Instance.isCountingDown == true)
        {
            await Task.Delay(5000);
        }
        if (GameManager.Instance.GameEnd == true) return;
        var UpDown = Random.Range(0, 2);
        switch (UpDown)
        {
            case 0:
                var meteo = Instantiate(inseki, new Vector2(Random.Range(rangeA.position.x, rangeB.position.x), -10f), Quaternion.Euler(0, 0, 90f), transform);
                var rb = meteo.GetComponent <Rigidbody2D>();
                var Xvec = Random.Range(-10f, 10f);
                var Yvec = Random.Range(1f, 10f);
                var radY = Mathf.Acos(Xvec / Mathf.Sqrt(Mathf.Pow(Xvec, 2) + Mathf.Pow(Yvec, 2))) * Mathf.Rad2Deg;
                // if (meteo.transform.position.x >= 0.01f) meteo.transform.localEulerAngles = new Vector3(0, 0, -90f);
                // else meteo.transform.localEulerAngles = new Vector3(0, 0, 180f);
                meteo.transform.localEulerAngles = new Vector3(0, 0, meteo.transform.localEulerAngles.z + radY);
                rb.velocity = new Vector3(Xvec, Yvec, 0);

                break; 
            case 1:
                meteo = Instantiate(inseki, new Vector2(Random.Range(rangeA.position.x, rangeB.position.x), 10f), Quaternion.Euler(0, 0, 90f), transform);
                rb = meteo.GetComponent<Rigidbody2D>();
                Xvec = Random.Range(-10f, 10f);
                Yvec = Random.Range(-1f, -10f);
                radY = Mathf.Acos(Xvec / Mathf.Sqrt(Mathf.Pow(Xvec, 2) + Mathf.Pow(Yvec, 2))) * Mathf.Rad2Deg;
                // if (meteo.transform.position.x >= 0.01f) meteo.transform.localEulerAngles = new Vector3(0, 0, -90f);
                // else meteo.transform.localEulerAngles = new Vector3(0, 0, 180f);
                meteo.transform.localEulerAngles = new Vector3(0, 0, meteo.transform.localEulerAngles.z - radY);
                rb.velocity = new Vector3(Xvec, Yvec, 0);
                break;
        }
    }
}
