using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool CanScaling = false;
    [SerializeField] private float _changedSizePerTick;
    [SerializeField] private float _changedSizeSpeed;

    void Awake()
    {
        EventsManager.Instance.OnInputPressed += () => CanScaling = true;
        EventsManager.Instance.OnInputReleased += () => CanScaling = false;
    }
    public void Update()
    {
        if (CanScaling)
        {
            transform.localScale -= new Vector3(_changedSizePerTick, _changedSizePerTick, _changedSizePerTick) * _changedSizeSpeed * Time.deltaTime;
        }
    }
}
