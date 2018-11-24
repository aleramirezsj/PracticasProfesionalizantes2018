using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CambiarEscena : MonoBehaviour {
	public Text txtTamanioPelota;
	public Text txtVelocidadPelotas;
	public Text txtCantidadPelotas;
	public Text txtCantidadResaltadas;
	public Text txtTiempoDeColor;
	public Text txtTiempoDeInicio;
	public TextMeshProUGUI infNombreJugador;
	public Toggle ChkIniciarInmediatamente;
	public Component lstJugadores;
	public Toggle chkContinuarRebotes;
	public GameObject pelota;
	private ParametrosJuego parametros=new ParametrosJuego();

	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);

		/* //prepara elementos para guardar en disco las configuraciones actuales del juego para poder recuperarlas en la próxima ejecución
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);

		ParametrosJuego parametros=new ParametrosJuego();	
		parametros.cantidadTotalPelotas=(int)sldCantidadPelotas.value;	
		parametros.cantidadResaltadas=(int)sldCantidadResaltadas.value;
		parametros.tamanioActualPelota=sldTamanioPelota.value;
		parametros.velocidadActualPelotas=(int)sldVelocidadPelotas.value;
		parametros.jugadorActual=infNombreJugador.text;
		//parametros.jugadores.Add(infNombreJugador.text);
		parametros.iniciarInmediatamente=iniciaInmediatamente.isOn;
		parametros.tiempoDeColor=(int)sldTiempoDeColor.value;
		parametros.tiempoDeInicio=(int)sldTiempoDeInicio.value;
		parametros.continuarRebotes=chkContinuarRebotes.isOn;
		bf.Serialize(archivo,parametros);
		archivo.Close();*/
	}

	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		 //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores
		//recuperados		
		//txtVelocidadPelotas.text=Application.persistentDataPath.ToString();
		if (File.Exists(Application.persistentDataPath+"/DatosJuego.dat")){
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);	
			parametros= (ParametrosJuego)bf.Deserialize(archivo);
			archivo.Close();
			txtTamanioPelota.text=parametros.tamanioActualPelota.ToString();
			txtCantidadPelotas.text=parametros.cantidadTotalPelotas.ToString();
			txtCantidadResaltadas.text=parametros.cantidadResaltadas.ToString();
			Debug.Log("Resaltadas:"+parametros.cantidadResaltadas);
			txtVelocidadPelotas.text=parametros.velocidadActualPelotas.ToString();	
			infNombreJugador.text=parametros.jugadorActual;	
			ChkIniciarInmediatamente.isOn=parametros.iniciarInmediatamente;
			txtTiempoDeColor.text=parametros.tiempoDeColor.ToString();
			txtTiempoDeInicio.text=parametros.tiempoDeInicio.ToString();
			chkContinuarRebotes.isOn=parametros.continuarRebotes;
			pelota.transform.localScale=new Vector3(parametros.tamanioActualPelota/2,parametros.tamanioActualPelota/2,parametros.tamanioActualPelota/2);
			//txtVelocidadPelotas.text="SI";
		}
	}


	void OnDisable()
	{
		PlayerPrefs.SetInt("iniciarInmediatamente",ChkIniciarInmediatamente.isOn?1:0);
		PlayerPrefs.SetInt("chkContinuarRebotes",chkContinuarRebotes.isOn?1:0);
		parametros.iniciarInmediatamente=ChkIniciarInmediatamente.isOn;
		parametros.continuarRebotes=chkContinuarRebotes.isOn;
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);	
		bf.Serialize(archivo,parametros);
		archivo.Close();			
		//Debug.Log(iniciaInmediatamente.isOn?1:0);

	}
	 void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		}
}
