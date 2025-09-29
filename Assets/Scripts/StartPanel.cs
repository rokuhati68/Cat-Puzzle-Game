using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartPanel : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject startPanel; // Inspectorで自分自身のStartPanelを入れる
    public GameObject gamePanel;  // InspectorでGamePanelを入れる
    public GridLayoutGroup shelvesGrid;
    public AudioSource audioSource;      // Inspector で設定
    public AudioClip startSound;
    void Start()
    {
        // 初期化処理が必要な場合はここに記述
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnClickEasy()
    {
        
        GameManager.Instance.SetBoardSize(1, 7);
        shelvesGrid.cellSize = new Vector2(250, 250);
        shelvesGrid.constraintCount = 1;
        StartCoroutine(ShowGamePanel());
    }

    public void OnClickNormal()
    {
        GameManager.Instance.SetBoardSize(3, 5);
        shelvesGrid.cellSize = new Vector2(250, 250);
        shelvesGrid.constraintCount = 3;
        StartCoroutine(ShowGamePanel());
    }

    public void OnClickMedium()
    {
        GameManager.Instance.SetBoardSize(1, 31);
        shelvesGrid.cellSize = new Vector2(200, 200);
        shelvesGrid.constraintCount = 4;
        StartCoroutine(ShowGamePanel());
    }

    public void OnClickHard()
    {
        GameManager.Instance.SetBoardSize(7, 9);
        shelvesGrid.cellSize = new Vector2(150, 150);
        shelvesGrid.constraintCount = 7;
        StartCoroutine(ShowGamePanel());
    }

    public void OnClickExpert()
    {
        GameManager.Instance.SetBoardSize(15, 17);
        shelvesGrid.cellSize = new Vector2(70, 70);
        shelvesGrid.constraintCount = 15;
        StartCoroutine(ShowGamePanel());
    }

    private IEnumerator ShowGamePanel()
    {

        if (audioSource != null && startSound != null)
        {
            audioSource.PlayOneShot(startSound);
            yield return new WaitForSeconds(startSound.length);  // 音声再生後まで待つ
        }
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    
}
