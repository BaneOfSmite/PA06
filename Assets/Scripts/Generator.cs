﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

	public float TimeLeft = 30;
	private bool inProgress, isDone;
	public RectTransform[] UIComponents;

	void Update() {
		if (transform.GetChild(0).GetComponent<TextMesh>() != null) {
			if (GameObject.FindGameObjectWithTag("Player") != null) {
				transform.GetChild(0).LookAt(2 * (new Vector3(transform.position.x, 1.5f, transform.position.z)) - (new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, 0, GameObject.FindGameObjectWithTag("Player").transform.position.z)));
			}
		}
		if (TimeLeft > 0 && inProgress) {
			TimeLeft -= Time.deltaTime;
			UIComponents[1].localPosition  = new Vector3((-450f + (30f - TimeLeft) * 15f), 0,0);
			transform.GetChild(0).GetComponent<TextMesh>().text = Mathf.FloorToInt(TimeLeft).ToString();
		}
		if (TimeLeft <= 0 && !isDone) {
			isDone = !isDone;
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GeneratorLeft -= 1;
			transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().Play();
			Destroy(transform.GetChild(0).gameObject);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			if (transform.GetChild(0).name == "TimeLeft") {
				transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
				UIComponents[0].gameObject.SetActive(true);
			}
			inProgress = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			UIComponents[0].gameObject.SetActive(false);
			if (transform.GetChild(0).name == "TimeLeft") {
				transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
			}
			inProgress = false;
		}
	}
}
