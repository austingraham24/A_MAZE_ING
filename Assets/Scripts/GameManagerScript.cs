using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
	public readonly int catLayer = 12;
	public readonly int mouseLayer = 13;
	public readonly int catWallsLayer = 10;
	public readonly int mouseWallsLayer = 9;
	public readonly int pickupLayer = 11;

	public Transform mouseSpawn;
	public Transform[] catSpawns;

	public Dictionary<string, Color[]> colors = new Dictionary<string, Color[]>();

	private string winner;
	private int itemsDelivered;
	private bool gameOver;

	public MouseScript mouse;
	public CatScript cat;
	private GameObject canvas;
	private GameObject winBox;
	private GameObject winText;
	private GameObject recoveredText;
	private Text livesText;

	public GameObject[] falseWalls;
	public GameObject gameUI;
	public GameObject startUI;
	public GameObject startCam;

	public GameObject Cheeses;
	public PickupScript[] pickups;

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision(catLayer,mouseWallsLayer);
		Physics.IgnoreLayerCollision(catLayer,pickupLayer);
		Physics.IgnoreLayerCollision(mouseLayer,catWallsLayer);
		colors.Add("green", new Color[] {new Color(0.258f, 0.850f, 0.325f, 1), new Color(0f, 1f, 0.047f,1)}); //66, 217, 83 && 0,255,12
		colors.Add("red", new Color[] {new Color(0.639f, 0.011f, 0f, 1), new Color(1f, 0.058f, 0.039f,1)});
		colors.Add("blue", new Color[] {new Color(0f, 0.137f, 0.839f, 1), new Color(0.141f, 0.705f, 1f,1)});
		colors.Add("yellow", new Color[] {new Color(1f, 0.97f, 0f, 1), new Color(0.639f, 0.619f, 0,1f)});
		canvas = gameUI;
		winBox = canvas.transform.GetChild(3).gameObject;
		livesText = canvas.transform.GetChild(4).gameObject.GetComponent<Text>();
		winText = winBox.transform.GetChild(1).gameObject;
		recoveredText = winBox.transform.GetChild(2).gameObject;

		Setup();
		HideControls();
	}

	void Setup() {
		winner = null;
		itemsDelivered = 0;
		gameOver = false;
		mouse.lives = 3;
		livesText.text = "Lives: " + mouse.lives;
		mouse.gameObject.transform.position = mouseSpawn.position;
		mouse.gameObject.transform.rotation = mouseSpawn.rotation;
		var randomNum = Random.value;
		var catStartPosition = 0;
		if (randomNum > 0.5) {
			catStartPosition = 1;
		}
		else if (randomNum > 0.8) {
			catStartPosition = 2;
		}
		cat.gameObject.transform.position = catSpawns[catStartPosition].position;
		cat.gameObject.transform.rotation = catSpawns[catStartPosition].rotation;
		winBox.active = false;
		gameUI.active = false;
		startUI.active = true;
		startCam.active = true;
		cat.gameObject.GetComponent<MovementScript>().enabled = false;
		mouse.gameObject.GetComponent<MovementScript>().enabled = false;

		foreach (Transform cheese in Cheeses.transform) {
			var image = cheese.gameObject.GetComponent<Image>();
			image.fillAmount = 0;
		}

		foreach (PickupScript pickup in pickups) {
			pickup.reset();
		}

		foreach (Transform mouseItem in mouse.livesDisplay) {
			mouseItem.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
	}

	public void StartGame() {
		gameUI.active = true;
		startUI.active = false;
		startCam.active = false;
		cat.gameObject.GetComponent<MovementScript>().enabled = true;
		mouse.gameObject.GetComponent<MovementScript>().enabled = true;
	}

	public void ShowControls() {
		var controlUI = startUI.transform.GetChild(0).Find("Info").gameObject;
		if (controlUI) {
			controlUI.active = true;
		}
	}

	public void HideControls() {
		var controlUI = startUI.transform.GetChild(0).Find("Info").gameObject;
		if (controlUI) {
			controlUI.active = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (itemsDelivered == 4) {
			winner = "The Mouse";
			gameOver = true;
			}
			else if (mouse.lives <= 0) {
				print("Mouse Died");
				gameOver = true;
				winner = "The Cat";
			}
			if (gameOver) {
				winText.GetComponent<Text>().text = winner + " Wins!";
				recoveredText.GetComponent<Text>().text = "Recovered Items: " + itemsDelivered;
				winBox.active = true;
			}
		}
		else {
			if (Input.GetKeyUp(KeyCode.R)) {
				print("Restart");
				foreach (GameObject wall in falseWalls) {
					wall.GetComponent<FalseWallScript>().reset();
				}
				Setup();
			}
		}
	}

	public void deliverItem() {
		itemsDelivered += 1;
		//falseWalls[itemsDelivered - 1].GetComponent<FalseWallScript>().move();
	}

	public void removeItem() {
		itemsDelivered -= 1;
	}
}
