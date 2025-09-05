using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameManager : MonoBehaviour
{
    // Inspectorに表示するため
    //クラスを Serializable 化する
    [System.Serializable]
    private class RoundStarPack
    {
        public List<Image> RoundStar;
    }

    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();
    [SerializeField] private List<PlayerController> PlayerControllers = new List<PlayerController>();
    public List<GaugeController> GaugesList = new List<GaugeController>();
    [SerializeField] private List<GameObject> Round = new List<GameObject>();
    [SerializeField] private List<Rigidbody2D> PlayerRigidbody2D = new List<Rigidbody2D>();
    [SerializeField] private List<Animator> PlayerAnimations = new List<Animator>();
    [SerializeField] private List<RoundStarPack> RoundStars = new List<RoundStarPack>();
    //[SerializeField] private List<Image> RoundStar1 = new List<Image>();
    //[SerializeField] private List<Image> RoundStar2 = new List<Image>();
    [SerializeField] private List<Vector3> PlayersStPosList = new List<Vector3>();

    [SerializeField] private GameObject WinnerPanel;
    // コメント化した GameObject をリスト化
    [SerializeField] private List<GameObject> WinnerLogos = new List<GameObject>();
    //[SerializeField] private GameObject Winner1;
    //[SerializeField] private GameObject Winner2;
    // コメント化した GameObject をリスト化
    [SerializeField] private List<GameObject> WinLogos = new List<GameObject>();
    //[SerializeField] private GameObject WinP1;
    //[SerializeField] private GameObject WinP2;
    [SerializeField] private Image CountImage;
    [SerializeField] private Sprite Round1Sp;
    [SerializeField] private Sprite Round2Sp;
    [SerializeField] private Sprite Round3Sp;
    [SerializeField] private Sprite StartSp;
    [SerializeField] private float CountDown = 3.0f;
    [SerializeField] private ChangeSceneGame sceneGame;
    [SerializeField] private List<Sprite> Wins = new List<Sprite>();


    private int RoundCount = 0;
    private int[] LifeCounts = { 2, 2 };
    private bool gameEnd = false;
    private float currentCountDown;
    public bool isCountingDown = false;
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
        // 各プレイヤーの取得
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

        // リスト化した GameObject をすべて非表示
        WinnerLogos.ForEach(logo => logo.SetActive(false));
        WinLogos.ForEach(logo => logo.SetActive(false));
        //Winner2.SetActive(false);
        //Winner1.SetActive(false);
        //WinP1.SetActive(false);
        //WinP2.SetActive(false);

        Time.timeScale = 1;

        Round[0].SetActive(true);



    }

    void Update()
    {
        StartCountDown();
    }



    /// <summary>
    /// スタートのカウントダウン
    /// </summary>
    public async void StartCountDown()
    {
        // カウントダウン
        if (!isCountingDown) return;

        if (currentCountDown > 0)
        {
            gameEnd = true;
            currentCountDown -= Time.deltaTime;
            switch (RoundCount)
            {
                case 0:
                    CountImage.sprite = Round1Sp;
                    break;
                case 1:
                    CountImage.sprite = Round2Sp;
                    break;
                case 2:
                    CountImage.sprite = Round3Sp;
                    break;
            }
            CountImage.gameObject.SetActive(true);

        }
        else
        {
            CountImage.sprite = StartSp;
            gameEnd = false;
            await Task.Delay(1000);
            CountImage.gameObject.SetActive(false);
            isCountingDown = false;
        }
    }

    /// <summary>
    /// 勝敗の処理
    /// </summary>
    /// <param name="playerNum">敗者のプレイヤー</param>
    public void SudLifeCount(int playerNum)
    {
        if (gameEnd) return;
        LifeCounts[playerNum]--;
        // 勝者の番号を playerNum に入れる(1 - playerNum で敗者の番号から勝者の番号に変更)
        StarRound( playerNum, LifeCounts[playerNum]);
    }

    /// <summary>
    /// ラウンドの勝者の表示
    /// </summary>
    /// <param name="winnerIndex">勝者プレイヤー</param>
    /// <param name="setStarIndex">ラウンド取得の表示</param>
    public void StarRound(int winnerIndex, int setStarIndex)
    {
        Debug.Log(winnerIndex);
        // 勝者の表示
        WinLogos[winnerIndex].SetActive(true);
        // 勝者を取得しそのプレイヤーの RoundStar を表示
        RoundStars[winnerIndex].RoundStar[setStarIndex].sprite = Wins[winnerIndex];
        /*
        int LifeNum = LifeCounts[0];

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
        //    }　*/
        
    }

    

    /// <summary>
    /// 次のラウンドのための処理
    /// </summary>
    /// <param name="winnerIndex">勝者プレイヤー</param>
    /// <returns></returns>
    public async Task NextRound(int winnerIndex)
    {
        if (gameEnd) return;
        gameEnd = true;
        RoundCount++;
        /*
        //int LifeNum = Array.IndexOf(LifeCounts, 0);

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
        */

        await fadeManager.FadeOut();

        // リスト化した GameObject をすべて非表示
        WinLogos.ForEach(logo => logo.SetActive(false));
        //WinP1.SetActive(false);
        //WinP2.SetActive(false);

        // ()の中で一つでも条件を満たせていたら true
        // Winner表示
        if (LifeCounts.Any(life => life == 0))
        {
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.PlayBgm(BGMType.BGM2);
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                PlayerControllers[i].ResetPlayer();
            }
            WinnerPanel.SetActive(true);
            // ゲームの勝者を表示
            WinnerLogos[winnerIndex].SetActive(true);
            //Winner2.SetActive(true);
            await fadeManager.FadeIn();

        }
       /* else if (LifeNum == 1)
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
        }*/
        else
        {
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                GaugesList[i].UpdateGauge(0, 5);
                PlayerControllers[i].ResetPlayer();
            }

            currentCountDown = CountDown;
            isCountingDown = true;

            CountImage.gameObject.SetActive(isCountingDown);
            CountImage.gameObject.SetActive(true);

            Round[RoundCount - 1].SetActive(false);
            Round[RoundCount].SetActive(true);

            await fadeManager.FadeIn();
            //sceneGame.FadeOut();

            gameEnd = false;
            isCountingDown = true;
        }
    }
}