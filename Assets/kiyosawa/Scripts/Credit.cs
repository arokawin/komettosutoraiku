using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    [SerializeField] GameObject imageGameobject;
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        bool isActive = false;
        button.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySe(SEType.SE2);
            isActive = !isActive;
            imageGameobject.SetActive(isActive);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
