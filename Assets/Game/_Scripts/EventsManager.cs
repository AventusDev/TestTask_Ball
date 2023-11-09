using UnityEngine;
using System;
public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    public Action OnInputPressed;
    public Action OnInputReleased;
    public Action OnDoorCanBeReached;
    public Action OnShortDistanceToDoor;
    public Action OnGameWin;
    public Action OnGameLoose;
    private void Awake()
    {
        Instance = this;
    }
}
