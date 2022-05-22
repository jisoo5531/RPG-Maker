using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    private PlayerManager thePlayer;    // 이벤트 도중 키입력 처리 방지
    private List<MovingObj> characters;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }
    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void PreLoadCharacter()
    {
        characters = ToList();
    }

    // 리스트 채우기
    public List<MovingObj> ToList()
    {
        List<MovingObj> tempList = new List<MovingObj>();
        MovingObj[] temp = FindObjectsOfType<MovingObj>();

        for(int i = 0; i < temp.Length; ++i)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void Move(string _name, string _dir)
    {
        for(int i = 0; i < characters.Count; ++i)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void SetTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animatior.SetFloat("DirX", 0);
                characters[i].animatior.SetFloat("DirY", 0);
                switch (_dir)
                {    
                    case "UP":
                        characters[i].animatior.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animatior.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        characters[i].animatior.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animatior.SetFloat("DirX", 1f);
                        break;

                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
