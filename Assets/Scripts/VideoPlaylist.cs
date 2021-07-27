using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VideoPlaylist : MonoBehaviour
{
    public string pathPlaylist;
    public List<string> playlist;

    public VideoController VC;

    public int _currentVideoIndex;

    private void Start()
    {       
        VC.OnPlaybackCompleted += VideoPlaybackCompleted;
    }

    public void ShowVideo(int index)
    {
        if (index < 0) //для кругового переключения в обратную сторону
            index = playlist.Count - 1;

        if (index == playlist.Count) //для кругового переключения
            index = 0;

        var url = playlist[index];
        VC.OpenFile(url);

        _currentVideoIndex = index;
    }

    public void NextVideo()
    {
        ShowVideo(_currentVideoIndex + 1);
    }

    public void PrevVideo()
    {
        ShowVideo(_currentVideoIndex - 1);
    }

    public void VideoPlaybackCompleted()
    {
        if (_currentVideoIndex != playlist.Count - 1) //пока видео не достигло последнего, идем дальше по плейлисту
        {
            NextVideo();
        }
        else //на окончании последнего видео возвращаемся в начало
        {
            _currentVideoIndex = 0;
            ShowVideo(_currentVideoIndex);
        }
    }
}
