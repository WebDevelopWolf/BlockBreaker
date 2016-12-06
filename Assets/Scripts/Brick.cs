using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	private LevelManager levelManager;
	private int timesHit;
	private bool isBreakable;
	
	public AudioClip crack;	
	public Sprite[] hitSprites;
	public static int breakableCount = 0;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			breakableCount++;
		}
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D (Collision2D col) { 
		AudioSource.PlayClipAtPoint (crack, transform.position, 10f);
		if (isBreakable) {
			HandleHits();		
		}
	}
	
	void HandleHits() { 
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	void LoadSprites () { 
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	//TODO Remove this method once we can win!
	void SimulateWin () {
		levelManager.LoadNextLevel();
	}
}
