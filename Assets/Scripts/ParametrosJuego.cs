﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParametrosJuego
{
    public List<string> jugadores = new List<string>();
    public int velocidadActualPelotas=0;
    public int cantidadActualPelotas=0;
    public float tamanioActualPelota=0;
    public String jugadorActual;
    public bool iniciarInmediatamente;
    public int tiempoDeColor=1;
    public int tiempoDeInicio=1;

    public bool continuarRebotes;
}
