using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace CustomUIEvents
{
    public class EventHolder
    {
        private Dictionary<string, SingleReturnDelegate> EventDictionary;

        private FullReturnDelegate FullSubscriptionEvent;
        public EventHolder()
        {
            EventDictionary = new Dictionary<string, SingleReturnDelegate>();
        }

        //returns a bool so that the UIEventManager can know when to print a debug. the UIManager should be the one to print the debug since it will have access to which type of event trigger failed
        public bool TriggerEvent(string ID, EventArgs infoObject)
        {
            bool atLeastOneEventFired = false;

            if (FullSubscriptionEvent != null)
            {
                FullSubscriptionEvent.Invoke(ID, infoObject);
                atLeastOneEventFired = true;
            }


            if (EventDictionary.ContainsKey(ID))
            {
                EventDictionary[ID]?.Invoke(infoObject);
                atLeastOneEventFired = true;
            }

            return atLeastOneEventFired;
        }

        public void AddIDedEvent(string ID, SingleReturnDelegate eventRef)
        {
            

            if (EventDictionary.ContainsKey(ID))
            {
                EventDictionary[ID] += eventRef;
            }
            else
            {
                SingleReturnDelegate Handler = null;
                Handler += eventRef;
                EventDictionary.Add(ID, Handler);
            }
        }

        public void RemoveIDedEvent(string ID, SingleReturnDelegate eventRef)
        {
            if (EventDictionary.ContainsKey(ID))
            {
                EventDictionary[ID] -= eventRef;
            }
        }

        public void AddEntireEvent(FullReturnDelegate eventRef)
        {
            FullSubscriptionEvent += eventRef;
        }

        public void RemoveEntireEvent(FullReturnDelegate eventRef)
        {
            FullSubscriptionEvent -= eventRef;
        }

        public void ClearDelegates()
        {
            string[] keys = new string[EventDictionary.Keys.Count];
            EventDictionary.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                EventDictionary[keys[i]] = null;
            }
        }
    }
}

