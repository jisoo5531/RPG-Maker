using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    
    public string transferSceneName;

    private PlayerManager thePlayer;

    
 


    // Start is called before the first frame update
    void Start()
    {
        
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        thePlayer.currentSceneName = transferSceneName;
        SceneManager.LoadScene(transferSceneName);      // æ¿ ¿Ãµø
    }
    
}
