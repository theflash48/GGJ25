using UnityEngine;
//using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public float levelTime = 30f; // Tiempo límite del nivel
    private float timer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = levelTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            
        }
    }

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene();
    }

    public void LoseLevel()
    {
        //SceneManager.LoadScene();
    }
}   
public class BubbleSpawner_Level1 : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Asignar los prefabs de burbujas
    public Transform spawnPoint; // Punto de aparición
    public float spawnInterval = 5f; // Intervalo de aparición

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        int randomType = Random.Range(0, 1); // Solo un tipo de burbuja válida
        Instantiate(bubblePrefabs[randomType], spawnPoint.position, Quaternion.identity);
    }
}
public class BubbleSpawner_Level2 : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Asignar los prefabs de burbujas
    public Transform spawnPoint; // Punto de aparición
    public float spawnInterval = 4f; // Intervalo de aparición

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        int randomType = Random.Range(0, 2); // Dos tipos de burbujas válidas
        Instantiate(bubblePrefabs[randomType], spawnPoint.position, Quaternion.identity);
    }
}
public class BubbleSpawner_Level3 : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Asignar los prefabs de burbujas
    public Transform spawnPoint; // Punto de aparición
    public float spawnInterval = 3f; // Intervalo de aparición

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        int randomType = Random.Range(0, 3); // Tres tipos de burbujas válidas
        Instantiate(bubblePrefabs[randomType], spawnPoint.position, Quaternion.identity);
    }
}
public class BubbleSpawner_Level4 : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Asignar los prefabs de burbujas
    public Transform spawnPoint; // Punto de aparición
    public float spawnInterval = 2f; // Intervalo de aparición

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        int randomType = Random.Range(0, 4); // Cuatro tipos de burbujas válidas
        Instantiate(bubblePrefabs[randomType], spawnPoint.position, Quaternion.identity);
    }
}



