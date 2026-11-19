using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static int lives;
    public TMP_Text scoreUI;
    public TMP_Text livesUI;
    public GameObject prefab;

    bool isGameOver = false;

    [Header("Configuración de Caída")]
    public float tiempoEntreSpawn = 2.0f; 
    public float tiempoMinimoSpawn = 0.5f; 
    public float velocidadDeAceleracion = 0.02f; 
    private float cronometroSpawn = 0f;

    void Start()
    {
        score = 0;
        lives = 5;
        isGameOver = false;
        Time.timeScale = 1f;
        cronometroSpawn = 0f;
    }

    void Update()
    {
        scoreUI.text = "Score: " + score.ToString();
        livesUI.text = "Lives: " + lives.ToString();

        if (lives <= 0 && !isGameOver)
        {
            isGameOver = true;
            PlayerPrefs.SetInt("PuntajeFinal", score);
            SceneManager.LoadScene("03_Gameover");
            return;
        }

        if (!isGameOver)
        {
            cronometroSpawn += Time.deltaTime;

            if (cronometroSpawn >= tiempoEntreSpawn)
            {
                SpawnDrop();
                cronometroSpawn = 0f; 
            }

            if (tiempoEntreSpawn > tiempoMinimoSpawn)
            {
                tiempoEntreSpawn -= velocidadDeAceleracion * Time.deltaTime;
            }
        }
    }

    void SpawnDrop()
    {
        Vector3 spawnPosition = new Vector3(Random.Range( -15f, 15f), 10f, -1f);
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}