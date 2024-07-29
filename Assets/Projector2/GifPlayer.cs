using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifPlayer : MonoBehaviour
{
    [SerializeField]
    Texture[] frames;
    [SerializeField]
    float timeBetweenFrames=.1f;

    Material mat;
    int index = 0;

    //Use this site to split frames: https://ezgif.com/split
    private void Awake()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mat = new Material(mr.material);
        mr.material = mat;
        mat.mainTexture = frames[0];
        StartCoroutine(GifCoroutine());
    }


    IEnumerator GifCoroutine()
    {
        while (true)
        {
            index++;
            if (index >= frames.Length)
                index = 0;
            mat.mainTexture = frames[index];
            yield return new WaitForSeconds(timeBetweenFrames);
        }
    }
}
