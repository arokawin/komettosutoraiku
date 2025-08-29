using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();
    [SerializeField] private List<PlayerController> PlayerControllers = new List<PlayerController>();
                     public  List<GaugeController> GaugesList = new List<GaugeController>();
    [SerializeField] private List<GameObject> Round = new List<GameObject>();
    [SerializeField] private List<Rigidbody2D> PlayerRigidbody2D = new List<Rigidbody2D>();
    [SerializeField] private List<Animator> PlayerAnimations = new List<Animator>();
    [SerializeField] private List<Image> RoundStar1 = new List<Image>();
    [SerializeField] private List<Image> RoundStar2 = new List<Image>();
    [SerializeField] private List<Vector3> PlayersStPosList = new List<Vector3>();

    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject Winner1;
    [SerializeField] private GameObject Winner2;
    [SerializeField] private GameObject WinP1;
    [SerializeField] private GameObject WinP2;
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private float CountDown = 3.0f;
    [SerializeField] private ChangeSceneGame sceneGame;
    [SerializeField] private Sprite Win1;
    [SerializeField] private Sprite Win2;
    

    private int RoundCount = 0;
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
            PlayerAnimations.Add(PlayersList[i].GetComponent<Animator>());
        }


        // カウントダウン
        currentCountDown = CountDown;
        isCountingDown = true;

        gameEnd = false;
        SoundManager.Instance.PlayBgm(BGMType.BGM1);

        WinnerPanel.SetActive(false);
        Winner2.SetActive(false);
        Winner1.SetActive(false);
        WinP1.SetActive(false);
        WinP2.SetActive(false); 

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
        //             Winner1.SetActive(true);
        //         }
        //         else if (PlayerControllers[1].HP <= 0)
        //         {
        //             Winner2.SetActive(true);
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
            switch(RoundCount)
            {
                case 0:
                CountText.text = "Round1";
                    break;

                case 1:
                    CountText.text = "Round2";
                    break;

                case 2:
                    CountText.text = "Round3";
                    break;
            }
            
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
        StarRound();
    }

    public void StarRound()
    {
        int LifeNum = LifeCounts [0];

        int LifeNum2 = LifeCounts[1];

        int RoundNum = RoundCount;

        if (RoundNum == 0)
        {
            if (LifeNum == 2)
            {
                WinP1.SetActive(true);
                RoundStar1[0].sprite = Win1;
            }
            else
            {
                WinP2.SetActive(true);
                RoundStar2[0].sprite = Win2;
            }
        }

        if (RoundNum == 1)
        {
            if (LifeNum == 2)
            {
                WinP1.SetActive(true);
                RoundStar1[1].sprite = Win1;
            }
            //else if (LifeNum == 1)
            //{
            //    WinP1.SetActive(true);
            //    RoundStar1[1].sprite = Win;
            //}
            else if (LifeNum2 == 2)
            {
                WinP2.SetActive(true);
                RoundStar2[1].sprite = Win2;
            }
            else
            {
                WinP2.SetActive(true);
                RoundStar2[1].sprite = Win2;
            }
        }

        if (RoundNum == 2)
        {
            if (LifeNum == 1)
            {
                WinP1.SetActive(true);
                RoundStar1[1].sprite = Win1;
            }
            else
            {
                WinP2.SetActive(true);
                RoundStar2[1].sprite = Win2;
            }
        }

        //    LifeNum = Array.IndexOf(LifeCounts, 0);

        //    if (LifeNum == 0)
        //    {
        //        WinP2.SetActive(true);
        //        RoundStar2[1].sprite = Win;
        //    }
        //    else if (LifeNum == 1)
        //    {
        //        WinP1.SetActive(true);
        //        RoundStar1[1].sprite = Win;
        //    }
    }

    // Winner表示
    public async Task NextRound()
    {
        gameEnd = true;
        RoundCount++;
        int LifeNum = Array.IndexOf(LifeCounts, 0);

        //if (LifeNum == 0)
        //{
        //    WinP2.SetActive(true);
        //    RoundStar2[0].sprite = Win;
        //}
        //else if (LifeNum == 1)
        //{
        //    WinP1.SetActive(true);
        //    RoundStar1[0].sprite = Win;
        //}

        //
        //if (PlayersList[0])
        //{
        //    Debug.Log("2");
        //    WinP2.SetActive(true);
        //}
        //else
        //{
        //    Debug.Log("1");
        //    WinP1.SetActive(true);
        //}
        //
        //await Task.Delay(1000);

        await fadeManager.FadeOut();

        WinP1.SetActive(false);
        WinP2.SetActive(false);


        if (LifeNum == 0)
        {
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.PlayBgm(BGMType.BGM2);
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                PlayerControllers[i].ResetPlayer();
            }
            //RoundStar1[1].sprite = Win;
            await fadeManager.FadeIn();
            WinnerPanel.SetActive(true);
            Winner2.SetActive(true);
        }
        else if (LifeNum == 1)
        {
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.PlayBgm(BGMType.BGM2);
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                PlayerControllers[i].ResetPlayer();
            }
            //RoundStar2[1].sprite = Win;
            await fadeManager.FadeIn();
            WinnerPanel.SetActive(true);
            Winner1.SetActive(true);
        }
        else
        {
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                GaugesList[i].UpdateGauge(0, 5);
                PlayerControllers[i].ResetPlayer();
            }

            currentCountDown = CountDown;
            isCountingDown = true;

            CountText.gameObject.SetActive(isCountingDown);
            CountText.gameObject.SetActive(true);

            Round[RoundCount -1].SetActive(false);
            Round[RoundCount].SetActive(true);

            await fadeManager.FadeIn();
            //sceneGame.FadeOut();

            gameEnd = false;
            isCountingDown = true;
        }
    }
}
