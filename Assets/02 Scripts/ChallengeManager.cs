using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour
{
    // 버튼
    public Button[] buttons;    // 보상버튼
    private bool[] challengesCompleted = new bool[5];   // 도전과제 완료 여부
    public ShopPanel shopPanel; // 동기화
    // 달성도
    public Text[] achievement;  // 달성도
    // 챌린지 변수
    private int achieve2 = 0;   // 총높이 저장
    private int achieve3 = 0;   // 동물갯수 저장

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].interactable = false;
            int index = i;  // 클로저 문제 해결
            buttons[i].onClick.AddListener(() => RewardCoin(index));
        }

        // 1번 챌린지 : 접속하기
        CompleteChallenge(0);
        achievement[0].text = "1 / 1";

    }

    void Update()
    {

    }

    private void CompleteChallenge(int index)
    {
        challengesCompleted[index] = true;
        buttons[index].interactable = true;
        Debug.Log((index+1) + "번 과제가 완료되었습니다.");
        
    }

    private void RewardCoin(int index)
    {
        if (challengesCompleted[index])
        {
            shopPanel.currentCoins += 20;
            shopPanel.CoinChange(shopPanel.currentCoins);   // 코인텍스트 업데이트
            buttons[index].interactable = false;
            Debug.Log("코인이 지급되었습니다.");

        }
    }

    // 2번 챌린지
    public void Challenge_2(float height)
    {
        int achieve = Mathf.FloorToInt(height); // 버림
        //int achieve = (int)height;            // 형변환

        if(achieve > achieve2)
        {
            achieve2 = achieve;
        }
        achievement[1].text = achieve2 + " / 10";
        
        if(achieve2 >= 10)
        {
            CompleteChallenge(1);
        }
    }

    // 3번 챌린지
    public void Challenge_3(int animalCount)
    {
        if(animalCount > achieve3)
        {
            achieve3 = animalCount;
        }
        achievement[2].text = achieve3 + " / 12";

        if(achieve3 >= 12)
        {
            CompleteChallenge(2);
        }
    }

    

    // 4번 챌린지
    // 5번 챌린지




}
