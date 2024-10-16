using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
//public class PlayerRank
//{
//    public int profile;
//    public string playerName;
//    public int animalCount;
//    public float score;

//    public PlayerRank(int profile, string playerName, int animalCount, float score)
//    {
//        this.profile = profile;
//        this.playerName = playerName;
//        this.animalCount = animalCount;
//        this.score = score;
//    }
//}
public class ScoreManager : MonoBehaviour
{
    public List<float> highScores = new List<float>();
    public int maxScoreNumber = 20;

    void Start()
    {
        LoadScores();
    }

    void Update()
    {

    }

    public void AddScore(float score)
    {
        highScores.Add(score);
        highScores.Sort((a,b) => b.CompareTo(a));   // �������� ����

        if(highScores.Count > maxScoreNumber)  // 20�� �ʰ��� ����
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveScores();
    }

    public float GetBestScore()
    {
        return highScores.Count > 0 ? highScores[0] : 0;
    }

    private void SaveScores()
    {
        for(int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, highScores[i]);   // �� ����
        }
        PlayerPrefs.SetInt("HighScoreCount", highScores.Count);     // ���� ����
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        int count = PlayerPrefs.GetInt("HighScoreCount", 0);        // ���� ��������
        highScores.Clear();

        for(int i = 0;i < count;i++)
        {
            highScores.Add(PlayerPrefs.GetFloat("HighScore" + i, 0));   // �ҷ�����
        }
    }

}
