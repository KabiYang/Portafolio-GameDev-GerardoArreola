using UnityEngine;
using TMPro;

public class TextoFlotante : MonoBehaviour
{
    private TMP_Text miTexto;
    public float velocidadSubida = 100f;
    public float tiempoDeVida = 1f;

    void Awake()
    {
        miTexto = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocidadSubida * Time.deltaTime);
    }

    public void ConfigurarTexto(string texto, Color colorDelTexto)
    {
        if (miTexto != null)
        {
            miTexto.text = texto;
            miTexto.color = colorDelTexto;
        }
    }
}