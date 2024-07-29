using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField]
    GameObject cam;

    public delegate void OnPlayerAreaChange(bool enter);
    public OnPlayerAreaChange onPlayerAreaChange;

    private void OnTriggerEnter(Collider other) {
        if (CheckIfPlayer(other))
        {
            foreach (GameObject camera in GameObject.FindGameObjectsWithTag("RoomCamera"))
            {
                camera.SetActive(false);
            }
            cam.SetActive(true);
            print("Player Entered Area");
            onPlayerAreaChange?.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(CheckIfPlayer(other))
        {
            print("Player Exited Area");
            onPlayerAreaChange?.Invoke(false);
        }
    }
    bool CheckIfPlayer(Collider collider)
    {
        return collider.TryGetComponent(out PlayerController pc);
    }
}
