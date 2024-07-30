using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUIEvents;
using CustomUIEvents.Listeners;
using System;
using SceneBlocks;

public class MainMenuListener : BaseEventListener
{
    public override void UIEventFiredFromType(string id, EventArgs eventData)
    {
        var namedEvent = (NamedEventInfo)eventData;

        switch (namedEvent.NamedEventID)
        {
            case "StartGame":
                SceneBlockManager.Instance.StartCoroutine(SceneBlockManager.Instance.ChangeScene(SceneBlockEnum.DayOne));
                //SceneBlockManager.Instance.ChangeSceneBlock(SceneBlockEnum.DayOne);
                break;
        }
    }
}
