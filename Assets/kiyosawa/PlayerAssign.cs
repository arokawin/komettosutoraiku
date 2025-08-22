using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Users;


public class PlayerAssign : MonoBehaviour
{
    [SerializeField]  List<GameObject> _playerList = new List<GameObject>();

    //private Player[] _players;

    public static int __playerIndex;

    private int _playerNum;

    private List<PlayerInput> _playerInputs = new List<PlayerInput> ();

    private Dictionary<int, GameObject> _numToPlayerObj = new Dictionary<int, GameObject> ();

    void Start()
    {
        Assign();
        //_players = GetComponentsInChildren<Player>();
    }

    /// <summary>
    /// 最初のプレイヤー出現処理
    /// </summary>
    void Assign()
    {
        //
        foreach (int key in DeviceManager.Instance.Gamepads.Keys)
        {
            // 取得した Input を要素に追加
          //  _playerInputs.Add(player.GetComponentInChildren<PlayerInput>());
            // プレイヤーとコントローラーの番号を紐づけ
          //_numToPlayerObj.Add(key, player);
        }
    }

}
