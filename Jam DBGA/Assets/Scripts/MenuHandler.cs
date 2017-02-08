using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {


	public List <GameObject> playerButtons;
	public List <GameObject> gameModesButtons;
	private Color32 deactivatedButtonsColor = new Color32 (200, 200, 200, 155);

	// Use this for initialization
	void Start () {
		//Evidenzia automaticamente 1 player
		for (int i = 1; i < playerButtons.Count; i++) {
			playerButtons [i].GetComponent<Image> ().color = deactivatedButtonsColor; // .interactable = false;
		}


		//Evidenzia automaticamente le modalità disponibili per 1 player
		for (int i = 0; i < playerButtons.Count; i++) {
			if(i != 0){
				gameModesButtons [i].GetComponent<Button> ().interactable = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnePlayer () {
		SceneController.Self.numberPlayerFromMenu = 1;
		//Evidenzia player 1
		for (int i = 0; i < playerButtons.Count; i++) {
			if (i != 0) {
				playerButtons [i].GetComponent<Image> ().color = deactivatedButtonsColor;
			} else {
				playerButtons [i].GetComponent<Image> ().color = Color.white;
			}
		}

		//Rende selezionabili le modalità
		for (int i = 0; i < playerButtons.Count; i++) {
			if (i != 0) {
				gameModesButtons [i].GetComponent<Button> ().interactable = false;
			} else {
				gameModesButtons [i].GetComponent<Button> ().interactable = true;
			}
		}

	}


	public void TwoPlayer () {
		SceneController.Self.numberPlayerFromMenu = 2;

		//Evidenzia player 1
		for (int i = 0; i < playerButtons.Count; i++) {
			if (i != 1) {
				playerButtons [i].GetComponent<Image> ().color = deactivatedButtonsColor;
			} else {
				playerButtons [i].GetComponent<Image> ().color = Color.white;
			}
		}

		//Rende selezionabili le modalità
		gameModesButtons [0].GetComponent<Button> ().interactable = true;
		gameModesButtons [1].GetComponent<Button> ().interactable = false;
		gameModesButtons [2].GetComponent<Button> ().interactable = true;
		gameModesButtons [3].GetComponent<Button> ().interactable = true;

	}


	public void ThreePlayer () {
		SceneController.Self.numberPlayerFromMenu = 3;

		//Evidenzia player 1
		for (int i = 0; i < playerButtons.Count; i++) {
			if (i != 2) {
				playerButtons [i].GetComponent<Image> ().color = deactivatedButtonsColor;
			} else {
				playerButtons [i].GetComponent<Image> ().color = Color.white;
			}
		}

		//Rende selezionabili le modalità
		for (int i = 0; i < playerButtons.Count; i++) {
			gameModesButtons [i].GetComponent<Button> ().interactable = true;
		}
	}


	public void FourPlayer () {
		SceneController.Self.numberPlayerFromMenu = 4;

		//Evidenzia player 1
		for (int i = 0; i < playerButtons.Count; i++) {
			if (i != 3) {
				playerButtons [i].GetComponent<Image> ().color = deactivatedButtonsColor;
			} else {
				playerButtons [i].GetComponent<Image> ().color = Color.white;
			}
		}

		//Rende selezionabili le modalità
		for (int i = 0; i < playerButtons.Count; i++) {
			gameModesButtons [i].GetComponent<Button> ().interactable = true;
		}
	}

}
