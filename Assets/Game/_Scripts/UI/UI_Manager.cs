using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _looseUI;
    void Awake()
    {
        EventsManager.Instance.OnGameWin += ShowWinUI;
        EventsManager.Instance.OnGameLoose += ShowLooseUI;
    }

    private void ShowLooseUI()
    {
        _looseUI.SetActive(true);
    }

    private void ShowWinUI()
    {
        _winUI.SetActive(true);
    }
}
