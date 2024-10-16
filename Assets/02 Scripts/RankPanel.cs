using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    public GameObject rankPrefab;   // ��ũ�� ǥ���� ������
    public Transform content;       // ��ũ�Ѻ��� content
    public List<PlayerRank> ranks;  // ǥ���� ����Ʈ
    public Sprite[] profileIMG;
    public ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }


    public void UpdateRank()
    {
        ranks = new List<PlayerRank>(scoreManager.GetHighScores());

        // ���� �־��ֱ�
        for (int i = 0; i < ranks.Count; i++)
        {
            ranks[i].rank = i + 1;
        }

        // ���� ������ ����
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // ���ο� ������Ʈ ����
        foreach(PlayerRank data in ranks)
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
