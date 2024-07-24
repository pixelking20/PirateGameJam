using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DayProgress
{
    public class DayManager : MonoBehaviour
    {
        public int DayNumber
        {
            get;

            private set;
        }

        public static DayManager Instance
        {
            get;

            private set;
        }
        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("There is already a copy of DayManager");
                Destroy(this);
            }

            DayNumber = 0;
        }

        public void ForceSetDay(int  day)
        {
            DayNumber = day;
        }

        public void NextDay()
        {
            DayNumber++;
        }
    }
}
