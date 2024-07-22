using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField]
    GameObject cam;

    private void OnTriggerEnter(Collider other) {
        foreach(GameObject camera in GameObject.FindGameObjectsWithTag("RoomCamera")) {
            camera.SetActive(false);
        }
        cam.SetActive(true);
    }
}
