using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SecondBall : BallAbstract, IInteractable
{
    [SerializeField] private float _ballImpulsePower;
    [SerializeField] private ParticleSystem _explosionParticle;
    private Collider _collider;
    private Rigidbody _rbSecondBall;
    private float explodeRadius;
    private Vector3 collisionPoint; //
    private bool InShootMode = false;
    private Vector3 initialPosition;
    private void Awake()
    {
        initialPosition = transform.localPosition;
        SetupComponents();
        EventsSubscribe();
    }
    private void SetupComponents()
    {
        _collider = GetComponent<Collider>();
        _rbSecondBall = GetComponent<Rigidbody>();
    }
    public override void EventsSubscribe()
    {
        EventsManager.Instance.OnInputPressed += () => CanScaling = true;
        EventsManager.Instance.OnInputReleased += () => CanScaling = false;
        EventsManager.Instance.OnInputReleased += Shoot;
        EventsManager.Instance.OnGameLoose += () => this.gameObject.SetActive(false);
    }
    public void Update()
    {
        if (CanScaling == true && InShootMode == false)
        {
            IncreaseScale();
        }
    }
    private void IncreaseScale()
    {
        transform.localScale += new Vector3(_changedSizePerTick, _changedSizePerTick, _changedSizePerTick) * _changedSizeSpeed * Time.deltaTime;
    }
    public void Shoot()
    {
        if (InShootMode == false)
        {
            InShootMode = true;
            transform.DOShakePosition(0.9f, 0.12f).
                OnKill(
                    () =>
                    {
                        transform.DOLocalMoveZ(transform.localPosition.z - 0.22f, 0.4f).OnKill(() =>
                            {
                                _rbSecondBall.AddForce(Vector3.forward * _ballImpulsePower, ForceMode.Impulse);
                            })
                        .Play();
                    }
                ).Play();
        }
    }
    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            PlayVFX(other.GetContact(0).point);
            Interact();
        }
    }

    private void PlayVFX(Vector3 contactPoint)
    {
        _explosionParticle.transform.position = contactPoint;
        _explosionParticle.transform.localScale = this.transform.localScale;
        _explosionParticle.Play();
    }

    public void Interact()
    {
        _collider.enabled = false;
        explodeRadius = 2.5f * transform.localScale.x;

        Collider[] obstaclesColliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider obstacle in obstaclesColliders)
        {
            if (obstacle.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        SecondBallSetup();
        _collider.enabled = true;
        InShootMode = false;
    }
    public void SecondBallSetup()
    {
        _rbSecondBall.velocity = Vector3.zero;
        transform.localScale = Vector3.zero;
        transform.localPosition = initialPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collisionPoint, explodeRadius);
    }
}
