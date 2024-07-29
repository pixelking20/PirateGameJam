using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    protected Camera miniGameCamera;
    [SerializeField]
    protected float miniGameTime = 10f;

    public delegate void OnMinigameComplete(bool success);
    public OnMinigameComplete onMinigameComplete;

    public static bool minigameRunning;
    protected bool thisMiniGameRunning;

    protected Tool tool;
    bool toolPickedUp = false;

    protected Timer timer;
    private void Awake()
    {
        miniGameCamera = GetComponentInChildren<Camera>();
        tool = GetComponentInChildren<Tool>();
        tool.onPickup += OnToolPickup;
        tool.Initialize();
        timer = GetComponentInChildren<Timer>();
        timer.SetTimer(miniGameTime, miniGameTime);
        OnAwake();
    }
    protected abstract void OnAwake();
    public Camera GetCamera()
    {
        return miniGameCamera;
    }
    public void InitializeMiniGame()
    {
        if (!thisMiniGameRunning)
            StartCoroutine(MainCoroutine());
    }
    IEnumerator MainCoroutine()
    {
        minigameRunning = true;
        thisMiniGameRunning = true;
        PrepareMiniGame();
        yield return TransitionCurtains.Transition(false);

        miniGameCamera.enabled = true;

        yield return TransitionCurtains.Transition(true);
        MinigameLoad();
        yield return WaitForGameStart();
        yield return RunGame();
    }
    void PrepareMiniGame()
    {
        tool.ResetTool();
        timer.SetTimer(miniGameTime, miniGameTime);
        OnPrepareMiniGame();
    }
    protected abstract void OnPrepareMiniGame();
    void MinigameLoad()
    {
        tool.SetPickupable(true);
        OnMinigameLoad();
    }
    protected abstract void OnMinigameLoad();
    IEnumerator WaitForGameStart()
    {
        while (!toolPickedUp)
        {
            yield return new WaitForEndOfFrame();
        }
    }
    protected abstract IEnumerator RunGame();
    protected void MinigameEnd(bool success)
    {
        StartCoroutine(EndGameCoroutine(success));
    }
    IEnumerator EndGameCoroutine(bool success)
    {
        OnMinigameEnd(success);
        yield return WaitForGameEnd();
        yield return TransitionCurtains.Transition(false);

        miniGameCamera.enabled = false;
        CloseMiniGame(success);

        yield return TransitionCurtains.Transition(true);       
    }
    protected abstract void OnMinigameEnd(bool success);
    protected abstract IEnumerator WaitForGameEnd();
    void CloseMiniGame(bool success)
    {
        OnCloseMiniGame(success);
        minigameRunning = false;
        thisMiniGameRunning = false;
        onMinigameComplete?.Invoke(success);
        tool.ResetTool();
        tool.SetPickupable(false);
        toolPickedUp = false;
    }
    protected abstract void OnCloseMiniGame(bool success);
    protected void OnToolPickup()
    {
        toolPickedUp = true;
    }
    public bool GetMinigameRunning()
    {
        return thisMiniGameRunning;
    }
}
