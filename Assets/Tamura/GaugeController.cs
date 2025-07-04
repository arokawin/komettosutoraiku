using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GaugeController : MonoBehaviour
{
 [SerializeField] protected Image _gaugeImage;    //ゲージとして使うImage

    //ゲージの見た目を設定
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmountを更新
    }
}
