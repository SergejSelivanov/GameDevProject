﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWidth : MonoBehaviour
{ 
    // Set this to your target aspect ratio, eg. (16, 9) or (4, 3).
    public Vector2 targetAspect = new Vector2(16, 9);
    Camera _camera;
    public GameObject DownBar;
    public GameObject UpBar;

    private void GetBlackBars()
    {
        float screenRatio = Screen.width / (float)Screen.height;
        float targetRatio = targetAspect.x / targetAspect.y;
        float normalizedHeight = screenRatio / targetRatio;
        float barThickness = (1f - normalizedHeight) / 2f;
        DownBar.SetActive(true);
        DownBar.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        DownBar.GetComponent<RectTransform>().anchorMax = new Vector2(1, barThickness);
        DownBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        DownBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        UpBar.SetActive(true);
        UpBar.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        UpBar.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1f - barThickness);
        UpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        UpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    void Start()
    {
        if (Screen.width / (float)Screen.height < 1.42f)
        {
            _camera = GetComponent<Camera>();
            UpdateCrop();
            GetBlackBars();
        }
    }

    // Call this method if your window size or target aspect change.
    public void UpdateCrop()
    {
        // Determine ratios of screen/window & target, respectively.
        float screenRatio = Screen.width / (float)Screen.height;
        float targetRatio = targetAspect.x / targetAspect.y;

        if (Mathf.Approximately(screenRatio, targetRatio))
        {
            // Screen or window is the target aspect ratio: use the whole area.
            _camera.rect = new Rect(0, 0, 1, 1);
        }
        else
        {
            // Screen or window is narrower than the target: letterbox.
            float normalizedHeight = screenRatio / targetRatio;
            float barThickness = (1f - normalizedHeight) / 2f;
            _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }
    }
}
