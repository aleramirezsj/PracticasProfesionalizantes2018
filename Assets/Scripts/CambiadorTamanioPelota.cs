using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorTamanioPelota : MonoBehaviour {

	private float escalaActualPelota;
	public Slider sldTamanioPelota;
	public Transform pelota;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (sldTamanioPelota.value!=escalaActualPelota)
		{
			pelota.transform.localScale=new Vector3(sldTamanioPelota.value,sldTamanioPelota.value,sldTamanioPelota.value);
			escalaActualPelota=sldTamanioPelota.value;
		}		
	}
	
}
