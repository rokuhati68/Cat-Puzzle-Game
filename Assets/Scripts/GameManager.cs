using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("棚のサイズ指定")]
    public int row;//縦
    public int col;//横
    public GameObject shelfPrefub;
    public Transform shelves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int shelfCount = row * col;
        List<int> shelfIDs = new List<int>();
        List<Shelf> shelfInstances = new List<Shelf>();
        for (int i = 0; i < shelfCount; i++)
        {
            shelfIDs.Add(i);
        }
        //シャッフル
        Shuffle(shelfIDs);
        //棚生成
        for (int i = 0; i < shelfCount; i++)
        {
            GameObject obj = Instantiate(shelfPrefub, shelves);
            Shelf shelf = obj.GetComponent<Shelf>();
            shelf.Initialized(shelfIDs[i]);
            shelfInstances.Add(shelf);

        }
        //親棚の設定
        int count = shelfInstances.Count;

        // 親ノードの設定
        for (int i = 0; i < count; i++)
        {
            Shelf child = shelfInstances[i];
            int id = child.shelfID;
            if (id == 0)
            {
                child.parent = null; // ルートノードは親なし
            }
            else
            {
                int parentID = (id - 1) / 2;
                Shelf parent = shelfInstances.Find(s => s.shelfID == parentID);
                if (parent != null)
                {
                    child.parent = parent;
                }
            }
        }

        // 🔽 子ノードの設定（外に出す！）
        for (int j = 0; j < count; j++)
        {
            Shelf parent = shelfInstances[j];
            int id = parent.shelfID;

            int leftID = 2 * id + 1;
            int rightID = 2 * id + 2;

            Shelf leftChild = shelfInstances.Find(s => s.shelfID == leftID);
            Shelf rightChild = shelfInstances.Find(s => s.shelfID == rightID);

            if (leftChild != null) parent.leftChild = leftChild;
            if (rightChild != null) parent.rightChild = rightChild;
        }

    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }
    
    public void SetBoardSize(int r, int c)
    {
        row = r;
        col = c;

        // 再生成のために一度全て削除
        foreach (Transform child in shelves)
        {
            Destroy(child.gameObject);
        }
        Start();
    }

}
