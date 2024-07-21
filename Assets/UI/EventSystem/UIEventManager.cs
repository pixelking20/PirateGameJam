using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUIEvents
{
    public class UIEventManager : MonoBehaviour
    {
        private static UIEventManager instance;

        private Dictionary<UIEventTypes, EventHolder> HolderDictionary;

        public static UIEventManager Instance
        {
            get
            {
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private EventHolder testHolder;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                Debug.LogError("There are more than one Instance of UI Event Manager inside the project");
                return;
            }

            Instance = this;

            HolderDictionary = new Dictionary<UIEventTypes, EventHolder>();

            Array eventTypes = Enum.GetValues(typeof(UIEventTypes));

            foreach (UIEventTypes type in eventTypes)
            {
                HolderDictionary.Add(type, new EventHolder());
            }
        }

        //Used to register to specific Events with a specific ID
        public void SubscripeToIDedEvent(UIEventTypes type, String ID, SingleReturnDelegate del)
        {
            HolderDictionary[type].AddIDedEvent(ID, del);
        }

        //Used to register to all Events of a specific type
        public void SubscribeToEntireEvent(UIEventTypes type, FullReturnDelegate del)
        {
            HolderDictionary[type].AddEntireEvent(del);
        }

        /// <summary>
        /// Unsubscribe from an event that the script subscribed to.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="del"></param>
        public void UnsubscribeFromIDedEvent(UIEventTypes type, String ID, SingleReturnDelegate del)
        {
            HolderDictionary[type].RemoveIDedEvent(ID, del);
        }

        public void UnsubscribeFromEntireEvent(UIEventTypes type, FullReturnDelegate del)
        {
            HolderDictionary[type].RemoveEntireEvent(del);
        }

        //method that should generally only be used by buttons to trigger a specific event to fire.
        public void TriggerUIEvent(string ID, UIEventTypes type,EventArgs eventInfo)
        {
            EventHolder eventHolder = HolderDictionary[type];

            Debug.Log($"event ID: {ID}, and type: {type} was triggered");
            
            if (eventHolder.TriggerEvent(ID, eventInfo))
            {
                return;
            }
            else
            {
                Debug.LogWarning($"There are no events of that type with the ID: {ID}");
            }
        }

        private void OnDestroy()
        {
            Array eventTypes = Enum.GetValues(typeof(UIEventTypes));

            foreach (UIEventTypes type in eventTypes)
            {
                HolderDictionary[type].ClearDelegates();
            }
        }
    }
}