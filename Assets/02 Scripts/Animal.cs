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

    private bool canInteract = true;    // ���۰��ɿ���
    public bool isGameOver = false;
    private bool isfalling = false;

    public SpawnManager spawnManager;
    public ScoreManager scoreManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;  // �ʱ⿡�� ���� ȿ���� �������� ����
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
                    // ���콺 Ŭ�� ��ġ���� Ray ����
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit.collider != null && hit.transform == transform)
                    {
                        isDragging = true;
                        offset = (Vector2)transform.position - hit.point;
                    }
                }

                if (isDragging)
                {
                    // ���콺 ��ġ�� ���� ������Ʈ �̵�
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 targetPosition = mousePosition + offset;
                    targetPosition.y = transform.position.y; // y�� ����
                    targetPosition.x = Mathf.Clamp(targetPosition.x, -2f, 2f);  // x�� �̵� ���� ����
                    transform.position = targetPosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (!isDragging) return;    // �巡�� ���°� �ƴ϶�� �Լ� ����
                    isDragging = false;
                    rb.isKinematic = false; // ���콺 ���� ���� ȿ�� ����
                    rb.gravityScale = 1;    // �߷� ����
                    canInteract = false;
                    isfalling = true;
                }

                // �ӷ��� 0�̰� �������� 0�̸� ���� ������ ����
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
        CompareHeight();    // �ְ���� ����
        spawnManager.SpawnRandomPrefab();

    }

    private void CompareHeight()
    {
        if (transform.position.y >= spawnManager.totalHeight)
        {
            spawnManager.totalHeight = transform.position.y;

        }
        // �ѳ��� ����
        spawnManager.ChangeHeight(spawnManager.totalHeight);
        // ��������Ʈ�� ��ġ ����
        spawnManager.SpawnPointMove();
        // ī�޶��� ��ġ ����
        spawnManager.CameraMove();
    }


}
