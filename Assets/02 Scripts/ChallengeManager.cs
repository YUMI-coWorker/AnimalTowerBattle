using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour
{
    // ��ư
    public Button[] buttons;    // �����ư
    private bool[] challengesCompleted = new bool[5];   // �������� �Ϸ� ����
    public ShopPanel shopPanel; // ����ȭ
    // �޼���
    public Text[] achievement;  // �޼���
    // ç���� ����
    private int achieve2 = 0;   // �ѳ��� ����
    private int achieve3 = 0;   // �������� ����

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            buttons[i].interactable = false;
            int index = i;  // Ŭ���� ���� �ذ�
            buttons[i].onClick.AddListener(() => RewardCoin(index));
        }

        // 1�� ç���� : �����ϱ�
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
        Debug.Log((index+1) + "�� ������ �Ϸ�Ǿ����ϴ�.");
        
    }

    private void RewardCoin(int index)
    {
        if (challengesCompleted[index])
        {
            shopPanel.currentCoins += 20;
            shopPanel.CoinChange(shopPanel.currentCoins);   // �����ؽ�Ʈ ������Ʈ
            buttons[index].interactable = false;
            Debug.Log("������ ���޵Ǿ����ϴ�.");

        }
    }

    // 2�� ç����
    public void Challenge_2(float height)
    {
        int achieve = Mathf.FloorToInt(height); // ����
        //int achieve = (int)height;            // ����ȯ

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

    // 3�� ç����
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

    

    // 4�� ç����
    // 5�� ç����




}
