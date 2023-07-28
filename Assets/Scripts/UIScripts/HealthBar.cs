using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text healthCounter;
    [SerializeField] private Character character;

    private void Start()
    {
        int healthValue = character.GetHealth();
        healthBar.maxValue = character.GetHealth();
        healthBar.value = healthValue;
        healthCounter.text = healthValue.ToString();
    }

    private void Update()
    {
        int healthValue = character.GetHealth();
        healthBar.value = healthValue;
        healthCounter.text = healthValue.ToString();
    }
}
