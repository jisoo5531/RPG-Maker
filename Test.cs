using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private OrderManager theOrder;
    //private NumberSystem theNumber;
    private DialogueManager theDM;

    public bool flag;
    public string[] texts;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        //theNumber = FindObjectOfType<NumberSystem>();
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }
    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        //theNumber.ShowNumber(correctNumber);
        //yield return new WaitUntil(() => !theNumber.activated);
        theDM.ShowText(texts);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();
    }
}
