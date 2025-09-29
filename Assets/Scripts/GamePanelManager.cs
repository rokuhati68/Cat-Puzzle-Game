using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GamePanelManager : MonoBehaviour
{
    public GameObject startPanel; // Inspectorで自分自身のStartPanelを入れる
    public GameObject gamePanel;  // InspectorでGamePanelを入れる
    private GameManager gameManager;
    public GameObject clearPanel; // ← これをInspectorで割り当てる
    public AudioSource audioSource;      // Inspector で設定
    public AudioClip ClearSound;
    public bool clearsound = true; // クリア音を鳴らすかどうか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (AllShelvesClosed())
        {
            clearPanel.SetActive(true);
            if (audioSource != null && ClearSound != null && clearsound)
            {
                audioSource.PlayOneShot(ClearSound);
                clearsound = false; // クリア音は一度だけ鳴らす
            }
        }
    }
    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        clearPanel.SetActive(false);
        clearsound = true; // クリア音を再び鳴らせるようにする
    }
    public void ResetAllShelves()
    {
        clearPanel.SetActive(false);
        foreach (Transform child in GameObject.Find("Shelves").transform)
        {
            Shelf shelf = child.GetComponent<Shelf>();
            if (shelf != null)
            {
                shelf.isOpen = true;
                shelf.UpdateVisual();
            }
        }
        clearsound = true; // クリア音を再び鳴らせるようにする

    }
    private bool AllShelvesClosed()
    {
        foreach (Transform child in GameObject.Find("Shelves").transform)
        {
            Shelf shelf = child.GetComponent<Shelf>();
            if (shelf != null && shelf.isOpen)
            {
                return false; // まだ開いてるやつがいる
            }
        }
        return true; // 全部閉じてた！
    }
}