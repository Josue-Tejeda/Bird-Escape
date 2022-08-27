using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_movement : MonoBehaviour
{
    // Move player with click variables
    public float moveSpeed = 10f;
    Vector2 lastClickPos;
    bool moving;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if (moving && (Vector2)transform.position != lastClickPos)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastClickPos, step);
        } else
        {
            moving = false;
        }

        //Flip character
        Vector3 characterScale = transform.localScale;
        if (transform.position.x > lastClickPos.x) characterScale.x = 1;
        if (transform.position.x < lastClickPos.x) characterScale.x = -1;
        transform.localScale = characterScale;
    }

}
