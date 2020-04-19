using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUP : MonoBehaviour
{
    public float multiplier = 2f;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        gameController.AddScore(scoreValue);
        Destroy(gameObject);

        
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
        

    }

    void Pickup(Collider player)
    {
        PlayerController speed = player.GetComponent<PlayerController>();
        speed.speed *= multiplier;
    }
}
