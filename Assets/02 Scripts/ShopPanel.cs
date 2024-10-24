using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    // 뽑기
    public Text titleCoin;
    public Text shopCoin;
    public int currentCoins = 500;  // 현재 갖고 있는 코인갯수
    public int drawPrice = 100;     // 1회 뽑기 비용
    public Button drawBtn;

    // 동물 목록
    public List<GameObject> drawPrefabs = new List<GameObject>();
    public SpawnManager spawnManager;

    // 등장
    public Image displayImage;
    public Text displayName;
    public float appearDuration = 1.0f;

    // 효과음
    public SoundManager soundManager;


    void Start()
    {
        CoinChange(currentCoins);
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
    }

    private IEnumerator ScaleUp(RectTransform imageTransform, string animalName)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = Vector3.one;    // 최종 크기

        while (elapsedTime < appearDuration)
        {
            imageTransform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / appearDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageTransform.localScale = originalScale;  // 최종 크기로 설정

        // 동물이름 텍스트 설정
        displayName.text = animalName;
    }

    // 뽑기
    public void DrawingLots()
    {

        if(currentCoins >= drawPrice)
        {
            // 잔액 변동
            currentCoins -= drawPrice;
            CoinChange(currentCoins);

            // 이전 뽑기 내용 초기화
            if (displayName.text != null) displayName.text = "";
            displayImage.color = new Color32(255, 255, 255, 255);

            // 효과음 재생
            soundManager.PlaySound(4);

            // 랜덤 동물 선택
            int rnd = Random.Range(0, drawPrefabs.Count);
            Debug.Log(rnd);
            Sprite selectedSprite = drawPrefabs[rnd].GetComponent<SpriteRenderer>().sprite;
            
            // UI 이미지의 스프라이트 변경
            displayImage.sprite = selectedSprite;

            //원래 스프라이트 크기를 기반으로 RectTransform 크기 조정
            RectTransform rt = displayImage.rectTransform;
            rt.sizeDelta = new Vector2(selectedSprite.rect.width, selectedSprite.rect.height);


            // 처음에는 보이지 않도록 크기 설정
            displayImage.rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            // 크기조정, 이름표시 코루틴
            StartCoroutine(ScaleUp(displayImage.rectTransform, drawPrefabs[rnd].name));

            // draw 목록에서 basic 목록으로 프리팹 이동
            spawnManager.basicPrefabs.Add(drawPrefabs[rnd]);
            drawPrefabs.RemoveAt(rnd);
        }
        else  // 뽑기가격보다 코인이 적으면 버튼 비활성화
        {
            drawBtn.interactable = false;
        }
        ButtonState(currentCoins);
    }
    public void CoinChange(int currentCoins)
    {
        titleCoin.text = currentCoins.ToString();
        shopCoin.text = titleCoin.text;
    }

    private void ButtonState(int currentCoins)
    {
        drawBtn.interactable = currentCoins >= drawPrice;
    }

    private void OnDisable()
    {
        ResetPanel();
    }

    private void ResetPanel()
    {
        displayImage.sprite = null; // 스프라이트 초기화
        RectTransform rt = displayImage.rectTransform;
        rt.sizeDelta = new Vector2(1, 1);   // 크기를 1픽셀로
        displayImage.rectTransform.localScale = Vector3.one;    // 스케일을 1로
        displayImage.color = new Color32(49, 208, 251, 255);    // 색상 설정

        displayName.text = "";
    }
}
