using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerProfile player;

    [SerializeField]
    private Image bar;

    private void Update()
    {
        bar.fillAmount = player.Health / player.MaxHealth;
    }

}
