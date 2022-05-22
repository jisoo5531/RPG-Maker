using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private PlayerStat thePlayerStat;

    public GameObject prefabs_Floating_Text;
    public GameObject parent;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    private void FloatingText(int number, string color)
    {
        Vector3 vector = thePlayerStat.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        if(color == "GREEN")
            clone.GetComponent<FloatingText>().text.color = Color.green;
        else if(color == "BLUE")
            clone.GetComponent<FloatingText>().text.color = Color.blue;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }
    public void UseItem(int _itemID)
    {
        switch (_itemID)
        {
            case 10001:
                if (thePlayerStat.hp >= thePlayerStat.currentHP + 50)
                    thePlayerStat.currentHP += 50;
                else
                    thePlayerStat.currentHP = thePlayerStat.hp;
                FloatingText(50, "GREEN");

                break;
            case 10002:
                if (thePlayerStat.mp >= thePlayerStat.currentMP + 50)
                    thePlayerStat.currentMP += 15;
                else
                    thePlayerStat.currentMP = thePlayerStat.mp;
                FloatingText(15, "BLUE");
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10001, "���� ����", "ü���� 50 ä����", Item.ItemType.Use));
        itemList.Add(new Item(10002, "�Ķ� ����", "������ 15 ä����", Item.ItemType.Use));
        itemList.Add(new Item(10003, "���� ���� ����", "ü���� 350 ä����", Item.ItemType.Use));
        itemList.Add(new Item(10004, "���� �Ķ� ����", "������ 80 ä����", Item.ItemType.Use));
        itemList.Add(new Item(11001, "���� ����", "�������� ������ ���´�. ���� Ȯ���� ��", Item.ItemType.Use));
        itemList.Add(new Item(20001, "ª�� ��", "�⺻���� ����� ��", Item.ItemType.Equip, 3));
        itemList.Add(new Item(20301, "�����̾� ����", "1�п� HP�� 1 ȸ��", Item.ItemType.Equip, 0, 0, 1));
        itemList.Add(new Item(30001, "��� ������ ���� 1", "������ �ɰ��� ��� ������ ����", Item.ItemType.Quest));
        itemList.Add(new Item(30002, "��� ������ ���� 2", "������ �ɰ��� ��� ������ ����", Item.ItemType.Quest));
        itemList.Add(new Item(30003, "��� ����", "ü���� 50 ä����", Item.ItemType.Quest));
    }

    
}
