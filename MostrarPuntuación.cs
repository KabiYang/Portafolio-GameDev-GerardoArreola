using UnityEngine;
using TMPro;

public class MostrarPuntuacion : MonoBehaviour
{
    public TMP_Text textoPuntajeFinal;

    void Start()
    {
        int puntosGuardados = PlayerPrefs.GetInt("PuntajeFinal", 0);

        if (textoPuntajeFinal != null)
        {
            textoPuntajeFinal.text = "Obtuviste " + puntosGuardados + " puntos felicidades";
        }
    }
}