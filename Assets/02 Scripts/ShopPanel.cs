using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public Text titleCoin;
    public Text shopCoin;
    public int currentCoins = 500;  // 현재 갖고 있는 코인갯수
    public int drawPrice = 100;     // 1회 뽑기 비용
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
        else  // 뽑기가격보다 코인이 적으면 버튼 비활성화
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
