using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody playerRb;
  public float jumpForce;
  public float gravityModifer;
  public bool isOnGround = true;
  public bool gameOver = false;
  private bool canDoubleJump = false; 
  public bool canDash = false; 
  
  private Animator playerAnim;
  public ParticleSystem explosionParticles;
  public ParticleSystem dirtParticle;

  public AudioClip jumpSound;
  public AudioClip crashSound;
  private AudioSource playerAudio; 

   // Start is called before the first frame update
   void Start()
   {
      playerRb = GetComponent<Rigidbody>();
      Physics.gravity *= gravityModifer;
      playerAnim = GetComponent<Animator>();
      playerAudio = GetComponent<AudioSource>();
   }


   // Update is called once per frame
   void Update()
   {
       if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
       {
           playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           isOnGround = false;
           playerAnim.SetTrigger("Jump_trig");
           dirtParticle.Stop();
           playerAudio.PlayOneShot(jumpSound, 1.0f);
           canDoubleJump = true;
       }
       else if ( Input.GetKeyDown(KeyCode.Space) && !gameOver && canDoubleJump )
       {
           playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           isOnGround = false;
           playerAnim.SetTrigger("Jump_trig");
           dirtParticle.Stop();
           playerAudio.PlayOneShot(jumpSound, 1.0f);
           canDoubleJump = false;
       }
        if (Input.GetKey(KeyCode.LeftShift) )
       {
        canDash = true; 
        playerAnim.SetFloat("Speed_Multiplier", 2.0f);
       }
       else if (canDash)
       {
        canDash = false; 
         playerAnim.SetFloat("Speed_Multiplier", 1.0f);
       }
   }
   private void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.CompareTag("Ground"))
       {
          isOnGround = true; 
          dirtParticle.Play();
       }
       else if (collision.gameObject.CompareTag("Obstacle"))
       {
           gameOver = true;
           Debug.Log("Game Over!");
           playerAnim.SetBool("Death_b", true);
           playerAnim.SetInteger("DeathType_int", 1);
           explosionParticles.Play(); 
           dirtParticle.Stop();
           playerAudio.PlayOneShot(crashSound, 1.0f);
       }
      
   }
}