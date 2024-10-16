using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class AdPanel : MonoBehaviour
{
    public Text countdownText;
    public Button closeBtn;
    public ChallengeManager challengeManager;

    private void Start()
    {
        challengeManager = GameObject.Find("ChallengeManager").GetComponent<ChallengeManager>();
    }

    private void OnEnable()
    {
        closeBtn.gameObject.SetActive(false);
        StartCoroutine(StartCountdown(5));
        Time.timeScale = 1;
    }
    private void OnDisable()
    {
        challengeManager.Challenge_4();
    }

    private IEnumerator StartCountdown(int startTime)
    {
        Debug.Log("Countdown Start");
        int timeLeft = startTime;

        while (timeLeft >= 0)
        {
            countdownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft = timeLeft - 1;
        }

        closeBtn.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}
