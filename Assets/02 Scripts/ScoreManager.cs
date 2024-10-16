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
        highScores.Sort((a,b) => b.score.CompareTo(a.score));   // 내림차순 정렬

        if(highScores.Count > maxScoreNumber)  // 20개 초과시 제거
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveScores();

        // 확인용
        Debug.Log("Current High Scores:");
        foreach (var rank in highScores)
        {
            Debug.Log($"Profile: {rank.profile}, Name: {rank.playerName}, Animal Count: {rank.animalCount}, Score: {rank.score}");
        }
    }

    public float GetBestScore()
    {
        return highScores.Count > 0 ? highScores[0].score : 0;  // 가장큰 값을 반환
    }

    private void SaveScores()
    {
        for(int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("Profile" + i, highScores[i].profile);           // 프로필 정보 저장
            PlayerPrefs.SetString("PlayerName" + i, highScores[i].playerName);  // 이름 저장
            PlayerPrefs.SetInt("AnimalCount" + i, highScores[i].animalCount);   // 동물갯수 저장
            PlayerPrefs.SetFloat("HighScore" + i, highScores[i].score);         // 점수 저장
        }
        PlayerPrefs.SetInt("PlayerRankCount", highScores.Count);     // 플레이어랭크 갯수 저장
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        int count = PlayerPrefs.GetInt("PlayerRankCount", 0);        // 플레이어랭크 갯수 가져오기
        highScores.Clear();

        for(int i = 0; i < count; i++)
        {
            int profile = PlayerPrefs.GetInt("Profile" + i, 0);
            string playerName = PlayerPrefs.GetString("PlayerName" + i, "No Name"); // 이름 불러오기
            int animalCount = PlayerPrefs.GetInt("AnimalCount" + i, 0);             // 동물갯수 불러오기
            float score = PlayerPrefs.GetFloat("HighScore" + i, 0.0f);              // 점수 불러오기

            highScores.Add(new PlayerRank(profile, playerName, animalCount, score));   // 리스트에 추가
        }
    }

    public List<PlayerRank> GetHighScores()
    {
        return highScores;
    }
}
