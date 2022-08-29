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
    public bool isWalking;
    bool gameStarted;
    bool startFlying;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x * Time.deltaTime;
        if (!gameStarted) walkingStart();

        if (transform.position.x * Time.deltaTime == posX) isWalking = false;
        else isWalking = true;

        if (Input.GetMouseButton(0))
        {
            gameStarted = true;
            StartCoroutine(gameStarter());
        } 
        if (gameStarted && startFlying) fly();


        animator.SetBool("walking", isWalking);
        animator.SetBool("gameStarted", gameStarted);

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
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(position_to_moveX, transform.position.y), walkingSpeed * Time.deltaTime);
        
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
