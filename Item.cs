using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;                  // item�� ���� ID. �ߺ� �Ұ���
    public string itemName;             // �������� �̸�. �ߺ� ����
    public string itemDescription;      // ������ ����
    public int itemCount;               // ������ ���� ����
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC,
    }

    public int atk;
    public int def;
    public int recover_HP;
    public int recover_MP;


    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _atk = 0, int _def = 0,
                int _recover_HP = 0, int _recover_MP = 0, int _count = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType =  _itemType;
        itemCount = _count;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;

        atk = _atk;
        def = _def;
        recover_HP = _recover_HP;
        recover_MP = _recover_MP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
