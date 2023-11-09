using DG.Tweening;
using UnityEngine;

public class MainBall : BallAbstract
{
    [SerializeField] private Transform _exitDoor;
    Tween jumpTween;
    private void Awake()
    {
        EventsSubscribe();
    }
    public override void EventsSubscribe()
    {
        EventsManager.Instance.OnDoorCanBeReached += JumpToTheDoor;
        EventsManager.Instance.OnInputPressed += () => CanScaling = true;
        EventsManager.Instance.OnInputReleased += () => CanScaling = false;
    }
    public void Update()
    {
        Debug.Log("UPD");
        if (CanScaling)
        {
            Debug.Log("UPD2");
            if (this.transform.localScale.x > 0.2f)
            {
                Debug.Log("UPD3");
                this.transform.localScale -= new Vector3(_changedSizePerTick, _changedSizePerTick, _changedSizePerTick) * _changedSizeSpeed * Time.deltaTime;
            }
            else
            {
                EventsManager.Instance.OnGameLoose?.Invoke();
                Debug.Log("LOOSE");
            }
        }
    }
    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {

            EventsManager.Instance.OnGameLoose?.Invoke();
            jumpTween.Kill();
            transform.DOScale(Vector3.zero, 2f)
            .OnKill(() =>
                {
                    Destroy(this.gameObject);
                })
            .Play();
        }
    }
    private void JumpToTheDoor()
    {
        jumpTween = transform.DOJump(_exitDoor.position, 1.25f, 12, 8f)
        .OnUpdate(() =>
        {
            if (Vector3.Distance(transform.position, _exitDoor.transform.position) < 5f)
            {
                EventsManager.Instance.OnShortDistanceToDoor?.Invoke();
            }
        })
        .OnComplete(() =>
        {
            EventsManager.Instance.OnGameWin?.Invoke();
            this.gameObject.SetActive(false);
        });

        jumpTween.Play();
    }
}
