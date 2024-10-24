using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AdPanel : MonoBehaviour
{
    public Text countdownText;
    public Button closeBtn;
    public ChallengeManager challengeManager;

    public VideoPlayer videoPlayer;
    public int adDuration = 5;

    private void Start()
    {
        challengeManager = GameObject.Find("ChallengeManager").GetComponent<ChallengeManager>();
    }

    public void ShowAd()
    {
        gameObject.SetActive(true);
        closeBtn.gameObject.SetActive(false);
        videoPlayer.Play();
        StartCoroutine(Countdown(adDuration));
    }



    public void CloseAd()
    {
        challengeManager.Challenge_4();
        videoPlayer.Stop();
        gameObject.SetActive(false);
    }

    private IEnumerator Countdown(int time)
    {
        Debug.Log("Countdown Start");

        while(time > 0)
        {
            countdownText.text = time.ToString();  // 남은 시간 표시
            yield return new WaitForSecondsRealtime(1); // 1초 대기
            time--;
        }
        countdownText.text = "0";
        countdownText.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(true);
        
        
    }

    
}
