using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    public GameObject dogHead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        print(player.transform.name);
    }

    private void Update() {
        dogHead.transform.LookAt(new Vector3(player.transform.position.x, dogHead.transform.position.y, player.transform.position.z));
        dogHead.transform.Rotate(dogHead.transform.rotation.x - 90, dogHead.transform.rotation.x + 90,0);
    }

}
