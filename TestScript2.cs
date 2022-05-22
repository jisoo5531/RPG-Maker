using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    private BGM_Manager BGM;

    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGM_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }
    IEnumerator abc()
    {
        BGM.FadeOutMusic();

        yield return new WaitForSeconds(3f);

        BGM.FadeInMusic();
    }
}
