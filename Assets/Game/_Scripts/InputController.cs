using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool CanResize { get; set; } = true;
    void Awake()
    {
        EventsManager.Instance.OnGameWin += () => this.enabled = false;
        EventsManager.Instance.OnGameLoose += () => this.enabled = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanResize)
        {
            EventsManager.Instance.OnInputPressed?.Invoke();
        }
        if (Input.GetMouseButtonUp(0) && CanResize)
        {
            EventsManager.Instance.OnInputReleased?.Invoke();
        }
    }
}
