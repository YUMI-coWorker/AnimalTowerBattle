using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerRank
{
    public int profile;
    public string playerName;
    public int animalCount;
    public float score;
    public int rank;

    public PlayerRank(int profile, string playerName, int animalCount, float score)
    {
        this.profile = profile;
        this.playerName = playerName;
        this.animalCount = animalCount;
        this.score = score;
        this.rank = 0;
    }
}
public class ScoreManager : MonoBehaviour
{
    public List<PlayerRank> highScores = new List<PlayerRank>();
    public int maxScoreNumber = 20;

    void Start()
    {
        LoadScores();
    }

    public void AddScore(int profile, string playerName, int animalCount, float score)
    {
        highScores.Add(new PlayerRank(profile, playerName, animalCount, score));
        highScores.Sort((a,b) => b.score.CompareTo(a.score));   // �������� ����

        if(highScores.Count > maxScoreNumber)  // 20�� �ʰ��� ����
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveScores();

        // Ȯ�ο�
        Debug.Log("Current High Scores:");
        foreach (var rank in highScores)
        {
            Debug.Log($"Profile: {rank.profile}, Name: {rank.playerName}, Animal Count: {rank.animalCount}, Score: {rank.score}");
        }
    }

    public float GetBestScore()
    {
        return highScores.Count > 0 ? highScores[0].score : 0;  // ����ū ���� ��ȯ
    }

    private void SaveScores()
    {
        for(int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("Profile" + i, highScores[i].profile);           // ������ ���� ����
            PlayerPrefs.SetString("PlayerName" + i, highScores[i].playerName);  // �̸� ����
            PlayerPrefs.SetInt("AnimalCount" + i, highScores[i].animalCount);   // �������� ����
            PlayerPrefs.SetFloat("HighScore" + i, highScores[i].score);         // ���� ����
        }
        PlayerPrefs.SetInt("PlayerRankCount", highScores.Count);     // �÷��̾ũ ���� ����
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        int count = PlayerPrefs.GetInt("PlayerRankCount", 0);        // �÷��̾ũ ���� ��������
        highScores.Clear();

        for(int i = 0; i < count; i++)
        {
            int profile = PlayerPrefs.GetInt("Profile" + i, 0);
            string playerName = PlayerPrefs.GetString("PlayerName" + i, "No Name"); // �̸� �ҷ�����
            int animalCount = PlayerPrefs.GetInt("AnimalCount" + i, 0);             // �������� �ҷ�����
            float score = PlayerPrefs.GetFloat("HighScore" + i, 0.0f);              // ���� �ҷ�����

            highScores.Add(new PlayerRank(profile, playerName, animalCount, score));   // ����Ʈ�� �߰�
        }
    }

    public List<PlayerRank> GetHighScores()
    {
        return highScores;
    }
}
