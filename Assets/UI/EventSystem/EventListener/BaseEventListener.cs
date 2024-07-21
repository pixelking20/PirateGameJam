using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUIEvents.Listeners
{
    public abstract class BaseEventListener : MonoBehaviour
    {
        [SerializeField]
        private string eventID;

        [SerializeField]
        private UIEventTypes eventType;

        public void Start()
        {
            if (string.IsNullOrEmpty(eventID)) {
                UIEventManager.Instance.SubscribeToEntireEvent(eventType, UIEventFiredFromType);
            }
            else
            {
                UIEventManager.Instance.SubscripeToIDedEvent(eventType, eventID, UIEventFiredFromID);
            }
        }

        //method used 
        public virtual void UIEventFiredFromType(string id, EventArgs eventData)
        {

        }

        public virtual void UIEventFiredFromID(EventArgs eventData)
        {

        }
    }
}
