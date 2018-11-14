using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinDelJuego : MonoBehaviour {

	public Text puntajeJugadorText;
	public Text puntajeEnemigoText; 

	int puntajeJugador;
	int puntajeEnemigo;

	public CambiarEscena cambiarEscena;

	private void OnTriggerEnter2D(Collider2D pelota)
	{
		//Debug.Log(gameObject.tag);
		if (gameObject.tag=="Jugador")
		{
			puntajeJugador++;
			ActualizarTextoPuntajes(puntajeJugadorText,puntajeJugador);
		}else if (gameObject.tag=="Enemigo")
		{
			puntajeEnemigo++;
			ActualizarTextoPuntajes(puntajeEnemigoText,puntajeEnemigo);
		}
		ComportamientoPelota.juegoIniciado=false;
		ChequearPuntaje();

	}

	private void ActualizarTextoPuntajes(Text objetoTexto, int puntaje)
	{
		objetoTexto.text=puntaje.ToString();
	}

	private void ChequearPuntaje()
	{
		if (this.puntajeEnemigo>=3)
			cambiarEscena.CambiarEscenaA("JuegoPerdido");
		if (this.puntajeJugador>=3)
			cambiarEscena.CambiarEscenaA("JuegoGanado");
	}

}
