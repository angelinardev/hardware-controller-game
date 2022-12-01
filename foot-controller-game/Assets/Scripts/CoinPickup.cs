using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    ScoreManager score;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            //add score
            score.IncreaseScore(1);
            //delete itself
            Destroy(gameObject);

        }
    }

    private void Start() {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreManager>();
    }
}
