﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject[] UIGameObjects;
	public TMP_Dropdown _qualitysetting, _difficulity;
	private int _quality;

	void Start() {
		if (!PlayerPrefs.HasKey("Chevy")) {
			PlayerPrefs.SetInt("Chevy", 0);
		} else {
			_difficulity.value = PlayerPrefs.GetInt("Chevy");
		}
		if (PlayerPrefs.HasKey("Quality")) {
			_quality = PlayerPrefs.GetInt("Quality");
			_qualitysetting.value = _quality;
			return;
		}
		_quality = QualitySettings.GetQualityLevel();
		_qualitysetting.value = _quality;

	}
	public void Buttons(int ButtonType) {
		switch (ButtonType) {
			case 0: //Quit
				Application.Quit();
				break;
			case 1: //Play
				SceneManager.LoadScene(1);
				break;
			case 2: //Options
				UIGameObjects[0].GetComponent<Animator>().SetBool("Options", false);
				StartCoroutine(DelayedFunction(1, 1f));
				break;
			case 3: //How To Play
				UIGameObjects[0].GetComponent<Animator>().SetBool("Options", false);
				StartCoroutine(DelayedFunction(3, 1f));
				break;
			case 4: //Return To Menu From Options
				UIGameObjects[1].GetComponent<Animator>().SetBool("Options", false);
				StartCoroutine(DelayedFunction(2, 1f));
				break;
			case 5: //Return To Menu From How To Play
				UIGameObjects[2].GetComponent<Animator>().SetBool("HowToPlay", false);
				StartCoroutine(DelayedFunction(2, 1f));
				break;

		}
	}
	public void QualityOption(int QualitySetting) {
		QualitySettings.SetQualityLevel(QualitySetting);
		PlayerPrefs.SetInt("Quality", QualitySetting);
	}
	public void ChevyFilter(int Difficulty) {
		if (Difficulty == 0) {
			PlayerPrefs.SetInt("Chevy", 0);
		} else {
			PlayerPrefs.SetInt("Chevy", 1);
		}
	}

	private IEnumerator DelayedFunction(int type, float delay) {
		yield return new WaitForSeconds(delay);
		switch (type) {
			case 1:
				UIGameObjects[1].GetComponent<Animator>().SetBool("Options", true);
				break;
			case 2:
				UIGameObjects[0].GetComponent<Animator>().SetBool("Options", true);
				break;
			case 3:
				UIGameObjects[2].GetComponent<Animator>().SetBool("HowToPlay", true);
				break;
		}
	}
}