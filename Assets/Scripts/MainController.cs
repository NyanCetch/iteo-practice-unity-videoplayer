using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public VideoPlaylist VPList;
    public PopUpScript popup;

    public string[] ext = { "*.mp4", "*.avi", "*.mov", "*.webm", "*.wmv" };

    void Start()
    {
        if (!Directory.Exists(Application.streamingAssetsPath)) //проверка на существование папки StreamingAssets
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if (!File.Exists(Application.streamingAssetsPath + "/Видеоплейлист.txt")) //проверка на существование текстовика с путем плейлиста
        {
            File.Create(Application.streamingAssetsPath + "/Видеоплейлист.txt");
        }

        VPList.pathPlaylist = File.ReadAllText(Application.streamingAssetsPath + "/Видеоплейлист.txt"); //считывание текстовика
  
        if (VPList.pathPlaylist == "" || !File.Exists(Application.streamingAssetsPath + "/Видеоплейлист.txt")) //если файл пустой или отсутствует, то вызов pop-up'а
        {
            popup.WriteText("Не указана папка с видео!");
        }

        if (!Directory.Exists(VPList.pathPlaylist))
        {
            popup.WriteText("Указан несуществующий путь до папки с видео!");
        }

        if (!Path.IsPathRooted(VPList.pathPlaylist))
        {
            popup.WriteText("Введеный текст не является путем до папки!");
        }
        
        else
        {
            foreach (string ex in ext) //получение плейлиста и вызов первого видео, если есть поддерживаемые видео
            {
                VPList.playlist.AddRange(Directory.GetFiles(VPList.pathPlaylist, ex, SearchOption.AllDirectories));
            }           

            if (VPList.playlist != null)
            VPList.ShowVideo(0);

            else //иначе попап
                popup.WriteText("В указанной папке нет поддерживаемых видео!");
        }       
    }   
}
