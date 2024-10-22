using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    public GameObject rankPrefab;   // 랭크를 표시할 프리팹
    public Transform content;       // 스크롤뷰의 content
    public Sprite[] profileIMG;

    public ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    public void UpdateRank()
    {
        // 순위 넣어주기
        for (int i = 0; i < scoreManager.highScores.Count; i++)
        {
            scoreManager.highScores[i].rank = i + 1;
        }

        // 기존 아이템 제거
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 새로운 오브젝트 생성
        foreach(PlayerRank data in scoreManager.highScores)
        {
            GameObject contents = Instantiate(rankPrefab, content);
            Text[] textComponents = contents.GetComponentsInChildren<Text>();

            foreach(Text text in textComponents)
            {
                if(text.name == "UserName")
                {
                    text.text = data.playerName;
                }
                if(text.name == "AnimalNumber")
                {
                    text.text = data.animalCount.ToString();
                }
                if(text.name == "Height")
                {
                    text.text = data.score.ToString("F2");
                }
                if(text.name == "Rank")
                {
                    text.text = data.rank.ToString();
                }
            }
            Image[] images = contents.GetComponentsInChildren<Image>();

            foreach(Image image in images)
            {
                if(image.name == "Profile")
                {
                    image.sprite = profileIMG[data.profile];
                }
            }
        }
    }

}
