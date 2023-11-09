using UnityEngine;

public class BallAbstract : MonoBehaviour
{
    [SerializeField] public float _changedSizePerTick;
    [SerializeField] public float _changedSizeSpeed;
    public bool CanScaling = false;
    public virtual void OnCollisionEnter(Collision other) { }
    public virtual void EventsSubscribe() { }
}
