using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    //public GameObject[] prefabs;    // ������ �迭
    public List<GameObject> basicPrefabs = new List<GameObject>(); 
    public Vector2 spawnPosition;   // ���� ��ġ
    public GameObject spawnedObject;

    public int changeCount = 0;    // ��üȽ��
    private int maxChangeCount = 2; // �ִ뱳üȽ��
    private int remainCount = 2;    // ���� Ƚ��
    public Text remainCnt;
    public UnityEngine.UI.Button button;

    public float rotationSpeed = 100f;  // ȸ���ӵ�
    public bool isRotating = false;     // ȸ������
    public SettingPanel rotationSet;     // ȸ������

    public float totalHeight = 0f;  // ���� ����
    public Text heightTxt;

    public int animalCount = 0;    // ���� ����

    public Text animalNameTxt;

    public Camera mainCamera;
    public float cameraOffset = 1.8f;
    public float cameraSpeed = 0.05f;

    public ChallengeManager challengeManager;
    public SoundManager soundManager;
    public AudioSource audioSource;

    public GameObject changeEffect; // ��ü ����Ʈ

    private void Awake()
    {
        spawnPosition = Vector3.zero;
    }
    void Start()
    {
        SpawnRandomPrefab();
        challengeManager = GameObject.Find("ChallengeManager").GetComponent<ChallengeManager>();
    }

    private IEnumerator SmoothCameraMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            yield return null;  // ���� �����ӱ��� ���
        }

        mainCamera.transform.position = targetPosition; // ������ ��ġ ����
    }

    void Update()
    {
        if (isRotating)
        {
            spawnedObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);

            if(!audioSource.isPlaying)
            {
                soundManager.PlaySound(1); // ȿ�������
            }
            
        }
        
    }

    public void SpawnRandomPrefab()
    {
        animalCount++;
        int randomIndex = Random.Range(0, basicPrefabs.Count);
        spawnedObject = Instantiate(basicPrefabs[randomIndex], transform); // �ڽ� ��ü�� ������ ����
        spawnedObject.transform.localPosition = spawnPosition;        // �����ջ��� ��ġ ����
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;  // ���� �� kinematic
        }

        ChangeName(basicPrefabs[randomIndex].name);

    }
    public void ChangeAnimal()
    {
        if (changeCount >= maxChangeCount)
        {
            Debug.Log("�� �̻� ��ü�� �� �����ϴ�.");
            return;
        }

        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
        SpawnRandomPrefab();
        soundManager.PlaySound(2);      // ȿ����
        changeEffect.transform.position = spawnedObject.transform.localPosition + new Vector3(0,3,0);        // ����Ʈ ��ġ
        changeEffect.GetComponent<ParticleSystem>().Play();     // ����Ʈ ���
        changeCount++;
        animalCount--;
        remainCount--;
        remainCnt.text = remainCount.ToString();
        ButtonControl();
    }

    private void ButtonControl()
    {
        if (button != null)
        {
            button.interactable = changeCount < maxChangeCount; // ��ü Ƚ���� ���� ��ư Ȱ��ȭ
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
            soundManager.PlaySound(1);
        }
        else
        {
            isRotating = true; // ��ư ������ ȸ�� ����
        }

    }

    public void OffRotate()
    {
        isRotating = false; // ��ư ���� ȸ�� ����
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

    public void Initialize()
    {
        // ç���� Ȯ��
        challengeManager.Challenge_2(totalHeight);
        challengeManager.Challenge_3(animalCount-1);
        challengeManager.Challenge_5();

        // �ʱ�ȭ
        DestroyAllChildren(transform);  // ��� ��� ���ְ�
        totalHeight = 0f;   // ���̱�� �ʱ�ȭ
        ChangeHeight(0f);   // ���� ǥ�� �ʱ�ȭ
        changeCount = 0;    // ��üȽ�� �ʱ�ȭ
        remainCount = 2;    // ������üȽ�� �ʱ�ȭ
        remainCnt.text = remainCount.ToString();    // ���� ��üȽ�� ǥ��
        button.interactable = true;     // ��ư Ȱ��ȭ
        animalCount = 0;    // ���� ���� �ʱ�ȭ
        CameraMove();       // ī�޶� ��ġ �ʱ�ȭ
        SpawnPointMove();   // ������ġ �ʱ�ȭ
        SpawnRandomPrefab();

    }
    public void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);  // �ڽİ�ü ����
        }
    }
}
