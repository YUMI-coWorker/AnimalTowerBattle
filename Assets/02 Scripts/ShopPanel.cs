using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public Text titleCoin;
    public Text shopCoin;
    public int currentCoins = 500;  // ���� ���� �ִ� ���ΰ���
    public int drawPrice = 100;     // 1ȸ �̱� ���
    public Button drawBtn;

    public List<GameObject> drawPrefabs = new List<GameObject>();
    public SpawnManager spawnManager;


    void Start()
    {
        CoinChange(currentCoins);
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
    }


    void Update()
    {
        
    }
    public void CoinChange(int currentCoins)
    {
        titleCoin.text = currentCoins.ToString();
        shopCoin.text = titleCoin.text;
    }

    // �̱�
    public void DrawingLots()
    {

        if(currentCoins >= drawPrice)
        {
            currentCoins -= drawPrice;
            CoinChange(currentCoins);

            int rnd = Random.Range(0, drawPrefabs.Count);
            Debug.Log("rnd");
            spawnManager.basicPrefabs.Add(drawPrefabs[rnd]);
            drawPrefabs.RemoveAt(rnd);
        }
        else  // �̱Ⱑ�ݺ��� ������ ������ ��ư ��Ȱ��ȭ
        {
            drawBtn.interactable = false;
        }
        ButtonState(currentCoins);
    }

    private void ButtonState(int currentCoins)
    {
        drawBtn.interactable = currentCoins >= drawPrice;
    }


}
