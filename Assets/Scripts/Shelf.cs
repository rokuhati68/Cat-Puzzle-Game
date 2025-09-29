using UnityEngine;
using UnityEngine.UI;
public class Shelf : MonoBehaviour
{
    public int shelfID; // 棚のID
    public bool isOpen; // 棚が開いているかどうか
    public Shelf parent;
    public Shelf leftChild;
    public Shelf rightChild;
    [Header("棚のUI要素")]
    public Sprite openSprite; // 開いているときのスプライト
    public Sprite closeSprite; // 閉じているときのスプライト

    public Image shelfImage; // 棚のイメージ
    public Sprite cat1_true;
    public Sprite cat1_false;
    public Sprite cat2_true;
    public Sprite cat2_false;
    public Sprite cat3_true;
    public Sprite cat3_false;
    int catType;
    public AudioClip[] nyattClips; // 5つの「にゃ」音声を入れる
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialized(int id)
    {
        shelfID = id;
        isOpen = true; // 初期状態は開いている
        // ここで棚の初期化処理を行う
        // 例えば、棚の名前を設定するなど
        gameObject.name = "Shelf_" + shelfID;
        shelfImage = GetComponentInChildren<Image>();  // 子のImageも対応
        catType = Random.Range(1, 4);
        UpdateImage();
        // UIの初期化


    }

    public void UpdateVisual()
    {
        if (shelfImage == null) return;

        if (isOpen && openSprite != null)
        {
            shelfImage.sprite = openSprite;
        }
        else if (!isOpen && closeSprite != null)
        {
            shelfImage.sprite = closeSprite;
        }
    }

    public void OnClick()
    {
        if (!isOpen) return;
        // 棚が開いている場合は閉じる、閉じている場合は開く
        isOpen = false;
        UpdateVisual();
        PlayRandomNyatt();

        // 親棚がある場合、親棚の状態も更新
        if (parent != null)
        {
            parent.isOpen = true; // 親棚を開く
            parent.UpdateVisual();
        }
        if (leftChild != null && rightChild != null)
        {
            bool leftClosed = !leftChild.isOpen;
            bool rightClosed = !rightChild.isOpen;

            // どちらか一方でも開いている → 両方開ける
            if (!leftClosed || !rightClosed)
            {
                leftChild.isOpen = true;
                rightChild.isOpen = true;

                leftChild.UpdateVisual();
                rightChild.UpdateVisual();
            }
        }
    }
    void UpdateImage()
    {
        if (catType == 1)
        {
            shelfImage.sprite = isOpen ? cat1_false : cat1_true;
            openSprite = cat1_false;
            closeSprite = cat1_true;

        }
        else if (catType == 2)
        {
            shelfImage.sprite = isOpen ? cat2_false : cat2_true;
            openSprite = cat2_false;
            closeSprite = cat2_true;
        }
        else
        {
            shelfImage.sprite = isOpen ? cat3_false : cat3_true;
            openSprite = cat3_false;
            closeSprite = cat3_true;
        }
    }
    public void PlayRandomNyatt()
    {
        if (nyattClips.Length == 0 || audioSource == null) return;

        int index = Random.Range(0, nyattClips.Length);
        audioSource.PlayOneShot(nyattClips[index]);
    }
}
