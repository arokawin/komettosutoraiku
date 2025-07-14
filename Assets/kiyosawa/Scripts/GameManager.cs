using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();
    [SerializeField] private List<PlayerController> PlayerControllers = new List<PlayerController>();
    public List<GaugeController> GaugesList = new List<GaugeController>();
    [SerializeField] private List<GameObject> Round = new List<GameObject>();
    [SerializeField] private List<Rigidbody2D> PlayerRigidbody2D = new List<Rigidbody2D>();


    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject WinP1;
    [SerializeField] private GameObject WinP2;
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private float CountDown = 3.0f;
    [SerializeField] private ChangeSceneGame sceneGame;
    [SerializeField] private List<Vector3> PlayersStPosList = new List<Vector3>();
   

    private int RoundCount = 0;
    private int PlayerP1WinCount = 0;
    private int PlayerP2WinCount = 0;
    private int[] LifeCounts = { 2, 2 };
    private bool gameEnd = false;
    private float currentCountDown;
    private bool isCountingDown = false;
    public bool GameEnd => gameEnd;
    private static GameManager instance;

    public FadeManager fadeManager;



    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
   

    // Start is called before the first frame update
     void Start()
    {
        for (int i = 0; PlayersList.Count > i; i++)
        {
            PlayerControllers.Add(PlayersList[i].GetComponent<PlayerController>());
            PlayerRigidbody2D.Add(PlayersList[i].GetComponent<Rigidbody2D>());
        }


        // カウントダウン
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
     void Update()
    {
        // // var Sum = PlayerControllers[0].WinCount + PlayerControllers[1].WinCount;

        // // リザルト表記
        //// if (PlayerControllers[0].HP <= 0 || PlayerControllers[1].HP <= 0)
        // {
        //     GameEnd  = true;

        //     StartCoroutine(sceneGame.GetComponent<ChangeSceneGame>().FadeIn());

        //     // Winner表示
        //     if (PlayerControllers[0].WinCount >= 2 || PlayerControllers[1].WinCount >= 2)
        //     {
        //         WinnerPanel.SetActive(true);
        //         if (PlayerControllers[0].HP <= 0)
        //         {
        //             WinP1.SetActive(true);
        //         }
        //         else if (PlayerControllers[1].HP <= 0)
        //         {
        //             WinP2.SetActive(true);
        //         }
        //     }

        //     // プレイ設定のリセット
        //     if (PlayerControllers[0].WinCount < 2 || PlayerControllers[1].WinCount < 2)
        //     {
        //         for (int i = 0; PlayerControllers.Count > i; i++)
        //         {
        //             PlayerControllers[i].HP = 1;
        //             PlayersList[i].transform.position = PlayersStPosList[i];
        //             GaugesList[i].UpdateGauge(0, 5);
        //             PlayerControllers[i].ctTime = 0;
        //         }
        //         //Player1.HP = 1;
        //         //Player2.HP = 1;

        //         //Player1.transform.position = Player1StPos;
        //         //Player2.transform.position = Player2StPos;

        //         //gauge1.UpdateGauge(0, 5);
        //         //Player1.ctTime = 0;
        //         //gauge2.UpdateGauge(0, 5);
        //         //Player2.ctTime = 0;

        //         currentCountDown = CountDown;
        //         isCountingDown = true;

        //         CountText.gameObject.SetActive(isCountingDown);
        //         CountText.gameObject.SetActive(true);

        //         //Round[Sum - 1].SetActive(false);
        //         //Round[Sum].SetActive(true);

        //         StartCoroutine(sceneGame.FadeOut());
        //     }

        StartCountDown();

    }



    public async void StartCountDown()
    {
        // カウントダウン
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


    public void SudLifeCount(int playerNum)
    {
        LifeCounts[playerNum]--;
        //await NextRound();
    }

    // Winner表示
    public async Task NextRound()
    {
        gameEnd = true;
        RoundCount++;
        await fadeManager.FadeOut();
        int LifeNum = Array.IndexOf(LifeCounts, 0);

        if (LifeNum == 0)
        {
            await fadeManager.FadeIn();
            WinnerPanel.SetActive(true);
            WinP2.SetActive(true);
        }
        else if (LifeNum == 1)
        {
            await fadeManager.FadeIn();
            WinnerPanel.SetActive(true);
            WinP1.SetActive(true);
        }
        else
        {
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                GaugesList[i].UpdateGauge(0, 5);
                PlayerControllers[i].ResetPlayer();
            }
            //Player1.HP = 1;
            //Player2.HP = 1;

            //Player1.transform.position = Player1StPos;
            //Player2.transform.position = Player2StPos;

            //gauge1.UpdateGauge(0, 5);
            //Player1.ctTime = 0;
            //gauge2.UpdateGauge(0, 5);
            //Player2.ctTime = 0;

            currentCountDown = CountDown;
            isCountingDown = true;

            CountText.gameObject.SetActive(isCountingDown);
            CountText.gameObject.SetActive(true);

           // var animator = GetComponent<Animator>();
           // animator.Play("idle");

            Round[RoundCount -1].SetActive(false);
            Round[RoundCount].SetActive(true);

            await fadeManager.FadeIn();
            //sceneGame.FadeOut();

            gameEnd = false;
            isCountingDown = true;
        }
    }
}
