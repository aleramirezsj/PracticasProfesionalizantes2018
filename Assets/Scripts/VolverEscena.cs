using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VolverEscena : MonoBehaviour {
	
	public GameObject pelota;

	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
		pelota.GetComponent<ComportamientoPelota>().juegoIniciado=false;
		ComportamientoPelota.finalizarJuego=false;
		ComportamientoPelota.instancias=0;

	}

	void Start () {
		
	}

	void OnDisable()
	{
		

	}
}
