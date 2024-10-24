using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    public GameObject rankPrefab;   // ·©Å©¸¦ Ç¥½ÃÇÒ ÇÁ¸®ÆÕ
    public Transform content;       // ½ºÅ©·ÑºäÀÇ content
    public Sprite[] profileIMG;
    public List<PlayerRank> copyRank = new List<PlayerRank>();

    public ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnEnable()
    {
        UpdateRank();
    }

    public void UpdateRank()
    {
        RemoveContent();
        MakeContent();
    }

    private void RemoveContent()
    {
        // ±âÁ¸ ÄÜÅÙÃ÷ Á¦°Å
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void MakeContent()
    {
        // »õ·Î¿î ¿ÀºêÁ§Æ® »ý¼º
        foreach (PlayerRank data in copyRank)
        {
            GameObject contents = Instantiate(rankPrefab, content);
            Text[] textComponents = contents.GetComponentsInChildren<Text>();

            foreach (Text text in textComponents)
            {
                if (text.name == "UserName")
                {
                    text.text = data.playerName;
                }
                if (text.name == "AnimalNumber")
                {
                    text.text = data.animalCount.ToString();
                }
                if (text.name == "Height")
                {
                    text.text = data.score.ToString("F2");
                }
                if (text.name == "Rank")
                {
                    text.text = data.rank.ToString();
                }
            }
            Image[] images = contents.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                if (image.name == "Profile")
                {
                    image.sprite = profileIMG[data.profile];
                }
            }
        }
    }

}
