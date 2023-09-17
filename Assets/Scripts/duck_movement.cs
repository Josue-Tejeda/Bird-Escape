using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_movement : MonoBehaviour
{
    // Move player with click variables
    public float moveSpeed = 2.5f;
    Vector2 lastClickPos;
    bool moving;
    bool hasBeenShot;
    bool timeToFall;
    public bool isShot;
    public bool speedUpColdown = true;
    float speedCap = 4f;

    // Walkin or iddle variables
    bool walkEnded = true;
    float walkingSpeed = 0.8f;
    float position_to_moveX;

    //Animation variables
    public Animator animator;
    bool isWalking;
    bool gameStarted;
    bool startFlying;

    //Audio
    public AudioSource AudioSrc;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) walkingStart();

        // Checking when game starts
        if (Input.GetMouseButton(0) && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(gameStarter());
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, transform.position.y + 5),
                walkingSpeed * Time.deltaTime);
        }

        if (gameStarted && startFlying) 
        {
            fly();
            AudioSrc.mute = false;
            if (!hasBeenShot) gameManager.Instance.UpdateGameState(GameState.Playing);
        }

        if (hasBeenShot) Dead();

        if (walkEnded)
        {
            StartCoroutine(walking());
        } 

        // Variables to trigger animation
        animator.SetBool("walking", isWalking);
        animator.SetBool("gameStarted", gameStarted);


        //Workaround for bug (player is no dying if no moving)
        if (isShot) Dead();

        StartCoroutine(isMoving());

        // Speeding game
        if (speedUpColdown && moveSpeed < speedCap) StartCoroutine(speedUp());
    }

    void fly()
    {
        // Move placer with click/tap
        if (Input.GetMouseButton(0))
        {
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if (moving && (Vector2)transform.position != lastClickPos)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastClickPos, step);
        }
        else
        {
            moving = false;
        }

        //Flip character
        Vector3 characterScale = transform.localScale;
        if (transform.position.x > lastClickPos.x) characterScale.x = 1;
        if (transform.position.x < lastClickPos.x) characterScale.x = -1;
        transform.localScale = characterScale;
    }

    void walkingStart()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector2(position_to_moveX, transform.position.y),
            walkingSpeed * Time.deltaTime);

        

        //Flip character
        Vector3 characterScale = transform.localScale;
        if (transform.position.x >= position_to_moveX) characterScale.x = 1;
        if (transform.position.x <= position_to_moveX) characterScale.x = -1;
        transform.localScale = characterScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "target")
        {
            Dead();
        }
    }

    private void Dead()
    {
        AudioSrc.Pause();
        hasBeenShot = true;
        gameManager.Instance.UpdateGameState(GameState.Lose);
        moveSpeed = 0;
        animator.SetBool("hasBeenShot", hasBeenShot);
        StartCoroutine(shot());

        //ScenesAdmin component////////////////////////////KERMIT///////////////////////////////////////
        //It gets function 'GameOver' from 'ScenesAdmin' Script, this function triggers GameOver menu 
        //and sets time scale to 0f
        ScenesAdmin scenesAdmin = FindObjectOfType<ScenesAdmin>();
		
        if (scenesAdmin != null)
        {
			scenesAdmin.counter = false;
			
            IEnumerator GameOverCoroutine()
            {
                yield return new WaitForSeconds(3f);
                scenesAdmin.GameOver();
            }
            Debug.Log("Calling <ScenesAdmin.GameOver()> function");
            StartCoroutine(GameOverCoroutine());
        }
        else
        {
            Debug.LogError("ScenesAdmin not found in scene, busca bien xdd");
        }
        ///////////////////////////////////////////////////KERMIT///////////////////////////////////////
    }

    private IEnumerator walking()
    {
        walkEnded = false;
        position_to_moveX = Random.Range(-2, 3);
        yield return new WaitForSeconds(Random.Range(4, 8));
        walkEnded = true;
    }

    private  IEnumerator isMoving()
    {
        Vector3 starPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 finalPos = transform.position;

        if (starPos.x != finalPos.x) isWalking = true;
        else isWalking = false;
    }

    private IEnumerator gameStarter()
    {
        yield return new WaitForSeconds(0.3f);
        startFlying = true;
        animator.SetBool("startFlying", startFlying);
        transform.position = Vector3.MoveTowards(transform.position, lastClickPos, 3f * Time.deltaTime);
    }

    private IEnumerator shot()
    {
        yield return new WaitForSeconds(0.7f);
        timeToFall = true;
        animator.SetBool("timeToFall", timeToFall);
        rb.gravityScale = 1;
    }

    private IEnumerator speedUp()
    {
        speedUpColdown = false;
        yield return new WaitForSecondsRealtime(35);
        moveSpeed *= 1.2f;
        speedUpColdown = true;
    }
}
