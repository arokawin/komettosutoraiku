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
    /// �ŏ��̃v���C���[�o������
    /// </summary>
    void Assign()
    {
        //
        foreach (int key in DeviceManager.Instance.Gamepads.Keys)
        {
            // �擾���� Input ��v�f�ɒǉ�
          //  _playerInputs.Add(player.GetComponentInChildren<PlayerInput>());
            // �v���C���[�ƃR���g���[���[�̔ԍ���R�Â�
          //_numToPlayerObj.Add(key, player);
        }
    }

}
