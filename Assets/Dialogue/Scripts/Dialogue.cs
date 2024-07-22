using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField]
    string title;
    [SerializeField]
    string[] text;
    [SerializeField]
    float timeBetweenCharacters = .1f;
    [SerializeField]
    Sprite profliePic;
    [SerializeField]
    AudioClip characterSound;

    public string GetTitle()
    {
        return title;
    }
    public string[] GetLines()
    {
        return text;
    }
    public float GetTimeBetweenCharacters()
    {
        return timeBetweenCharacters;
    }
    public AudioClip GetAudioClip()
    {
        return characterSound;
    }
    public Sprite GetProfilePic()
    {
        return profliePic;
    }
}
