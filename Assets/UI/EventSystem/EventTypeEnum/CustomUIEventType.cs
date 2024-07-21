using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUIEvents
{

    //Types of delegates that can be used inside the customEventSystem
    public delegate void SingleReturnDelegate(EventArgs args);

    public delegate void FullReturnDelegate(string id, EventArgs args);

    /// <summary>
    /// Enum that Defines the different types of events that are present in the game
    /// </summary>
    //Used primerily by editor scripts and by the UIEventManager to know how many EventHolders to Instance
    public enum UIEventTypes
    {
        None = 0,
        NamedEvent = 1
    }

    //Extended Event Types Section
    //Here is where each event type is defined. They should be their own classed that extend the c# EventArgs class

    public class NamedEventInfo : EventArgs
    {
        public string NamedEventID;

        public NamedEventInfo (string eventID)
        {
            NamedEventID = eventID;
        }
    }

}