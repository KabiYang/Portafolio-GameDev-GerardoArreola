using UnityEngine;
using TMPro;

public class MostrarFinal : MonoBehaviour
{
    void Start()
    {
        if (ControladorJuego.Instancia != null)
        {
            GetComponent<TextMeshProUGUI>().text = "¡Lograste hacer " + ControladorJuego.Instancia.cantidadClicks + " clicks!";
        }
    }
}