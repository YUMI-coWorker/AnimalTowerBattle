using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePanel : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject playPanel;
    public GameObject gameOverPanel;
    public GameObject noticePanel;
    public GameObject rankPanel;
    public GameObject settingPanel;
    public GameObject shopPanel;
    public GameObject challengePanel;
    public GameObject adPanel;

    public void GameStart()
    {
        gameOverPanel.SetActive(false);
        titlePanel.SetActive(false);
        playPanel.SetActive(true);
        Time.timeScale = 1;
    }
    public void OpenNotice()
    {
        noticePanel.SetActive(true);
    }
    public void CloseNotice()
    {
        noticePanel.SetActive(false);
    }
    public void OpenRankPanel()
    {
        rankPanel.SetActive(true);
    }
    public void CloseRankPanel()
    {
        rankPanel.SetActive(false);
    }
    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }
    public void OpenShopPanel()
    {
        shopPanel.SetActive(true);
    }
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }
    public void OpenChallengePanel()
    {
        challengePanel.SetActive(true);
    }
    public void CloseChallengePanel()
    {
        challengePanel.SetActive(false);
    }

    public void OpenAdPanel()
    {
        adPanel.SetActive(true);
    }

    public void CloseAdPanel()
    {
        adPanel.SetActive(false);
    }

}
