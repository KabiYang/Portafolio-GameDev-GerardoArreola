using System;
using UnityEngine;

[Serializable]
public class Bonus
{
    public string nombre = "Bonus";
    public int puntosExtra = 0;
    public float tiempoExtra = 0f;
    public Color color = Color.yellow;
    public float probabilidad = 10f;

    public bool IntentarAplicar(ControladorJuego juego)
    {
        float probReal = probabilidad > 1f ? probabilidad / 100f : probabilidad;
        if (UnityEngine.Random.value > probReal) return false;

        if (puntosExtra > 0) juego.cantidadClicks += puntosExtra;
        if (tiempoExtra > 0) juego.tiempoRestante += tiempoExtra;

        return true;
    }

    public bool IntentarAplicar(MonoBehaviour c) { return IntentarAplicar(c as ControladorJuego); }
}