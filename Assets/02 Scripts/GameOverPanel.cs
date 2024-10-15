using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    public GameObject playPanel;
    public GameObject titlePanel;
    public GameObject gameOverPanel;

    // 현재기록
    public SpawnManager spawnManager;
    public Text latestRecord;

    // 최고기록
    public ScoreManager scoreManager;
    public Text bestRecord;

    // 동물수
    public Text animalCount;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {

    }

    public void GoLobby()   // 로비이동
    {
        gameOverPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
    public void Restart()   // 재시작
    {
        gameOverPanel.SetActive(false);
        playPanel.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        // 현재기록 보여주기
        latestRecord.text = spawnManager.totalHeight.ToString("F2") + "m";
        // 최고기록 보여주기
        float bestScore = scoreManager.GetBestScore();
        bestRecord.text = bestScore.ToString("F2") + "m";
        // 동물갯수 보여주기
        animalCount.text = (spawnManager.animalCount -1).ToString() + "마리";

    }
    private void OnDisable()
    {   
        spawnManager.Initialize();  // 초기화
    }

}
