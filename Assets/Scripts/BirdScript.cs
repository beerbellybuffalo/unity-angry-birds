using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	
	public float timeAlive = 5; //default is 5 seconds
	private float createTime;
	
    // Start is called before the first frame update
    void Start()
    {
		//we want Unity to remember the time that the bird was created
        createTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Bird is destroyed when time elapsed exceeds timeAlive
        if (Time.time - createTime > timeAlive) {
			Destroy(gameObject);
		}
    }
}
