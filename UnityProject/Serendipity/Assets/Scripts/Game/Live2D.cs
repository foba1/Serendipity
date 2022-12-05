using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Live2D : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(4f);
        Destroy(gameObject);
    }
}
