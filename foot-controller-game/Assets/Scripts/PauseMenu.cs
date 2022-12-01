using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public Canvas menuUI;
    // Start is called before the first frame update
    void Start()
    {
        menuUI.enabled = false;
    }

   public void ChangeUI()
   {
        menuUI.enabled = !menuUI.enabled;
        if (menuUI.enabled)
           { 
                Time.timeScale=0;
           }
        else
           { 
                Time.timeScale = 1;
           }
   }

   public void Restart()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
