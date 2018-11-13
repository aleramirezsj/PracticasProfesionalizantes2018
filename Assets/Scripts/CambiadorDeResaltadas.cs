using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorDeResaltadas : MonoBehaviour {

	public Text TxtCantidadResaltadas;
	public Slider sldCantidadResaltadas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldCantidadResaltadas.value!=ComportamientoPelota.cantidadResaltadas)
		{
			ComportamientoPelota.cantidadResaltadas=(int)sldCantidadResaltadas.value;
			TxtCantidadResaltadas.text=ComportamientoPelota.cantidadResaltadas.ToString();
        }
	}

	

}
