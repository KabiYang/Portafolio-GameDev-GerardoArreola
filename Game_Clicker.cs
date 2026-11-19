using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego Instancia;
    public int cantidadClicks;
    public float tiempoRestante = 20f;
    public TextMeshProUGUI textoClicks, textoTiempo, textoPlusBase, textoPlusBonus, textoPlusTiempo;

    public Bonus bonusPuntos = new Bonus() { nombre = "Mega", puntosExtra = 10, color = new Color(0.2f, 0.4f, 1f), probabilidad = 10f };
    public Bonus bonusTiempo = new Bonus() { nombre = "Tiempo", tiempoExtra = 2f, color = new Color(0.4f, 0.8f, 1f), probabilidad = 5f };

    void Awake()
    {
        if (Instancia == null) { Instancia = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    void OnEnable() { SceneManager.sceneLoaded += AlCargarEscena; }
    void OnDisable() { SceneManager.sceneLoaded -= AlCargarEscena; }

    void AlCargarEscena(Scene s, LoadSceneMode m)
    {
        if (s.name == "02_Game")
        {
            textoClicks = GameObject.Find("Texto_Clicks")?.GetComponent<TextMeshProUGUI>();
            textoTiempo = GameObject.Find("Texto_Tiempo")?.GetComponent<TextMeshProUGUI>();
            textoPlusBase = GameObject.Find("Texto_PlusBase")?.GetComponent<TextMeshProUGUI>();
            textoPlusBonus = GameObject.Find("Texto_PlusBonus")?.GetComponent<TextMeshProUGUI>();
            textoPlusTiempo = GameObject.Find("Texto_PlusTiempo")?.GetComponent<TextMeshProUGUI>();
            cantidadClicks = 0;
            tiempoRestante = 20f;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "02_Game") return;
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (textoTiempo != null) textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante) + "s";
        }
        else SceneManager.LoadScene("03_Gameover");
    }

    public void RegistrarClic()
    {
        int pb = (cantidadClicks >= 15) ? 2 : 1;
        cantidadClicks += pb;
        Color cb = (pb == 1) ? Color.white : new Color(0.3f, 1f, 0.3f);
        Mostrar(textoPlusBase, "+" + pb, cb, 0.5f, 1);

        if (bonusPuntos.IntentarAplicar(this))
            Mostrar(textoPlusBonus, "+10!", bonusPuntos.color, 1f, 2);
        else if (bonusTiempo.IntentarAplicar(this))
            Mostrar(textoPlusTiempo, "+2s", bonusTiempo.color, 1f, 3);

        if (textoClicks != null) textoClicks.text = "Clicks: " + cantidadClicks;
    }

    void Mostrar(TextMeshProUGUI t, string m, Color c, float d, int tp)
    {
        if (t == null) return;
        t.text = m;
        t.color = c;
        t.fontSize = 36;
        if (tp == 1) { CancelInvoke(nameof(B1)); Invoke(nameof(B1), d); }
        if (tp == 2) { CancelInvoke(nameof(B2)); Invoke(nameof(B2), d); }
        if (tp == 3) { CancelInvoke(nameof(B3)); Invoke(nameof(B3), d); }
    }

    void B1() { if (textoPlusBase != null) textoPlusBase.text = ""; }
    void B2() { if (textoPlusBonus != null) textoPlusBonus.text = ""; }
    void B3() { if (textoPlusTiempo != null) textoPlusTiempo.text = ""; }
}