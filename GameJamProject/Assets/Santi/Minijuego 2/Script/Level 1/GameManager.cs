using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1; // Nivel actual (Nivel 1)
    public int validBubbleCount = 1; // Solo 1 tipo de burbuja válida
    public float spawnInterval = 5f; // Intervalo de generación de burbujas
    public float levelTime = 30f; // Duración del nivel
    private float timer;

    public Text timerText; // Texto del temporizador
    public Text scoreText; // Texto de puntuación
    private int score = 0;

    public BubbleSpawner bubbleSpawner; // Referencia al generador de burbujas

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = levelTime; // Inicializar el temporizador
        bubbleSpawner.StartSpawning(spawnInterval, validBubbleCount); // Iniciar generación de burbujas
    }

    void Update()
    {
        // Actualizar el temporizador
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            EndLevel(true); // El jugador gana si el tiempo llega a 0
        }
    }

    public void CheckBubble(Bubble bubble)
    {
        if (bubble.bubbleType < validBubbleCount) // Burbuja válida
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
        else // Burbuja incorrecta
        {
            EndLevel(false); // Pierdes si seleccionas una burbuja incorrecta
        }
    }

    public void EndLevel(bool success)
    {
        bubbleSpawner.StopSpawning(); // Detener generación de burbujas

        if (success)
        {
            Debug.Log("¡Nivel completado!");
            // Aquí podrías agregar una transición al siguiente nivel
        }
        else
        {
            Debug.Log("¡Has perdido!");
            // Aquí podrías reiniciar el nivel
        }
    }
}
