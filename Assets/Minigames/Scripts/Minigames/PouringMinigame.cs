using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class PouringMinigame : Minigame
{
    [SerializeField]
    float fillGoal, fillGrace,minTilt,maxFlowRate,tiltSpeed,totalVolume;

    [SerializeField]
    Transform pouredLiquid, pouringLiquid,container;
    Transform beaker,liquidExit,containerTop,containerBottom,goalMarker;
    Quaternion uprightBeaker, tiltedBeaker;
    protected override void OnAwake()
    {
        beaker = tool.transform;
        liquidExit = beaker.Find("LiquidExit");
        uprightBeaker = beaker.localRotation;
        tiltedBeaker = uprightBeaker * Quaternion.AngleAxis(45f, Vector3.right);
        containerTop = container.Find("Top");
        containerBottom = container.Find("Bottom");
        goalMarker = container.Find("GoalMarker");
        SetupGoalArea();
    }

    protected override void OnPrepareMiniGame()
    {
        SetPouringLiquidSize(0);
        SetPouredLiquidSize(0);
        SetBeakerTilt(minTilt);
    }
    protected override void OnMinigameLoad()
    {

    }
    protected override void OnCloseMiniGame(bool success)
    {

    }

    protected override void OnMinigameEnd(bool success)
    {

    }

    protected override IEnumerator RunGame()
    {
        float currentFill = 0, currentTilt=minTilt, timeElapsed = 0,fillMin = fillGoal -fillGrace,fillMax = fillGoal + fillGrace;

        while(timeElapsed < miniGameTime)
        {
            AdjustTilt(ref currentTilt);
            SetBeakerTilt(currentTilt);
            currentFill += GetFillRate(currentTilt);
            SetPouredLiquidSize(currentFill);
            if(currentFill > fillMax)
            {
                MinigameEnd(false);
                yield break;
            }
            timeElapsed += Time.deltaTime;
            timer.SetTimer(miniGameTime, miniGameTime - timeElapsed);
            yield return new WaitForEndOfFrame();

            if (currentTilt == 0 && CheckWinCondition())
            {
                MinigameEnd(true);
                yield break;
            }
        }

        MinigameEnd(CheckWinCondition());
        yield break;


        bool CheckWinCondition()
        {
            return !(currentFill < fillMin || currentFill > fillMax);
        }
    }

    
    protected override IEnumerator WaitForGameEnd()
    {
        SetPouringLiquidSize(0);
        yield return new WaitForEndOfFrame();
        while (!InputHandler.GetInput(Inputs.Interact,ButtonInfo.Press))
        {
            yield return new WaitForEndOfFrame();
        }
    }
    void AdjustTilt(ref float tilt)
    {
        float axis = (-InputHandler.GetXAxis()) + InputHandler.GetYAxis();
        axis = Mathf.Clamp(axis, -1, 1);
        tilt += tiltSpeed * axis * Time.deltaTime;
        tilt = Mathf.Clamp(tilt,minTilt,1);
        SetPouringLiquidSize(tilt);
    }
    float GetFillRate(float tilt)
    {
        return Mathf.Lerp(0, maxFlowRate, tilt) * Time.deltaTime;
    }
    void SetupGoalArea()
    {
        goalMarker.transform.localScale = new Vector3(1,(fillGrace*2f)/totalVolume, 1);
        goalMarker.position = Vector3.Lerp(containerBottom.position, containerTop.position, Mathf.InverseLerp(0, totalVolume, fillGoal));
    }
    void SetBeakerTilt(float currentTilt)
    {
        beaker.transform.localRotation = Quaternion.Lerp(uprightBeaker, tiltedBeaker, currentTilt);
    }
    void SetPouringLiquidSize(float percentageFull)
    {
        pouringLiquid.position = liquidExit.position;
        pouringLiquid.localScale = new Vector3(percentageFull, 1, percentageFull);
    }
    void SetPouredLiquidSize(float accumulatedLiquid)
    {
        pouredLiquid.localScale = new Vector3(1,Mathf.InverseLerp(0,totalVolume,accumulatedLiquid), 1);
    }

}
