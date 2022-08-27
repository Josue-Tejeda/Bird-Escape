using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public float moveXSpeed = 2f;
    public float moveYSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + moveXSpeed * Time.deltaTime, transform.position.y + moveYSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bRight") moveXSpeed *= -1;
        if (collision.gameObject.tag == "bLeft") moveXSpeed = Mathf.Abs(moveXSpeed);
        if (collision.gameObject.tag == "bUp") moveYSpeed *= -1;
        if (collision.gameObject.tag == "bDown") moveYSpeed = Mathf.Abs(moveYSpeed);
    }
}
