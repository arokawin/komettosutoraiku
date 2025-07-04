using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject WinP1;
    [SerializeField] private GameObject WinP2;
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private float CountDown = 3.0f;
    [SerializeField] private ChangeSceneGame sceneGame;
    private float currentCountDown;
    private bool isCountingDown = false;
    public bool gameEnd;
    public int P1WinCount = 0;
    public int P2WinCount = 0;

    [SerializeField] private List<GameObject> Round = new List<GameObject>();


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

        Round[0].SetActive(true);

    }

    // Update is called once per frame
     async void Update()
    {
        var Sum = P1WinCount + P2WinCount;

        if (Player1.GetComponent<PlayerController>().HP <= 0 || Player2.GetComponent<PlayerController>().HP <= 0)
        {
            gameEnd  = true;
            
            StartCoroutine(sceneGame.GetComponent<ChangeSceneGame>().FadeIn());


            if (P1WinCount >= 2 || P2WinCount >= 2)
            {
                WinnerPanel.SetActive(true);
                if (Player1.GetComponent<PlayerController>().HP <= 0)
                {
                    WinP1.SetActive(true);
                }
                else if (Player2.GetComponent<PlayerController>().HP <= 0)
                {
                    WinP2.SetActive(true);
                }
            }

            if (P1WinCount < 2 || P2WinCount < 2)
            {
                Player1.GetComponent<PlayerController>().HP = 1;
                Player2.GetComponent<PlayerController>().HP = 1;
                currentCountDown = CountDown;
                isCountingDown = true;
                CountText.gameObject.SetActive(isCountingDown);
                CountText.gameObject.SetActive(true);

                //Round[Sum - 1].SetActive(false);
                //Round[Sum].SetActive(true);

                StartCoroutine(sceneGame.GetComponent<ChangeSceneGame>().FadeOut());
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
