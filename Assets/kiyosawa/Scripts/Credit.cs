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
        // クレジットの表示
        button.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySe(SEType.SE2);
            isActive = !isActive;
            imageGameobject.SetActive(isActive);
        });
    }
}
