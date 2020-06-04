using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
	public int hitsToDestroy;
	
	private GameObject player;
	private int hits = 0;
	private AudioSource audioSource;
		
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.position.y<-10) {//so object is destroyed after falling off the platform
			Destroy(gameObject);
		}
    }
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Bird") {
			hits++;
			AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 0.3f);
			
			//changes colour based on number of hits it has taken
			gameObject.GetComponent<Renderer>().material.color = new Color((float)hits/(float)hitsToDestroy,0,0);

			if (hits >= hitsToDestroy) {
				player.GetComponent<LauncherScript>().ChangePoints(hitsToDestroy);
				
				//if there are no more blocks left, end the game
				if (CheckNumberBlocks() == 1) {
					player.GetComponent<LauncherScript>().EndGame();
				}
				
				Destroy(gameObject);
			}			
		}
	}
	
	int CheckNumberBlocks() {
		return (GameObject.FindGameObjectsWithTag("Wood Block").Length + GameObject.FindGameObjectsWithTag("Steel Block").Length);
	}
}
