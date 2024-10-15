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


    void Start()
    {
        CoinChange(currentCoins);
    }


    void Update()
    {
        
    }
    public void CoinChange(int currentCoins)
    {
        titleCoin.text = currentCoins.ToString();
        shopCoin.text = titleCoin.text;
    }
    public void DrawingLots()
    {

        if(currentCoins >= drawPrice)
        {
            currentCoins -= drawPrice;
            CoinChange(currentCoins);
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
