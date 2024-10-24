using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverZone : MonoBehaviour
{
    public ScoreManager scoreManager;
    public SpawnManager spawnManager;
    public RankPanel rankPanel;
    public SoundManager soundManager;

    public GameObject gameOverPanel;
    public GameObject playPanel;


    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
        gameOverPanel.SetActive(false);
    }
    
    // 게임오버존에 도달
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);

        if (collision.CompareTag("Animal"))
        {
            Debug.Log(collision.name);
            collision.GetComponent<Animal>().isGameOver = true; // 동물의 조작을 멈춘다.
            Time.timeScale = 0;
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("게임 오버!");

        // 효과음을 재생한다.
        soundManager.PlaySound(3);

        // 이번게임의 점수를 기록한다.
        scoreManager.AddScore(SettingPanel.profileNum, SettingPanel.userName, spawnManager.animalCount - 1, spawnManager.totalHeight);

        // 패널을 열고 닫는다.
        playPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        // 랭크패널에 리스트를 넣어준다.
        rankPanel.copyRank.AddRange(scoreManager.highScores);
    }




    void Update()
    {
        
    }
}
