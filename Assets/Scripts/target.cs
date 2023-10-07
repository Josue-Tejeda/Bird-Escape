using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    //Target variables and components
    public float moveXSpeed = 3f;
    public float moveYSpeed = 3f;
    public bool isFollow = false;
    private CircleCollider2D targetCollider;

    //Shooting variables
    private bool isShooting = false;
    private bool shootCooldown = true;
    private GameObject player;
    private bool starCool = false;
    private bool speedUpColdown = true;
    private float speedCap = 6f;

    //Audio
    public AudioSource src;
    public AudioClip shotSfx;

    //Limit position
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.Find("duck_player");
        targetCollider = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine(startColdown());
    }

    private void Update()
    {
        if (speedUpColdown && moveXSpeed < speedCap) StartCoroutine(speedUp());

        if (gameManager.Instance.State == GameState.Lose) StartCoroutine(destroyTarget());

        //Workaround for bug (player is no dying if no moving)
        if (isShooting && transform.position == player.transform.position) player.GetComponent<duck_movement>().isShot = true;
    }

    private void FixedUpdate()
    {
        if (starCool) TargetLogic();
    }
    private void TargetLogic()
     {
        if (!isShooting)
        {
            if (isFollow)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (moveXSpeed * 0.6f) * Time.deltaTime);
                if (player.transform.position == transform.position) transform.position = new Vector3(transform.position.x + 0.0001f, transform.position.y);
            }
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
        src.clip = shotSfx;
        src.Play();
        targetCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        targetCollider.enabled = false;
        isShooting = false;
        yield return new WaitForSeconds(Random.Range(2f, 6f));
        shootCooldown = true;
    }

    private IEnumerator startColdown()
    {
        yield return new WaitForSeconds(1.5f);
        starCool = true;
    }

    private IEnumerator destroyTarget()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    private IEnumerator speedUp()
    {
        speedUpColdown = false;
        yield return new WaitForSecondsRealtime(35);
        moveXSpeed *= 1.1f;
        moveYSpeed *= 1.1f;
        speedUpColdown = true;
    }

}
