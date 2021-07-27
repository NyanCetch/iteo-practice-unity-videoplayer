using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class VideoControls : MonoBehaviour
{
    public CanvasGroup CG;
    public bool autoHide;
    public float hideDelay = 2f; 

    public bool isTimer;
    public float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) //при любом нажатии и удерживании кнопок панель появляется
        {
            ShowPanel();
        }

        if (isTimer != true)
        {
            return;
        }

        if (timer < 3 && timer > 0)
            timer -= Time.deltaTime;

        if (timer < 0) //по истечению таймера панель скрывается
        {
            if (autoHide == true)
                Hide();
            isTimer = false;
        }       
    }

    public void Show()
    {
        CG.alpha = 1;
        CG.interactable = true;
    }
    public void Hide()
    {
        CG.alpha = 0;
        CG.interactable = false;
    }

    public void ShowPanel()
    {
        timer = hideDelay;
        isTimer = true;
        Show();
    }
}
