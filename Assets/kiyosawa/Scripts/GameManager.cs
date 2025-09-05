using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameManager : MonoBehaviour
{
    // Inspector�ɕ\�����邽��
    //�N���X�� Serializable ������
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
    // �R�����g������ GameObject �����X�g��
    [SerializeField] private List<GameObject> WinnerLogos = new List<GameObject>();
    //[SerializeField] private GameObject Winner1;
    //[SerializeField] private GameObject Winner2;
    // �R�����g������ GameObject �����X�g��
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
        // �e�v���C���[�̎擾
        for (int i = 0; PlayersList.Count > i; i++)
        {
            PlayerControllers.Add(PlayersList[i].GetComponent<PlayerController>());
            PlayerRigidbody2D.Add(PlayersList[i].GetComponent<Rigidbody2D>());
            PlayerAnimations.Add(PlayersList[i].GetComponent<Animator>());
        }


        // �J�E���g�_�E��
        currentCountDown = CountDown;
        isCountingDown = true;

        gameEnd = false;
        SoundManager.Instance.PlayBgm(BGMType.BGM1);

        WinnerPanel.SetActive(false);

        // ���X�g������ GameObject �����ׂĔ�\��
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
    /// �X�^�[�g�̃J�E���g�_�E��
    /// </summary>
    public async void StartCountDown()
    {
        // �J�E���g�_�E��
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
    /// ���s�̏���
    /// </summary>
    /// <param name="playerNum">�s�҂̃v���C���[</param>
    public void SudLifeCount(int playerNum)
    {
        if (gameEnd) return;
        LifeCounts[playerNum]--;
        // ���҂̔ԍ��� playerNum �ɓ����(1 - playerNum �Ŕs�҂̔ԍ����珟�҂̔ԍ��ɕύX)
        StarRound( playerNum, LifeCounts[playerNum]);
    }

    /// <summary>
    /// ���E���h�̏��҂̕\��
    /// </summary>
    /// <param name="winnerIndex">���҃v���C���[</param>
    /// <param name="setStarIndex">���E���h�擾�̕\��</param>
    public void StarRound(int winnerIndex, int setStarIndex)
    {
        Debug.Log(winnerIndex);
        // ���҂̕\��
        WinLogos[winnerIndex].SetActive(true);
        // ���҂��擾�����̃v���C���[�� RoundStar ��\��
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
        //    }�@*/
        
    }

    

    /// <summary>
    /// ���̃��E���h�̂��߂̏���
    /// </summary>
    /// <param name="winnerIndex">���҃v���C���[</param>
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

        // ���X�g������ GameObject �����ׂĔ�\��
        WinLogos.ForEach(logo => logo.SetActive(false));
        //WinP1.SetActive(false);
        //WinP2.SetActive(false);

        // ()�̒��ň�ł������𖞂����Ă����� true
        // Winner�\��
        if (LifeCounts.Any(life => life == 0))
        {
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.PlayBgm(BGMType.BGM2);
            for (int i = 0; PlayerControllers.Count > i; i++)
            {
                PlayerControllers[i].ResetPlayer();
            }
            WinnerPanel.SetActive(true);
            // �Q�[���̏��҂�\��
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