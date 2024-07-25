using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class ChoppingMinigame : Minigame
{
    [SerializeField]
    float leadTime;
    [SerializeField]
    float[] chopTimes;
    [SerializeField]
    float graceTime;

    [SerializeField]
    Transform startConveyor, endConveyor;
    [SerializeField]
    float conveyorTime;
    [SerializeField]
    GameObject targetPrefab;

    Dictionary<float, Transform> chopTargets;
    protected override void OnAwake()
    {
        
    }

    protected override void OnCloseMiniGame(bool success)
    {
        DestroyAllTargets(chopTargets);
    }

    protected override void OnMinigameEnd(bool success)
    {
        
    }

    protected override IEnumerator RunGame()
    {

        List<float> chops = new List<float>();
        chopTargets = new Dictionary<float, Transform>();

        for (int i = 0;i< chopTimes.Length;i++)
        {
            chops.Add(chopTimes[i]);
            GameObject newTarget = Instantiate(targetPrefab);
            chopTargets.Add(chopTimes[i],newTarget.transform);
        }

        float timeElapsed = -(conveyorTime+leadTime);//Starts moving first note immediately: (-conveyorTime + chops[0] + graceTime)
        while (true)
        {
            float conveyorEndTime = timeElapsed - graceTime, conveyorStartTime = conveyorEndTime + conveyorTime;
            foreach (float chopTime in chops)
            {
                float timeTillChop = chopTime - timeElapsed;
                float ratio = Mathf.InverseLerp(conveyorStartTime, conveyorEndTime, chopTime);
                Transform target = chopTargets[chopTime];
                target.position = Vector3.Lerp(startConveyor.position, endConveyor.position, ratio);
            }

            if (timeElapsed > chops[0] + graceTime)
            {
                MinigameEnd(false);
                print("Miss!");
                yield break;
            }
            else if (InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
            {
                if(timeElapsed > (chops[0] - graceTime))
                {
                    print("Hit!");
                    Destroy(chopTargets[chops[0]].gameObject);
                    chopTargets.Remove(chops[0]);
                    chops.RemoveAt(0);
                    if (chops.Count == 0)
                    {
                        MinigameEnd(true);
                        yield break;
                    }
                }
                else
                {
                    MinigameEnd(false);
                    print("Miss!");
                    yield break;
                }
            }
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    void DestroyAllTargets(Dictionary<float, Transform> dictionary)
    {
        foreach (KeyValuePair<float, Transform> kvp in dictionary)
        {
            Destroy(kvp.Value.gameObject);
        }
    }
    protected override IEnumerator WaitForGameEnd()
    {
        yield return new WaitForEndOfFrame();
        while (!InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
