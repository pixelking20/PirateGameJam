using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUIEvents;
using CustomUIEvents.Triggers;

public class BasicNamedButton : BaseUITrigger
{

    [SerializeField]
    public string NamedEvent;

    private UIEventTypes UIEventType = UIEventTypes.NamedEvent;

    public void Start()
    {
        var dataPacket = new NamedEventInfo(NamedEvent);
        base.SetEventData(NamedEvent, UIEventType, dataPacket);
    }
    public void Clicked()
    {
        base.TriggerEvent();
    }
}
