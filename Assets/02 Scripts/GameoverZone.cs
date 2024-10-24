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
    
    // ���ӿ������� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);

        if (collision.CompareTag("Animal"))
        {
            Debug.Log(collision.name);
            collision.GetComponent<Animal>().isGameOver = true; // ������ ������ �����.
            Time.timeScale = 0;
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("���� ����!");

        // ȿ������ ����Ѵ�.
        soundManager.PlaySound(3);

        // �̹������� ������ ����Ѵ�.
        scoreManager.AddScore(SettingPanel.profileNum, SettingPanel.userName, spawnManager.animalCount - 1, spawnManager.totalHeight);

        // �г��� ���� �ݴ´�.
        playPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        // ��ũ�гο� ����Ʈ�� �־��ش�.
        rankPanel.copyRank.AddRange(scoreManager.highScores);
    }




    void Update()
    {
        
    }
}
