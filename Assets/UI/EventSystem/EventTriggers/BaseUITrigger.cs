using CustomUIEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUIEvents.Triggers
{
    public class BaseUITrigger : MonoBehaviour
    {
        protected string eventID;
        protected EventArgs dataPacket;
        protected UIEventTypes eventType;

        protected void SetEventData(string ID, UIEventTypes type, EventArgs data)
        {
            eventID = ID;
            eventType = type;
            dataPacket = data;
        }

        protected void TriggerEvent()
        {
            UIEventManager.Instance.TriggerUIEvent(eventID, eventType, dataPacket);
        }
    }
}
