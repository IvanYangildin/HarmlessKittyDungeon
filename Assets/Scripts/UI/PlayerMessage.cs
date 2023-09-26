using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMessage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI messageBox;

    [SerializeField]
    [Min(0f)]
    float standardDuration;

    void Start()
    {
        StartCoroutine(Timer.Countdown(() => HideMessage(), standardDuration));
    }

    public void ShowMessage(string message)
    {
        ShowMessage(message, standardDuration);
    }

    public void ShowMessage(string message, float duration)
    {
        if (duration > 0)
        {
            StartCoroutine(Timer.Countdown(() => HideMessage(), duration));
        }
        messageBox.text = message;
    }

    public void HideMessage(string message = null)
    {
        if ((message == null) || (message == messageBox.text))
            messageBox.text = "";
    }
}
