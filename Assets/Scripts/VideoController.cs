using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public event Action OnPlaybackCompleted; //эвент окончания видео

    [Range(0f, 0.99f)]
    public float progressPercent = 0.99f;


    public bool completePlaybackOnProgress;

    public VideoPlayer VP;
    public VideoPlaylist VPList;

    public Button playButton;
    public Button pauseButton;
    public Button stopButton;
    public Toggle volumeModeToggle;
    public Button nextVideoButton;
    public Button prevVideoButton;
    public Button exitButton;


    public Slider SeekSlider;
    public Slider ProgressSlider;

    public bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        if (VP != null)
            VP.targetTexture.Release();

        playButton.onClick.AddListener(Play); 
        pauseButton.onClick.AddListener(Pause);
        stopButton.onClick.AddListener(Stop);
        SeekSlider.onValueChanged.AddListener(SeekSliding);
        volumeModeToggle.onValueChanged.AddListener((isOn) => VolumeModeChanger(isOn));
        nextVideoButton.onClick.AddListener(VPList.NextVideo);
        prevVideoButton.onClick.AddListener(VPList.PrevVideo);
        exitButton.onClick.AddListener(Exit);
        
        EventTrigger trigger = SeekSlider.GetComponent<EventTrigger>(); //тригерр для обнаружения перетягивания слайдера и остановки видеоролика в это время
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry2.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener(OnBeginDrag);
        entry2.callback.AddListener(OnEndDrag);
        trigger.triggers.Add(entry);
        trigger.triggers.Add(entry2);
    }

    // Update is called once per frame
    void Update()
    {
        if (VP == null)
            return;

        var nTime = 1f * VP.frame / VP.frameCount; 
        ProgressSlider.value = nTime; //связь слайдера с прогрессом видеоряда

        if (VP.isPlaying) //вызов события при окончания видеоролика
        {
            if (nTime > progressPercent && completePlaybackOnProgress == true)
            {
                RaisePlaybackCompleted();
            }
        }       
        if (Input.GetKeyDown(KeyCode.Escape)) //закрытие приложения при esc
        {
            Exit();
        }
    }

    private void RaisePlaybackCompleted()
    {
        OnPlaybackCompleted?.Invoke();
    }

    public void OpenFile(string url)
    {
        VP.Stop();
        VP.url = url;
        VP.Play();
    }

    public void Play()
    {
        VP.Play();
    }

    public void Pause()
    {
        VP.Pause();
    }

    public void Stop()
    {
        VP.Stop();
        VP.targetTexture.Release();
    }

    public void VolumeModeChanger(bool isOn)
    {
        if (isOn)
            VP.SetDirectAudioVolume(0, 0);
        else 
            VP.SetDirectAudioVolume(0, 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SeekSliding(float nTime) //перетягивание слайдера
    {
        VP.frame = (long)(VP.frameCount * nTime);
    }

    public void OnBeginDrag(BaseEventData data)
    {
        isDragging = true;
        VP.Pause();
    }
    public void OnEndDrag(BaseEventData data)
    {
        isDragging = false;
        VP.Play();
    }
}
