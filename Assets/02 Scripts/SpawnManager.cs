using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //public GameObject[] prefabs;    // 프리팹 배열
    public List<GameObject> basicPrefabs = new List<GameObject>(); 
    public Vector2 spawnPosition;   // 생성 위치
    public GameObject spawnedObject;

    public int changeCount = 0;    // 교체횟수
    private int maxChangeCount = 2; // 최대교체횟수
    public Button button;

    public float rotationSpeed = 100f;  // 회전속도
    public bool isRotating = false;     // 회전상태
    public SettingPanel rotationSet;     // 회전설정

    public float totalHeight = 0f;  // 최종 높이
    public Text heightTxt;

    public int animalCount = 0;    // 동물 갯수

    public Text animalNameTxt;

    public Camera mainCamera;
    public float cameraOffset = 1.8f;
    public float cameraSpeed = 0.05f;

    public GameObject gameOverPanel;
    public GameObject playPanel;

    public ChallengeManager challengeManager;

    private void Awake()
    {
        spawnPosition = Vector3.zero;
    }
    void Start()
    {
        SpawnRandomPrefab();
        button = GameObject.Find("ChangeBtn").GetComponent<Button>();
        challengeManager = GameObject.Find("ChallengeManager").GetComponent<ChallengeManager>();
        gameOverPanel.SetActive(false);
    }

    private IEnumerator SmoothCameraMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            yield return null;  // 다음 프레임까지 대기
        }

        mainCamera.transform.position = targetPosition; // 마지막 위치 보정
    }

    void Update()
    {
        if (isRotating)
        {
            spawnedObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }

    public void SpawnRandomPrefab()
    {
        animalCount++;
        int randomIndex = Random.Range(0, basicPrefabs.Count);
        spawnedObject = Instantiate(basicPrefabs[randomIndex], transform); // 자식 객체로 프리팹 생성
        spawnedObject.transform.localPosition = spawnPosition;        // 프리팹생성 위치 조정
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;  // 생성 시 kinematic
        }

        ChangeName(basicPrefabs[randomIndex].name);

    }
    public void ChangeAnimal()
    {
        if (changeCount >= maxChangeCount)
        {
            Debug.Log("더 이상 교체할 수 없습니다.");
            return;
        }

        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
        SpawnRandomPrefab();
        changeCount++;
        animalCount--;
        ButtonControl();
    }

    private void ButtonControl()
    {
        if (button != null)
        {
            button.interactable = changeCount < maxChangeCount; // 교체 횟수에 따라 버튼 활성화
        }
    }

    private void ChangeName(string prefabName)
    {
        animalNameTxt.text = prefabName;
    }

    public void ChangeHeight(float totalHeight)
    {
        heightTxt.text = totalHeight.ToString("F2") + " m";
    }

    public void OnRotate()
    {
        if (rotationSet.is45Degree)
        {
            spawnedObject.transform.Rotate(Vector3.forward, -45);
        }
        else
        {
            isRotating = true; // 버튼 누르면 회전 시작
        }

    }

    public void OffRotate()
    {
        isRotating = false; // 버튼 떼면 회전 멈춤
    }

    public void SpawnPointMove()
    {
        spawnPosition = new Vector2(0, totalHeight);
    }

    public void CameraMove()
    {
        Vector3 newCameraPosition = new Vector3(0, totalHeight + cameraOffset, -10);
        StartCoroutine(SmoothCameraMove(newCameraPosition));
    }

    public void GameOver()
    {
        playPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void Initialize()
    {
        // 챌린지 확인
        challengeManager.Challenge_2(totalHeight);
        challengeManager.Challenge_3(animalCount-1);
        challengeManager.Challenge_5();

        // 초기화
        DestroyAllChildren(transform);  // 블록 모두 없애고
        totalHeight = 0f;   // 높이기록 초기화
        ChangeHeight(0f);   // 높이 표시 초기화
        changeCount = 0;    // 교체횟수 초기화
        animalCount = 0;    // 동물 갯수 초기화
        CameraMove();       // 카메라 위치 초기화
        SpawnPointMove();   // 스폰위치 초기화
        SpawnRandomPrefab();

    }
    public void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);  // 자식객체 삭제
        }
    }
}
