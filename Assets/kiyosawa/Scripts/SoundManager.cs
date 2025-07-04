using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGMType
{
    BGM1,
    BGM2,
    BGM3,
    Null
}

// シリアライズ化
[System.Serializable]
struct BGMData
{
    public BGMType Type;
    public AudioClip Clip;
    [Range(0, 1)]
    public float Volume;
    public bool Loop;
}
public enum SEType
{
    SE1,
    SE2,
    SE3,
    SE4,
    SE5,
    Null
}

[System.Serializable]
struct SEData
{
    public SEType Type;
    public AudioClip Clip;
    [Range(0, 1)]
    public float Volume;
    public bool Loop;
}

public class SoundManager : MonoBehaviour
{
        // 関数のクラスを Instance化

        private static SoundManager instance;
        public static SoundManager Instance { get => instance; }
        //　ゲーム内で再生するBGMのリスト
        [SerializeField]
        private List<BGMData> bgmDateList = new List<BGMData>();

        [SerializeField]
        private List<SEData> seDataList = new List<SEData>();

        [SerializeField]
        private AudioSource bgmSource = null;

        [SerializeField]
        private AudioSource seSource = null;
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            { instance = this; }
            else return;
            DontDestroyOnLoad(this.gameObject);
            PlayBgm(BGMType.BGM3);
    }

        // Update is called once per frame
        void Update()
        {

        }
        // BGMの再生
        public void PlayBgm(BGMType type)
        {
            if (type == BGMType.Null) return;
            var bgm = bgmDateList[(int)type];
            bgmSource.clip = bgm.Clip;
            bgmSource.volume = bgm.Volume;
            bgmSource.loop = bgm.Loop;
            bgmSource.Play();
        }
        public void StopBgm()
        {
            bgmSource.Stop();
        }

        public void PlaySe(SEType type)
        {
            if (type == SEType.Null) return;
            var se = seDataList[(int)type];
            seSource.clip = se.Clip;
            seSource.volume = se.Volume;
            seSource.PlayOneShot(se.Clip);
        }
        // サウンドループ再生
        public void PlayLoopSe(SEType type)
        {
            var se = seDataList[(int)type];
            seSource.clip = se.Clip;
            seSource.volume = se.Volume;
            seSource.loop = se.Loop;
            seSource.Play();
        }
        public void StopLoopBgm()
        { 
            seSource.Stop();
        }

        public AudioSource PassAudioSource()
        {
            return seSource;
        }
    
}
