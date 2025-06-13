using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject WinP1;
    [SerializeField] private GameObject WinP2;
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private float CountDown = 3.0f;
    private float currentCountDown;
    private bool isCountingDown = false;
    public bool gameEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        currentCountDown = CountDown;
        isCountingDown = true;

        gameEnd = false;
        SoundManager.Instance.PlayBgm(BGMType.BGM1);

        WinnerPanel.SetActive(false);
        WinP2.SetActive(false);
        WinP1.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
     async void Update()
    { 
        if (Player1.GetComponent<PlayerController>().HP <= 0 || Player2.GetComponent<PlayerController>().HP <= 0)
        {
           
            gameEnd  = true;
            

            WinnerPanel.SetActive(true);
            if (Player1.GetComponent<PlayerController>().HP <= 0)
            {
                WinP1.SetActive(true);
            }
            else if(Player2.GetComponent<PlayerController>().HP <= 0)
            {
                WinP2.SetActive(true);
            }
        }

        if (!isCountingDown) return;

        if (currentCountDown > 0)
        {
            gameEnd = true;
            currentCountDown -= Time.deltaTime;
            CountText.text = Mathf.Ceil(currentCountDown).ToString();
        }
        else
        {
            CountText.text = "Start";
            gameEnd = false;
            await Task.Delay(1000);
            CountText.gameObject.SetActive(false);
            isCountingDown = false;
        }
    }
}
