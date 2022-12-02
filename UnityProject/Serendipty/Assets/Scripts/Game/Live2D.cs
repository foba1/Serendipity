using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Live2D : MonoBehaviour
{
    private bool startDelay = false;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        StartCoroutine(WaitCoroutine());
        videoPlayer = transform.GetChild(0).GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (startDelay)
        {
            if (!videoPlayer.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        startDelay = true;
    }
}
