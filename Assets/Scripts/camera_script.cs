using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public float movementSpeed = 0.5f;
    private bool timeToMove = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StarMoving());
    }

    private void FixedUpdate()
    {
        if (timeToMove) MoveCamera();
    }

    private IEnumerator StarMoving()
    {
        yield return new WaitForSeconds(3);
        timeToMove = true;
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, transform.position.z);
    }
}
