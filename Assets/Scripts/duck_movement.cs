using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_movement : MonoBehaviour
{
    // Move player with click variables
    public float moveSpeed = 10f; 
    Vector2 lastClickPos;
    bool moving;
    
    // Walkin or iddle variables
    bool walkEnded = true;
    float walkingSpeed = 0.8f;
    float position_to_moveX;

    //Animation variables
    public Animator animator;
    bool isWalking;
    bool gameStarted;
    bool startFlying;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get duck position to compare if its walking or standing
        float posX = transform.position.x * Time.deltaTime;
        if (!gameStarted) walkingStart();

        if (transform.position.x * Time.deltaTime == posX) isWalking = false;
        else isWalking = true;

        // Checking when game starts
        if (Input.GetMouseButton(0) && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(gameStarter());
            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(transform.position.x, transform.position.y + 5),
                walkingSpeed * Time.deltaTime);
        }

        if (gameStarted && startFlying) fly();

            

        // Variables to trigger animation
        animator.SetBool("walking", isWalking);
        animator.SetBool("gameStarted", gameStarted);


        // Gettin position to compare if duck its moving
        posX = transform.position.x * Time.deltaTime;
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
        if (transform.position.x > position_to_moveX) characterScale.x = 1;
        if (transform.position.x < position_to_moveX) characterScale.x = -1;
        transform.localScale = characterScale;

        if (walkEnded)
        {
            StartCoroutine(walking());
        } 
    }

    private IEnumerator walking()
    {
        walkEnded = false;
        position_to_moveX = Random.Range(-3,3);
        yield return new WaitForSeconds(Random.Range(3, 8));
        walkEnded = true;
    }

    private IEnumerator gameStarter()
    {
        yield return new WaitForSeconds(0.3f);
        startFlying = true;
        animator.SetBool("startFlying", startFlying);
        transform.position = Vector3.MoveTowards(transform.position, lastClickPos, 3f * Time.deltaTime);
    }

}
