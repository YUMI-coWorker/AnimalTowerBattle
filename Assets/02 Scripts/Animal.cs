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
    public bool isGameOver = false;
    private bool isfalling = false;

    public SpawnManager spawnManager;
    public ScoreManager scoreManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;  // 초기에는 물리 효과를 적용하지 않음
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
                    isfalling = true;
                }

                // 속력이 0이고 움직임이 0이면 다음 프리팹 생성
                if (rb.velocity.magnitude < 0.001f && Mathf.Abs(rb.position.y - transform.position.y) < 0.001f)
                {
                    if (!isfalling) return;
                    //Debug.Log("corouting");
                    StartCoroutine(WaitSpawn());
                }
            }
        }
    }

    private IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(2.5f);
        CompareHeight();    // 최고높이 갱신
        spawnManager.SpawnRandomPrefab();

    }

    private void CompareHeight()
    {
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


}
