using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using UnityEngine.UI;


public class StirringMinigame : Minigame
{
    [SerializeField]
    Cauldron cauldron;

    Vector3 stirPoint;
    float currentRPSSample;
    bool mouseOver;
    [SerializeField]
    Text successTextBox,clockText;

    delegate void ReportCurrentRPS(float currentRPS);
    ReportCurrentRPS currentRPSReport;

    Coroutine detectCircleCoroutine, CalculateRPSCoroutine;

    [SerializeField]
    float minRPS=.5f, maxRPS=1.5f, maxOutsideRangeTime=3f;
    IndicatorBar indicatorBar;
    

    protected override void OnAwake()
    {

        cauldron.mouseOverStateChange += OnMouseOverCauldronStateChange;
        indicatorBar = GetComponentInChildren<IndicatorBar>();
        indicatorBar.Initialize();
        indicatorBar.SetGoodRange(minRPS, maxRPS);
    }
    protected override void OnPrepareMiniGame()
    {
        cauldron.SetPotionStatus(PotionStatus.Good);
        indicatorBar.SetShownValue(Mathf.Lerp(minRPS, maxRPS, .5f),0);
        clockText.text = miniGameTime.ToString();
    }
    protected override void OnMinigameLoad()
    {
        stirPoint = miniGameCamera.WorldToScreenPoint(cauldron.transform.position);
    }
    protected override IEnumerator RunGame()
    {
        StartGame();
        currentRPSReport += OnRPSReport;

        float timeElapsed = 0, RPS = Mathf.Lerp(minRPS, maxRPS, .5f), timeOutsideRange = 0;

        while (timeElapsed < miniGameTime)
        {
            clockText.text = (Mathf.Round((miniGameTime - timeElapsed)*10f)/10f).ToString();
            timeElapsed += Time.deltaTime;
            timer.SetTimer(miniGameTime, miniGameTime - timeElapsed);
            if (RPS < minRPS || RPS > maxRPS)
            {
                if (RPS < minRPS)
                {
                    cauldron.SetPotionStatus(PotionStatus.Slow);
                }
                else if (RPS > maxRPS)
                {
                    cauldron.SetPotionStatus(PotionStatus.Fast);
                }

                timeOutsideRange += Time.deltaTime;
                if (timeOutsideRange > maxOutsideRangeTime)
                {
                    MinigameEnd(false);
                    yield break;
                }
            }
            else
            {
                timeOutsideRange = Mathf.Clamp(timeOutsideRange - Time.deltaTime, 0, Mathf.Infinity);
                cauldron.SetPotionStatus(PotionStatus.Good);
            }
            yield return new WaitForEndOfFrame();
        }

        currentRPSReport -= OnRPSReport;

        if (RPS < minRPS || RPS > maxRPS)
        {
            MinigameEnd(false);
            yield break;
        }
        else
        {
            MinigameEnd(true);
            yield break;
        }

        void OnRPSReport(float rps)
        {
            RPS = rps;
            indicatorBar.SetShownValue(RPS, .25f);
        }
    }
    void StartGame()
    {
        detectCircleCoroutine = StartCoroutine(DetectCircles());
        CalculateRPSCoroutine = StartCoroutine(CalculateDynamicRPS());
    }
    void OnMouseOverCauldronStateChange(bool state)
    {
        mouseOver = state;
        currentRPSSample = 0;
    }

    #region RPSCalculations

    IEnumerator CalculateDynamicRPS()
    {
        int RPSSamplesLength = 10;
        float perfectRPS = Mathf.Lerp(minRPS, maxRPS, .5f);
        float[] RPSSamples = new float[RPSSamplesLength];
        for (int i = 0; i < RPSSamples.Length; i++)
        {
            RPSSamples[i] = perfectRPS;
        }
        int stillStrikes = 0;
        float previousSample = 0;
        while (true)
        {
            if (currentRPSSample == previousSample)
            {
                stillStrikes++;
                if (stillStrikes > RPSSamplesLength/2)
                {
                    RPSSamples = new float[RPSSamplesLength];
                }
            }
            else
            {
                stillStrikes = 0;
                AddRPSSample(ref RPSSamples, currentRPSSample);
            }

            previousSample = currentRPSSample;

            yield return new WaitForSeconds(.1f);
            currentRPSReport?.Invoke(CalculateRPSAverage(RPSSamples));
        }
    }
    float CalculateRPSAverage(float[] RPSSamples)
    {
        float RPSTotal = 0;
        for (int i = 0; i < RPSSamples.Length; i++)
        {
            RPSTotal += RPSSamples[i];
        }
        return RPSTotal / RPSSamples.Length;
    }
    float CalculateRPS(float circleFraction, float time)
    {
        return circleFraction / time;
    }
    void AddRPSSample(ref float[] RPSSamples, float sample)
    {
        for (int i = 0; i < RPSSamples.Length - 1; i++)
        {
            RPSSamples[i] = RPSSamples[i + 1];
        }
        RPSSamples[RPSSamples.Length - 1] = sample;
    }


    IEnumerator DetectCircles()
    {
        float minimumCirclePercentage = .25f;
        while (true)
        {
            float startTime = Time.time;
            yield return DetectFractionalCircle(minimumCirclePercentage);
            float endTime = Time.time;
            float timeTaken = endTime - startTime;
            float RPS = CalculateRPS(minimumCirclePercentage, timeTaken);
            currentRPSSample = RPS;
        }
    }
    IEnumerator DetectFractionalCircle(float circleFraction)
    {
        Vector3 previousVector = GetCurrentMouseVector();
        float storedCircles = 0;
        while (Mathf.Abs(storedCircles) < circleFraction)
        {
            if (mouseOver)
            {
                Vector3 newVector = GetCurrentMouseVector();
                float angleDif = Vector3.SignedAngle(previousVector, newVector, Vector3.forward);
                storedCircles += angleDif / 360f;
                previousVector = newVector;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    Vector3 GetCurrentMouseVector()
    {
        return Input.mousePosition - stirPoint;
    }

    #endregion

    protected override void OnMinigameEnd(bool success)
    {
        if (success)
        {
            successTextBox.text = "You win!";
        }
        else
        {
            successTextBox.text = "You got eat by dog";
        }
        
        StopCoroutine(detectCircleCoroutine);
        StopCoroutine(CalculateRPSCoroutine);
    }
    protected override IEnumerator WaitForGameEnd()
    {
        yield return new WaitForEndOfFrame();
        while (!InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
        {
            yield return new WaitForEndOfFrame();
        }
    }
    protected override void OnCloseMiniGame(bool success)
    {
        successTextBox.text = "";
    }
}
