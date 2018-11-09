using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorDeTiempoDeColor : MonoBehaviour {

	public Text TxtTiempoDeColor;
	private int tiempoDeColor;
	public Slider sldTiempoDeColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldTiempoDeColor.value!=tiempoDeColor)
		{
			tiempoDeColor=(int)sldTiempoDeColor.value;
			TxtTiempoDeColor.text=tiempoDeColor.ToString();
		}
	}

	

}
