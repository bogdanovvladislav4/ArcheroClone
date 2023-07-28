using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{

    [SerializeField] private Character character;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private Text scoreCounter;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float speedAnimTakeCoin;
    [SerializeField] private GameObject inGamePanel;
    
    private int score;

    private const string keyScore = "Score";

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        score = PlayerPrefs.GetInt(keyScore);
    }

    public void AddScore(Vector3 pos)
    {
        score++;
        if (UnityEngine.Camera.main != null) TakeCoinAnimation(UnityEngine.Camera.main.WorldToScreenPoint(pos));
    }

    private void Update()
    {
        if (character.GetHealth() <= 0)
        {
            diePanel.SetActive(true);
            Time.timeScale = 0;
        }

        scoreCounter.text = score.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(keyScore, score);
        PlayerPrefs.Save();
    }

    private void TakeCoinAnimation(Vector3 pos)
    {
        GameObject coin = Instantiate(coinPrefab, inGamePanel.transform, true);
        coin.transform.position = pos;
        coin.transform.DOMove(scoreCounter.transform.position, speedAnimTakeCoin);
        Destroy(coin, 1);
    }
}
