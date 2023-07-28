using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void PauseClick()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
