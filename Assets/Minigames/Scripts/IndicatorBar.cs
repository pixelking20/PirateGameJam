using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorBar : MonoBehaviour
{
    [SerializeField]
    float minValue, maxValue, minGood, maxGood;
    Transform minHolder, maxHolder,valueIndicator,goodRange;
    float barMoveSpeed;

    delegate void GetNewValue(float value);
    GetNewValue getNewValue;

    public void Initialize()
    {
        minHolder = transform.Find("MinHolder");
        maxHolder = transform.Find("MaxHolder");
        valueIndicator = transform.Find("ValueIndicator");
        goodRange = transform.Find("GoodRange");
    }
    public void SetGoodRange(float min, float max)
    {
        minGood = min;
        maxGood = max;
        SetGoodRangeIndicator();
    }
    void SetGoodRangeIndicator()
    {
        float totalRange = maxValue - minValue;
        float goodScoreRange = maxGood - minGood;
        float ratio = goodScoreRange / totalRange;
        float middleGood = Mathf.Lerp(minGood, maxGood, .5f);
        goodRange.localScale = new Vector3(ratio, 1, 1);
        ratio = (middleGood - minValue) / (maxValue - minValue);
        goodRange.localPosition = Vector3.Lerp(minHolder.localPosition, maxHolder.localPosition, ratio);
    }
    public void SetShownValue(float value,float timeToMove)
    {
        StartCoroutine(MoveSlider(value, timeToMove));
    }
    Vector3 CalculateGoalPosition(float value)
    {
        float ratio = (value - minValue) / (maxValue - minValue);
        return Vector3.Lerp(minHolder.localPosition, maxHolder.localPosition, ratio);
    }
    IEnumerator MoveSlider(float goalValue,float timeToMove)
    {
        float timeElapsed = 0;
        Vector3 startPos = valueIndicator.localPosition,goalPos = CalculateGoalPosition(goalValue);

        while(timeElapsed < timeToMove)
        {
            valueIndicator.localPosition = Vector3.Lerp(startPos, goalPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        valueIndicator.localPosition = goalPos;
    }
}
