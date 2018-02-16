using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform enemyPrefab;

	public Transform spawnPoint;

	public Text waveCountdownText;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	private int waveNumber = 0;

	private void Update() {
		if(countdown <= 0f) {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;

		waveCountdownText.text = Mathf.Round(countdown).ToString();
	}

	private IEnumerator SpawnWave() {
		//Debug.Log("Wave Beginning");

		waveNumber++;
		for (int i = 0; i < waveNumber; i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}		
	}

	private void SpawnEnemy() {
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
