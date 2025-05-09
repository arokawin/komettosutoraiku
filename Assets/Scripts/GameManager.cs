using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private GameObject WinP1;
    [SerializeField] private GameObject WinP2;

    // Start is called before the first frame update
    void Start()
    {

        WinnerPanel.SetActive(false);
        WinP2.SetActive(false);
        WinP1.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Player1.GetComponent<PlayerController1>().HP <= 0 || Player2.GetComponent<PlayerController1>().HP <= 0)
        {
            WinnerPanel.SetActive(true);
            if (Player1.GetComponent<PlayerController1>().HP <= 0)
            {
                WinP1.SetActive(true);
            }
            else if(Player2.GetComponent<PlayerController1>().HP <= 0)
            {
                WinP2.SetActive(true);
            }
        }
    }
}
