using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSelfDeleting : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip clip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip _clip)
    {
        clip = _clip;
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (clip && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
