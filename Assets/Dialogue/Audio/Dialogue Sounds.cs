using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private AudioClip clip;
    private float timeElapsed = 0;

    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= .125) {
            int index = Random.Range(0, audioClips.Length);
            clip = audioClips[index];
            audioSource.clip = clip;
            audioSource.Play();
            timeElapsed = 0;
        }
    }

}
