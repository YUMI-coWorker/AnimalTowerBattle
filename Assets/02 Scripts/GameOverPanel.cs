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

    // ������
    public SpawnManager spawnManager;
    public Text latestRecord;

    // �ְ���
    public ScoreManager scoreManager;
    public Text bestRecord;

    // ������
    public Text animalCount;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {

    }

    public void GoLobby()   // �κ��̵�
    {
        gameOverPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
    public void Restart()   // �����
    {
        gameOverPanel.SetActive(false);
        playPanel.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        // ������ �����ֱ�
        latestRecord.text = spawnManager.totalHeight.ToString("F2") + "m";
        // �ְ��� �����ֱ�
        float bestScore = scoreManager.GetBestScore();
        bestRecord.text = bestScore.ToString("F2") + "m";
        // �������� �����ֱ�
        animalCount.text = (spawnManager.animalCount -1).ToString() + "����";

    }
    private void OnDisable()
    {   
        spawnManager.Initialize();  // �ʱ�ȭ
    }

}
