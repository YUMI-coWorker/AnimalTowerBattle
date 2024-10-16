using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isDragging = false;
    public Vector2 offset;

    private bool canInteract = true;    // 조작가능여부
    private bool isGameOver = false;

    public SpawnManager spawnManager;
    public ScoreManager scoreManager;

    public float gameOverHeight = -4f;  // 게임오버 기준 Y값

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;  // 초기에는 물리 효과를 적용하지 않음
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }
    IEnumerator CompareHeight(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (transform.position.y >= spawnManager.totalHeight)
        {
            spawnManager.totalHeight = transform.position.y;
            
        }
        // 총높이 변경
        spawnManager.ChangeHeight(spawnManager.totalHeight);
        // 스폰포인트의 위치 조정
        spawnManager.SpawnPointMove();
        // 카메라의 위치 조정
        spawnManager.CameraMove();
    }
    IEnumerator SpawnPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnManager.SpawnRandomPrefab();
    }
    void Update()
    {
        if(!isGameOver)
        {
            if (canInteract)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // 마우스 클릭 위치에서 Ray 생성
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit.collider != null && hit.transform == transform)
                    {
                        isDragging = true;
                        offset = (Vector2)transform.position - hit.point;
                    }
                }

                if (isDragging)
                {
                    // 마우스 위치를 따라 오브젝트 이동
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 targetPosition = mousePosition + offset;
                    targetPosition.y = transform.position.y; // y축 고정
                    targetPosition.x = Mathf.Clamp(targetPosition.x, -2f, 2f);  // x축 이동 범위 제한
                    transform.position = targetPosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (!isDragging) return;    // 드래깅 상태가 아니라면 함수 종료
                    isDragging = false;
                    rb.isKinematic = false; // 마우스 떼면 물리 효과 적용
                    rb.gravityScale = 1;    // 중력 적용
                    canInteract = false;

                    // 2.8초뒤 오브젝트의 y값을 토탈하이트와 비교해 높은 y값을 토탈하이트로 기록한다.
                    StartCoroutine(CompareHeight(2.8f));
                    // 3초뒤 랜덤프리팹 생성 호출
                    StartCoroutine(SpawnPrefabAfterDelay(3f));
                }
            }

            if (transform.position.y < gameOverHeight)
            {
                isGameOver = true;
                scoreManager.AddScore(SettingPanel.profileNum,SettingPanel.userName, spawnManager.animalCount-1, spawnManager.totalHeight);
                spawnManager.GameOver();
                Time.timeScale = 0;
            }
        }
       
    }


}
