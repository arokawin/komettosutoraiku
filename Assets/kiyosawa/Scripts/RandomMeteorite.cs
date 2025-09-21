using System.Threading.Tasks;
using UnityEngine;

public class RandomMeteorite : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject meteorile;

    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;

   public  void Start()
    {
        // 繰り返し処理
        SoundManager.Instance.PlaySe(SEType.SE2);
        InvokeRepeating("MeteorileGenerate", 1f, 2f);        
    }
    private void Update()
    {
        if (GameManager.Instance.GameEnd)
        {
            // 子オブジェクトに削除
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// 隕石のランダム生成
    /// </summary>
    /// <returns></returns>
    async Task MeteorileGenerate()
    {
        // カウントダウン開始から５秒後に生成
        if (GameManager.Instance.isCountingDown)
        {
            await Task.Delay(5000);
        }
        if (GameManager.Instance.GameEnd) return;
        var UpDown = Random.Range(0, 2);
        switch (UpDown)
        {
            // 上からランダム生成
            case 0:
                // 隕石の生成、生成する場所のランダム、オブジェクトの回転
                var meteo = Instantiate(meteorile, new Vector2(Random.Range(rangeA.position.x, rangeB.position.x), -10f), Quaternion.Euler(0, 0, 90f), transform);
                var rb = meteo.GetComponent <Rigidbody2D>();
                var Xvec = Random.Range(-10f, 10f);
                var Yvec = Random.Range(1f, 10f);
                // 進んでいる方向を計算
                var radY = Mathf.Acos(Xvec / Mathf.Sqrt(Mathf.Pow(Xvec, 2) + Mathf.Pow(Yvec, 2))) * Mathf.Rad2Deg;
                // if (meteo.transform.position.x >= 0.01f) meteo.transform.localEulerAngles = new Vector3(0, 0, -90f);
                // else meteo.transform.localEulerAngles = new Vector3(0, 0, 180f);
                // 計算した方向に回転
                meteo.transform.localEulerAngles = new Vector3(0, 0, meteo.transform.localEulerAngles.z + radY);
                rb.velocity = new Vector3(Xvec, Yvec, 0);
                break; 
            // 下からランダム生成
            case 1:
                // 隕石の生成、生成する場所のランダム、オブジェクトの回転
                meteo = Instantiate(meteorile, new Vector2(Random.Range(rangeA.position.x, rangeB.position.x), 10f), Quaternion.Euler(0, 0, 90f), transform);
                rb = meteo.GetComponent<Rigidbody2D>();
                Xvec = Random.Range(-10f, 10f);
                Yvec = Random.Range(-1f, -10f);
                // 進んでいる方向を計算
                radY = Mathf.Acos(Xvec / Mathf.Sqrt(Mathf.Pow(Xvec, 2) + Mathf.Pow(Yvec, 2))) * Mathf.Rad2Deg;
                // if (meteo.transform.position.x >= 0.01f) meteo.transform.localEulerAngles = new Vector3(0, 0, -90f);
                // else meteo.transform.localEulerAngles = new Vector3(0, 0, 180f);
                // 計算した方向に回転
                meteo.transform.localEulerAngles = new Vector3(0, 0, meteo.transform.localEulerAngles.z - radY);
                rb.velocity = new Vector3(Xvec, Yvec, 0);
                break;
        }
    }
}
