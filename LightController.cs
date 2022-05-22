using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerManager thePlayer;    // �÷��̾ �ٶ󺸰� �ִ� ����
    private Vector2 vector;

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = thePlayer.transform.position;
        vector.Set(thePlayer.animatior.GetFloat("DirX"), thePlayer.animatior.GetFloat("DirY"));
    
        if(vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            this.transform.rotation = rotation;
        }
        else if(vector.x == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            this.transform.rotation = rotation;
        }
        else if(vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            this.transform.rotation = rotation;
        }
        else if(vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }
    
    }
}