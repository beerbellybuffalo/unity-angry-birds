using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LauncherScript : MonoBehaviour
{
	public GameObject birdPrefab;	
	public float firingSensitivity;
	public float fireInterval;
	public float maxTimeToHoldFire;
	public int maxBirdsToCreate;
	
	private float initialTime;
	private float fireTiming;
	private int birdsCreated = 0;
	private bool isFiring = false;
	private bool endGame = false;
	
	private int points = 0;
	private Text pointsTrackerText;
	private Text birdsTrackerText;
	private Text fireTrackerText;
	private Animator firepowerTrackerAnimator;
	
	
    // Start is called before the first frame update
    void Start()
    {
		fireTiming = -fireInterval;//set this so that canfire will return true at start of game
		pointsTrackerText = GameObject.FindGameObjectWithTag("PointsTracker").GetComponent<Text>();
		pointsTrackerText.text = "Points: " + points.ToString();
		birdsTrackerText = GameObject.FindGameObjectWithTag("BirdsTracker").GetComponent<Text>();
		birdsTrackerText.text = "Birds Remaining: " + maxBirdsToCreate.ToString() + "/" + maxBirdsToCreate.ToString();
		fireTrackerText = GameObject.FindGameObjectWithTag("FireTracker").GetComponent<Text>();
		
		firepowerTrackerAnimator = GameObject.FindGameObjectWithTag("FirepowerBar").GetComponent<Animator>();
		firepowerTrackerAnimator.speed = (1/maxTimeToHoldFire);
    }

    // Update is called once per frame
    void Update()
    {
		if (endGame) {
			return;
		}
		
		if (!CanFire()) {
			fireTrackerText.text = "Reloading...";
			return;
		}
		else if (isFiring) {
			fireTrackerText.text = "";
		}
		else {
			fireTrackerText.text = "Ready to Fire!!!";
		}
		
		//checking for mouseclick down to fire a bird
		if (Input.GetMouseButtonDown(0) == true) {
			//get vector of mouse position
			initialTime = Time.time;
			isFiring = true; //this bool is here because when you spam click the mouse button, you can GetMouseButtonUp to trigger but not GetMouseButtonDown sometimes
			
			//play animation
			firepowerTrackerAnimator.SetBool("isFiring", true);
		}
		
		//when mouse button is released, create and fire a bird
		else if (Input.GetMouseButtonUp(0) == true && isFiring) {
			
			//spawn the bird
			birdsCreated++;
			GameObject birdGameObject = Instantiate(birdPrefab, transform.position, transform.rotation);
			
			//adjust the ui
			birdsTrackerText.text = "Birds Remaining: " + (maxBirdsToCreate - birdsCreated).ToString() + "/" + maxBirdsToCreate.ToString();
			firepowerTrackerAnimator.SetBool("isFiring", false);
			firepowerTrackerAnimator.Play("Empty");
			
			//adjusting the position of the bird
			birdGameObject.transform.position -= transform.forward * 1f;
			birdGameObject.transform.position += transform.up * 1f;
			
			//check for how much force to fire the bird with
			float firingForceMultiplier;
			if (Time.time-initialTime > maxTimeToHoldFire) {
				firingForceMultiplier = maxTimeToHoldFire * firingSensitivity;
			}
			else {
				firingForceMultiplier = (Time.time - initialTime) * firingSensitivity;
			}
			
			//firing the bird
			Vector3 firingVector = transform.forward + Vector3.up;		
			birdGameObject.GetComponent<Rigidbody>().AddForce(firingVector * firingForceMultiplier, ForceMode.Impulse);
						
			//if the player runs out of birds, end the game
			if (birdsCreated == maxBirdsToCreate) {
				EndGame();
			}
			
			//moved checking for no blocks left into object script
			
			//sets the time that you fired the bird - there's an interval before you can shoot again
			fireTiming = Time.time;
			isFiring = false;
		}
    }
	
	bool CanFire() {
		if (((Time.time - fireTiming) > fireInterval)) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public void ChangePoints(int change) {
		points += change;
		pointsTrackerText.text = "Points: " + points.ToString();
	}
	
	public void EndGame() {
		endGame = true;
		SceneManager.LoadScene("EndGameScene");
	}
}
