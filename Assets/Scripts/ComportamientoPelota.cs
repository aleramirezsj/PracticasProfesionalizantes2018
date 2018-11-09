using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class ComportamientoPelota : MonoBehaviour {
	public bool juegoIniciado= false;
	public Rigidbody2D rbBall;
	public Canvas canvas;
	private int cantidadPelotasActual;
	private float escalaActualPelota;
	private int velocidadPelotasActual;
	private int tiempoDeColor;
	private int tiempoDeInicio;
	public static int instancias;
	public Text txtNombreJugador;
	public Text txtTiempoDeInicio;
	private Color colorOriginal;
	int segundos=0;
	float contadorDeSegundos=0;
	float medidaDeTiempo=1;
	bool iniciarInmediatamente;
	static public bool finalizarJuego;
	bool continuarRebotes;
	

    void Start () {
		Logs();
		//RectTransform canvasDimensiones = canvas.GetComponent<RectTransform> ();
		float x=Random.Range(-7,7);
		float y=Random.Range(-4,4);
		Vector3 posicionAleatoria = new Vector3(x,y,transform.position.z);
		//transform.position=Camera.main.ScreenToWorldPoint(posicionAleatoria);
		transform.position=posicionAleatoria;
		transform.localScale=new Vector3(escalaActualPelota,escalaActualPelota,escalaActualPelota);
		if(instancias<cantidadPelotasActual-1){
			for(int i=0;i<cantidadPelotasActual-1;i++){
				x=Random.Range(-7,7);
				y=Random.Range(-4,4);
				posicionAleatoria = new Vector3(x,y,transform.position.z);
				GameObject.Instantiate(this, posicionAleatoria, transform.rotation);
				instancias++;
				if(instancias==cantidadPelotasActual-1){
					gameObject.tag="Resaltada";
					colorOriginal=GetComponent<Renderer>().material.color;
					GetComponent<Renderer>().material.color = Color.red;
				}
			}
		}
		
	}
	void OnEnable()
	{
		cantidadPelotasActual=PlayerPrefs.GetInt("cantidadPelotasActual");
		escalaActualPelota=PlayerPrefs.GetFloat("escalaActualPelota");
		velocidadPelotasActual=PlayerPrefs.GetInt("velocidadPelotasActual")*2;
		txtNombreJugador.text=PlayerPrefs.GetString("nombreJugador");
		iniciarInmediatamente=(PlayerPrefs.GetInt("iniciarInmediatamente"))==1;
		tiempoDeColor=PlayerPrefs.GetInt("tiempoDeColor");
		tiempoDeInicio=PlayerPrefs.GetInt("tiempoDeInicio");
		continuarRebotes=(PlayerPrefs.GetInt("chkContinuarRebotes"))==1;
		//Debug.Log("llega el valor "+PlayerPrefs.GetInt("iniciarInmediatamente").ToString());

	}	
	
	void FixedUpdate () {
		if (!juegoIniciado)
		{
			if(Input.GetMouseButtonDown(0)||iniciarInmediatamente)
			{
				int multiX=Random.Range(-1,0);
				if (multiX==0)
					multiX=1;
				int multiY=Random.Range(-1,0);
				if(multiY==0)
					multiY=1;
				rbBall.velocity=new Vector2(velocidadPelotasActual*multiX,velocidadPelotasActual*multiY);				
				juegoIniciado=true;
			}
		}
		if(juegoIniciado)
		{
			contadorDeSegundos += Time.deltaTime;
			if (contadorDeSegundos >= medidaDeTiempo){
				contadorDeSegundos=0;
				segundos++;
				txtTiempoDeInicio.text=(tiempoDeInicio+tiempoDeColor-segundos).ToString();
			}
			if(segundos==tiempoDeColor && gameObject.tag=="Resaltada"){
				//GetComponent<Renderer>().material.color = Color.green;
				GetComponent<Renderer>().material.color = colorOriginal;
			}
			if(tiempoDeInicio+tiempoDeColor==segundos-1){
				txtTiempoDeInicio.enabled=false;
				if(continuarRebotes==false)
					rbBall.velocity=new Vector2(0,0);
			}
		}
		if(finalizarJuego)
		{
			rbBall.velocity=new Vector2(0,0);
		}


		
		

	}

	void OnMouseDown ()
    {
		if (gameObject.tag=="Resaltada"&& juegoIniciado && segundos>tiempoDeColor+tiempoDeInicio)
		{
        	rbBall.velocity=new Vector2(0,0);
			finalizarJuego=true;
			GetComponent<Renderer>().material.color = Color.red;

		}
    }

	void Logs (){
		/* Debug.Log("z de la pelota "+transform.position.z);
		//Debug.Log("z de el canvas "+canvasDimensiones.position.z);
		Debug.Log("x de la pelota "+transform.position.x);
		Debug.Log("y de la pelota "+transform.position.y);*/
		//Debug.Log(iniciarInmediatamente.ToString());
	}
}
