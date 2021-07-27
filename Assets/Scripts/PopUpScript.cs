using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public GameObject popup;
    public TextMeshProUGUI text;

    public void Show()
    {
        popup.SetActive(true);
    }

    public void UI_Hide()
    {
        popup.SetActive(false);
        Application.Quit();
    }

    public void WriteText(string error)
    {
        text.text = error;
        Show();
    }
}
