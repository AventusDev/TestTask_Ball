using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator _animator;
    [SerializeField] private string _openDoorAnimation;
    [SerializeField] private ParticleSystem _portalParticle;
    void Start()
    {
        _animator = GetComponent<Animator>();
        EventsManager.Instance.OnShortDistanceToDoor += OpenDoor;
    }
    public void Interact()
    {
        EventsManager.Instance.OnDoorCanBeReached?.Invoke();
        _portalParticle.Play();
    }
    private void OpenDoor()
    {
        _animator.SetTrigger(_openDoorAnimation);
    }
}