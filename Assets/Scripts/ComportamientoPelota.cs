﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System;

public class ComportamientoPelota : MonoBehaviour {
	//public static ComportamientoPelota Instance {get; set;}
	public static bool juegoIniciado= false;
	public Rigidbody2D rbBall;
	public Canvas canvas;
	private int cantidadTotalPelotas;
	private float escalaActualPelota;
	private int velocidadPelotasActual;
	public static int tiempoDeColor;
	public static int tiempoDeInicio;
	public static int instancias;
	public Text txtNombreJugador;
	public Text txtTiempoDeInicio;
	public static Color colorOriginal;
	int segundos=0;
	float contadorDeSegundos=0;
	float medidaDeTiempo=1;
	bool iniciarInmediatamente;
	static public bool finalizarJuego;
	bool continuarRebotes;
	static public int cantidadResaltadas;
	static public int cantidadEncontradas;
	public GameObject pelota;
	public static List<GameObject> pelotasInstanciadas=new List<GameObject>();
	

    void Start () {
		Logs();
		//almaceno el color original en la propiedad estática 
		if(instancias==0)
			colorOriginal=GetComponent<Renderer>().material.color;
		GameObject pelotaInstanciada;
		//transform.position=Camera.main.ScreenToWorldPoint(posicionAleatoria);
		transform.position=obtenerPosicionAleatoria();
		transform.localScale=new Vector3(escalaActualPelota,escalaActualPelota,escalaActualPelota);
		if(instancias<cantidadTotalPelotas-1){
			for(int i=0;i<cantidadTotalPelotas-1;i++){
				pelotaInstanciada=Instantiate(pelota, obtenerPosicionAleatoria(), transform.rotation) as GameObject;
				instancias++;
				if(cantidadTotalPelotas-instancias<=cantidadResaltadas){
					pelotaInstanciada.tag="Resaltada";
					pelotaInstanciada.GetComponent<Renderer>().material.color = Color.red;
				}
				pelotasInstanciadas.Add(pelotaInstanciada);
			}
		}
		
	}



    void OnEnable()
	{
		cantidadTotalPelotas=PlayerPrefs.GetInt("cantidadTotalPelotas");
		escalaActualPelota=PlayerPrefs.GetFloat("escalaActualPelota");
		velocidadPelotasActual=PlayerPrefs.GetInt("velocidadPelotasActual")*2;
		txtNombreJugador.text=PlayerPrefs.GetString("nombreJugador");
		iniciarInmediatamente=(PlayerPrefs.GetInt("iniciarInmediatamente"))==1;
		tiempoDeColor=PlayerPrefs.GetInt("tiempoDeColor");
		tiempoDeInicio=PlayerPrefs.GetInt("tiempoDeInicio");
		continuarRebotes=(PlayerPrefs.GetInt("chkContinuarRebotes"))==1;
		cantidadResaltadas=PlayerPrefs.GetInt("cantidadResaltadas");
		//Debug.Log("llega el valor "+PlayerPrefs.GetInt("iniciarInmediatamente").ToString());

	}	
	
	void FixedUpdate () {
		if (!juegoIniciado)
		{
			if(Input.GetMouseButtonDown(0)||iniciarInmediatamente)
			{
				int multiX=UnityEngine.Random.Range(-1,0);
				if (multiX==0)
					multiX=1;
				int multiY=UnityEngine.Random.Range(-1,0);
				if(multiY==0)
					multiY=1;
				foreach(GameObject pelo in pelotasInstanciadas){
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(velocidadPelotasActual*multiX,velocidadPelotasActual*multiY);
					Debug.Log("movimiento pelota");		
				}
				
				
				juegoIniciado=true;
			}
		}
		if(juegoIniciado)
		{
			contadorDeSegundos += Time.deltaTime;
			if (contadorDeSegundos >= medidaDeTiempo){
				contadorDeSegundos=0;
				segundos++;
				
			}
			if(segundos==tiempoDeColor){
				GameObject pelotaActual=gameObject.GetComponent<ComportamientoPelota>().pelota;
				pelotaActual.GetComponent<Renderer>().material.color=colorOriginal;

			}
			if(tiempoDeInicio+tiempoDeColor==segundos-1){
				//txtTiempoDeInicio.enabled=false;
				if(continuarRebotes==false)
					rbBall.velocity=new Vector2(0,0);
			}
		}
		if(finalizarJuego && juegoIniciado)
		{
			rbBall.velocity=new Vector2(0,0);
			txtTiempoDeInicio.enabled=true;
			//GUI.Label (new Rect (0,0,100,50), "This is the text string for a Label Control");
			SpriteRenderer sprite=txtTiempoDeInicio.GetComponent<SpriteRenderer>();
			sprite.sortingOrder = 100;
            sprite.sortingLayerName = "Texto";
			juegoIniciado=false;
			
		}
		if(tiempoDeInicio+tiempoDeColor>segundos-1)
			txtTiempoDeInicio.text=(tiempoDeInicio+tiempoDeColor-segundos).ToString();
		else
		{
			string lcCero=(contadorDeSegundos*100)<10?"0":"";
			txtTiempoDeInicio.text=(segundos-(tiempoDeInicio+tiempoDeColor)).ToString()+":"+lcCero+((int)(contadorDeSegundos*100)).ToString();
		}
		//Debug.Log(GetComponent<Renderer>().material.color);
	}

	void OnMouseDown ()
    {
		GameObject pelotaPresionada=gameObject.GetComponent<ComportamientoPelota>().pelota;
		if (pelotaPresionada.tag=="Resaltada"&& juegoIniciado && segundos>tiempoDeColor+tiempoDeInicio)
		{
        	rbBall.velocity=new Vector2(0,0);
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.red;			
			cantidadEncontradas++;
			Debug.Log("Encontradas:"+cantidadEncontradas.ToString());
			if (cantidadEncontradas==cantidadResaltadas)
				finalizarJuego=true;
		}
    }

	void Logs (){
		Debug.Log("inicio pelota");
		/* Debug.Log("z de la pelota "+transform.position.z);
		//Debug.Log("z de el canvas "+canvasDimensiones.position.z);
		Debug.Log("x de la pelota "+transform.position.x);
		Debug.Log("y de la pelota "+transform.position.y);*/
		//Debug.Log(iniciarInmediatamente.ToString());
	}

	private Vector3 obtenerPosicionAleatoria()
    {
		float x=UnityEngine.Random.Range(-7,7);
		float y=UnityEngine.Random.Range(-4,4);
		Vector3 posicionAleatoria = new Vector3(x,y,transform.position.z);    
		return posicionAleatoria;
	}


}
