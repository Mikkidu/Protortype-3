using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    private Animator playerAnim;
    GameControl _gameControl;

    

    public ParticleSystem explosionParticle, dirtParticle;
    public AudioClip jumpSound, CrushSound;
    private AudioSource playerAudio;
    public float jumpForce = 10f, moveSpeed = 30f;
    public float gravityModifier, dashScore = 0;
    bool isOnGround;
    public bool gameOver = false, gameStart = false;
    int numJumps;
    


    // Присваиваем компонент Rigidbody и настраиваем величину гравитации
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;       
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        _gameControl = GetComponent<GameControl>();
    
    }

    // 
    void Update()
    {
        if (gameStart && !gameOver)
        {
            // Если игрок на земле, то по нажатию Space осуществляется прыжок
            if (Input.GetKeyDown(KeyCode.Space) && numJumps < 2 && !gameOver )
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                numJumps++;
                //isOnGround = false;  
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                //playerAnim.SetBool("Grounded", false);
                playerAudio.PlayOneShot(jumpSound);   
                playerAnim.SetBool("Jump_b", true);        
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && !gameOver)
            {
                moveSpeed *= 1.5f;
                playerAnim.SetFloat("Speed_f", moveSpeed);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) )
            {
                moveSpeed /= 1.5f;
                playerAnim.SetFloat("Speed_f", moveSpeed);
                _gameControl.Score += dashScore;
                Debug.Log($"Dash Bonus: {dashScore.ToString("0")}");
                dashScore = 0;
                
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                dashScore += moveSpeed / 1.5f * Time.deltaTime;
            }
        }
        else if (!gameOver)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime / 2);
            if (transform.position.x > 0)
            {
                gameStart = true;
                playerAnim.SetBool("Static_b", true);
                StartGame();
            }
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если коснулись земли - выставляем флаг на разрешение прыжков
        //Если коснулись препятствия - игра окончена
        if (collision.gameObject.CompareTag("Ground") && !gameOver ) 
        {
            //isOnGround = true;
            numJumps = 0;
            dirtParticle.Play();
            playerAnim.SetBool("Jump_b", false); 
            //playerAnim.SetBool("Grounded", true);
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(CrushSound);
        }
    }

    private void StartGame()
    {
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enabled = true;
        GameObject.Find("Background").GetComponent<MoveLeft>().enabled = true;
        GetComponent<GameControl>().enabled = true;
    }
}
