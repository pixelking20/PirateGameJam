using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InputSystem;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox instance;
    [SerializeField]
    TextMeshProUGUI speechBox, titleCard;
    [SerializeField]
    Image profileSlot;
    AudioSource speaker;

    Vector3 shownPosition, collapsedPosition;
    float collapseTime = .25f;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        speaker = GetComponent<AudioSource>();
        shownPosition = transform.position;
        Transform collapsedPositionHolder = transform.Find("CollapsedPositionHolder");
        collapsedPosition = collapsedPositionHolder.position;
        Destroy(collapsedPositionHolder.gameObject);
        transform.position = collapsedPosition;
    }

    public void LoadDialogueInfo(Dialogue dialogue)
    {
        SetProfilePic(dialogue.GetProfilePic());
        SetTitleCard(dialogue.GetTitle());
        speechBox.SetText("");
    }
    public IEnumerator InitializeNewDialogue(Dialogue dialogue)
    {
        yield return Speak(dialogue);
    }
    IEnumerator Speak(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();

        float timeBetweenCharacters = dialogue.GetTimeBetweenCharacters();
        int lineIndex = 0;

        string[] lines = dialogue.GetLines();
        string constructedText = "", line = lines[0];
        speaker.clip = dialogue.GetAudioClip();

        while (lineIndex < lines.Length)
        {
            line = lines[lineIndex];
            int characterIndex = 0;
            while (characterIndex < line.Length)
            {
                constructedText += line[characterIndex];
                characterIndex++;
                SetText(constructedText);
                PlayCharacterNoise();

                float timeElapsed = 0;
                while (timeElapsed < timeBetweenCharacters)
                {
                    timeElapsed += Time.deltaTime;
                    if (InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
                    {
                        constructedText = "";

                        for (int i = 0; i < lines.Length; i++)
                        {
                            constructedText += lines[i] + "<br>";
                        }

                        lineIndex = lines.Length;
                        characterIndex = line.Length;
                        timeElapsed = timeBetweenCharacters;
                        SetText(constructedText);
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
            constructedText += "<br>";
            lineIndex++;
        }
    }

    void SetText(string text)
    {
        speechBox.SetText(text);
    }
    void SetTitleCard(string text)
    {
        titleCard.text = text;
    }
    void PlayCharacterNoise()
    {
        GameObject newSource = new GameObject("SelfDeletingAudioSource");
        newSource.transform.parent = transform;
        newSource.AddComponent<AudioSource>();
        AudioSourceSelfDeleting audioSourceSelfDeleting = newSource.AddComponent<AudioSourceSelfDeleting>();

        audioSourceSelfDeleting.PlayClip(speaker.clip);
    }
    void SetProfilePic(Sprite pic)
    {
        profileSlot.sprite = pic;
    }

    public static void Collapse()
    {
        instance.StartCoroutine(instance.CollapseMotion());
    }
    public static void PopUp()
    {
        instance.StartCoroutine(instance.PopUpMotion());
    }
    public IEnumerator CollapseMotion()
    {
        float timeElapsed = 0;
        while(timeElapsed < collapseTime)
        {
            transform.position = Vector3.Lerp(shownPosition, collapsedPosition, timeElapsed / collapseTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator PopUpMotion()
    {
        float timeElapsed = 0;
        while (timeElapsed < collapseTime)
        {
            transform.position = Vector3.Lerp(collapsedPosition, shownPosition, timeElapsed / collapseTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
