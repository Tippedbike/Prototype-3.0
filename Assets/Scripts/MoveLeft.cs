using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveLeft : MonoBehaviour
{
   private float speed = 12;
   private float leftBound = -15;
   private float sprintSpeed = 20;

   private PlayerController playerControllerScript;
 
   void Start()
   {
       playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

   }
   void Update()
   {
       // stops movement when player touches object
       if (playerControllerScript.gameOver == false)
        {
          transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
       if (playerControllerScript.canDash == true)
        {
          transform.Translate(Vector3.left * Time.deltaTime * sprintSpeed);
        }
        // Destroys objects out of bounds
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
           Destroy(gameObject);
        }
   }
}
