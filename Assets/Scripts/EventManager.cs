using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public static Event GameStartEvent = new Event();

    public delegate void GameStartDelegate();

    public GameStartDelegate gameStartDelegate;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


}
