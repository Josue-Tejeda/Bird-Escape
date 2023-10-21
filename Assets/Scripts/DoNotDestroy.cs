using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
	private void Awake()
	{
		GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
		GameObject[] gameObj = GameObject.FindGameObjectsWithTag("InGame");
		
        if (gameObj.Length > 0)
        {
            Debug.Log("In GameScene, destroying music object.");
        }
		if (musicObj.Length > 1)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}


}
