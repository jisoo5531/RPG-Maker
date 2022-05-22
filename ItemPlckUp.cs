using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlckUp : MonoBehaviour
{
    public int itemID;
    public int _count;
    public string pickUpSound;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        }
    }
}
