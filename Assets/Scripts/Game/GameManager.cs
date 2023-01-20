using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float timeEachTick;

    private float timeUntilTick;

    public static Action onTick;
    public static Action onNewTick;

    #region Properties
    public float TimeUntilTick
    {
        get { return timeUntilTick; }
    }
    public float TimeEachTick
    {
        get { return timeEachTick; }
    }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        timeUntilTick = timeEachTick;
    }

    private void Update()
    {
        timeUntilTick -= Time.deltaTime;
        if (timeUntilTick <= 0f)
        {
            DoTick();
        }
    }

    public void DoTick()
    {
        onTick?.Invoke();
        onNewTick?.Invoke();
        timeUntilTick = timeEachTick;
    }
}
