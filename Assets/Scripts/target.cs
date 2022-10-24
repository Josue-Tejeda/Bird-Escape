using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public float moveXSpeed = 2f;
    public float moveYSpeed = 2f;
    public bool isFollow = false;

    //Shooting variables
    private bool isShooting = false;
    private bool shootCooldown = true;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("duck_player");
    }

    private void Update()
    { 
        if (!isShooting)
        {
            if (isFollow) transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (moveXSpeed * 0.5f) * Time.deltaTime);
            else transform.position = new Vector3(transform.position.x + moveXSpeed * Time.deltaTime, transform.position.y + moveYSpeed * Time.deltaTime);
        }

        if (shootCooldown)
        {
            shootCooldown = false;
            StartCoroutine(shot());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bRight") moveXSpeed *= -1;
        if (collision.gameObject.tag == "bLeft") moveXSpeed = Mathf.Abs(moveXSpeed);
        if (collision.gameObject.tag == "bUp") moveYSpeed *= -1;
        if (collision.gameObject.tag == "bDown") moveYSpeed = Mathf.Abs(moveYSpeed);

        //Kill player if on targe
        if (isShooting && collision.gameObject.tag == "duck_player")
        {
            player.GetComponent<duck_movement>().death();
        }
    }

    private IEnumerator shot()
    {
        isShooting = true;
        yield return new WaitForSeconds(2f);
        isShooting = false;
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        shootCooldown = true;
    }
}
