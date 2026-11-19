using UnityEngine;
using TMPro;

public class Drop : MonoBehaviour
{
    public enum TipoPelota { Normal, Enemigo, Vida, Dorada }
    public TipoPelota tipoActual;

    private SpriteRenderer spriteRenderer;
    private Collider2D objetoCollider;

    private TMP_Text textoEfectoPuntos;
    private TMP_Text textoEfectoVidas;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objetoCollider = GetComponent<Collider2D>();
        ConfigurarPelotaAleatoria();

        GameObject objPuntos = GameObject.Find("Texto_EfectoPuntos");
        if (objPuntos != null) textoEfectoPuntos = objPuntos.GetComponent<TMP_Text>();

        GameObject objVidas = GameObject.Find("Texto_EfectoVidas");
        if (objVidas != null) textoEfectoVidas = objVidas.GetComponent<TMP_Text>();
    }

    void ConfigurarPelotaAleatoria()
    {
        float azar = Random.Range(0f, 100f);

        if (azar < 60f)
        {
            tipoActual = TipoPelota.Normal;
        }
        else if (azar < 80f)
        {
            tipoActual = TipoPelota.Enemigo;
            spriteRenderer.color = Color.red;
        }
        else if (azar < 93f)
        {
            tipoActual = TipoPelota.Vida;
            spriteRenderer.color = Color.green;
        }
        else
        {
            tipoActual = TipoPelota.Dorada;
            spriteRenderer.color = new Color(0f, 0.1f, 0.4f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (tipoActual)
            {
                case TipoPelota.Normal:
                    GameManager.score += 1;
                    MostrarMensaje(textoEfectoPuntos, "+1 Pts", Color.yellow);
                    break;
                case TipoPelota.Enemigo:
                    GameManager.lives -= 1;
                    MostrarMensaje(textoEfectoVidas, "-1 Vida", Color.red);
                    break;
                case TipoPelota.Vida:
                    GameManager.lives += 1;
                    MostrarMensaje(textoEfectoVidas, "+1 Vida", Color.green);
                    break;
                case TipoPelota.Dorada:
                    GameManager.score += 10;
                    MostrarMensaje(textoEfectoPuntos, "+10 Pts", new Color(0f, 0.1f, 0.4f));
                    break;
            }
            OcultarYDestruir();
        }
        else if (col.CompareTag("LoseLives"))
        {
            if (tipoActual == TipoPelota.Normal || tipoActual == TipoPelota.Dorada)
            {
                GameManager.lives -= 1;
                MostrarMensaje(textoEfectoVidas, "-1 Vida", Color.red);
            }

            OcultarYDestruir();
        }
    }

    void MostrarMensaje(TMP_Text componenteTexto, string mensaje, Color colorTexto)
    {
        if (componenteTexto == null) return;

        componenteTexto.text = mensaje;
        componenteTexto.color = colorTexto;

        CancelInvoke(nameof(BorrarTexto));
        Invoke(nameof(BorrarTexto), 0.5f);
    }

    void OcultarYDestruir()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (objetoCollider != null) objetoCollider.enabled = false;

        Destroy(gameObject, 0.55f);
    }

    void BorrarTexto()
    {
        if (textoEfectoPuntos != null) textoEfectoPuntos.text = "";
        if (textoEfectoVidas != null) textoEfectoVidas.text = "";
    }
}