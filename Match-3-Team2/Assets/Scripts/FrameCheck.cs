using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameCheck : MonoBehaviour
{
    public TextMeshProUGUI fpsText;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetFPS());
    }
    
    private IEnumerator SetFPS()
    {
        yield return new WaitForSeconds(0.5f);
        float fps = 1f / Time.deltaTime;
        fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
    }
}
