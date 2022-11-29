using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Time.timeScale=1;
        else
            Time.timeScale = 0;
   }
}
