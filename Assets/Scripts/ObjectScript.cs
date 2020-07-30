using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when do we need to run this script? on Start/ on Update/ on something else?
public class ObjectScript : MonoBehaviour
{
		
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	//Create a function to destroy the Block when it is hit by a Bird
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Bird") {
			Destroy(gameObject);
		}
	}
}
