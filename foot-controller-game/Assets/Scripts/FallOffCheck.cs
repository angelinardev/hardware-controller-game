using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffCheck : MonoBehaviour
{
    public Canvas menu;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            menu.enabled = true;
            Time.timeScale = 0;
        }
    }
    private void Start() {
        Time.timeScale = 1;
        menu.enabled = false;
    }
}
