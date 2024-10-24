using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    // �̱�
    public Text titleCoin;
    public Text shopCoin;
    public int currentCoins = 500;  // ���� ���� �ִ� ���ΰ���
    public int drawPrice = 100;     // 1ȸ �̱� ���
    public Button drawBtn;

    // ���� ���
    public List<GameObject> drawPrefabs = new List<GameObject>();
    public SpawnManager spawnManager;

    // ����
    public Image displayImage;
    public Text displayName;
    public float appearDuration = 1.0f;

    // ȿ����
    public SoundManager soundManager;


    void Start()
    {
        CoinChange(currentCoins);
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
    }

    private IEnumerator ScaleUp(RectTransform imageTransform, string animalName)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = Vector3.one;    // ���� ũ��

        while (elapsedTime < appearDuration)
        {
            imageTransform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / appearDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageTransform.localScale = originalScale;  // ���� ũ��� ����

        // �����̸� �ؽ�Ʈ ����
        displayName.text = animalName;
    }

    // �̱�
    public void DrawingLots()
    {

        if(currentCoins >= drawPrice)
        {
            // �ܾ� ����
            currentCoins -= drawPrice;
            CoinChange(currentCoins);

            // ���� �̱� ���� �ʱ�ȭ
            if (displayName.text != null) displayName.text = "";
            displayImage.color = new Color32(255, 255, 255, 255);

            // ȿ���� ���
            soundManager.PlaySound(4);

            // ���� ���� ����
            int rnd = Random.Range(0, drawPrefabs.Count);
            Debug.Log(rnd);
            Sprite selectedSprite = drawPrefabs[rnd].GetComponent<SpriteRenderer>().sprite;
            
            // UI �̹����� ��������Ʈ ����
            displayImage.sprite = selectedSprite;

            //���� ��������Ʈ ũ�⸦ ������� RectTransform ũ�� ����
            RectTransform rt = displayImage.rectTransform;
            rt.sizeDelta = new Vector2(selectedSprite.rect.width, selectedSprite.rect.height);


            // ó������ ������ �ʵ��� ũ�� ����
            displayImage.rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            // ũ������, �̸�ǥ�� �ڷ�ƾ
            StartCoroutine(ScaleUp(displayImage.rectTransform, drawPrefabs[rnd].name));

            // draw ��Ͽ��� basic ������� ������ �̵�
            spawnManager.basicPrefabs.Add(drawPrefabs[rnd]);
            drawPrefabs.RemoveAt(rnd);
        }
        else  // �̱Ⱑ�ݺ��� ������ ������ ��ư ��Ȱ��ȭ
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
        displayImage.sprite = null; // ��������Ʈ �ʱ�ȭ
        RectTransform rt = displayImage.rectTransform;
        rt.sizeDelta = new Vector2(1, 1);   // ũ�⸦ 1�ȼ���
        displayImage.rectTransform.localScale = Vector3.one;    // �������� 1��
        displayImage.color = new Color32(49, 208, 251, 255);    // ���� ����

        displayName.text = "";
    }
}
