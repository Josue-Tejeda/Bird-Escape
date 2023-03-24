using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_manager : MonoBehaviour
{
    public GameObject targetPrefab;
    public List<GameObject> Targets = new List<GameObject>();

    private bool spawnCooldown = true;
    private bool changeFollowtarget = true;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject target = Instantiate(targetPrefab);
        //Targets.Add(target);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.State == GameState.Playing) targetSpawner();

    }


    private void targetSpawner()
    {
        if (spawnCooldown)
        {
            spawnCooldown = false;
            StartCoroutine(SpawnTargets());
        }

        if (changeFollowtarget)
        {
            changeFollowtarget = false;
            StartCoroutine(SwithFollowTarget());
        }
    }

    private IEnumerator SpawnTargets()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), Random.Range(-3f, 4f));
        GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        target.GetComponent<target>().isFollow = false;
        Targets.Add(target);
        yield return new WaitForSeconds(10f);
        spawnCooldown = true;
    }

    private IEnumerator SwithFollowTarget()
    {
        int targetFollowIndex = Random.Range(0, Targets.Count); 
        Targets[targetFollowIndex].GetComponent<target>().isFollow = true;
        
        for (int i = 0; i < Targets.Count; i++)
        {
            if (i != targetFollowIndex) Targets[i].GetComponent<target>().isFollow = false;
        }
        yield return new WaitForSeconds(Random.Range(14f, 25f));
        changeFollowtarget = true;
    }




}
