using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    //Target variables and components
    public float moveXSpeed = 2f;
    public float moveYSpeed = 2f;
    public bool isFollow = false;
    private CircleCollider2D circleCollider;

    //Shooting variables
    private bool isShooting = false;
    private bool shootCooldown = true;
    private GameObject player;

    //Limit position
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.Find("duck_player");
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
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

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.0) moveXSpeed = Mathf.Abs(moveXSpeed);
        if (1.0 < pos.x) moveXSpeed *= -1;
        if (pos.y < 0.0) moveYSpeed = Mathf.Abs(moveYSpeed);
        if (1.0 < pos.y) moveYSpeed *= -1;
    }

      private IEnumerator shot()
    {
        isShooting = true;
        circleCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        circleCollider.enabled = false;
        yield return new WaitForSeconds(0.8f);
        isShooting = false;
        yield return new WaitForSeconds(Random.Range(2f, 6f));
        shootCooldown = true;
    }
}
