using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Prefabs de burbujas
    public Transform spawnPoint; // Punto donde aparecen las burbujas
    private Coroutine spawnCoroutine; // Para iniciar/detener generación
    public GameObject generationArea; // Objeto que delimita la zona de generación
    private Bounds areaBounds; // Límites de la zona de generación

    private void Start()
    {
        if (generationArea != null)
        {
            areaBounds = generationArea.GetComponent<Renderer>().bounds;
            GenerateObjects(); // Primera generación
        }
        else
        {
            Debug.LogError("No se ha asignado una zona de generación.");
        }
    }
    void GenerateObjects()
    {
        if (generationArea == null)
        {
            Debug.LogError("No se ha asignado una zona de generación.");
            return;
        }

        // Obtener los límites del área de generación
        float minX = areaBounds.min.x;
        float maxX = areaBounds.max.x;
        float minZ = areaBounds.min.z;
        float maxZ = areaBounds.max.z;

        // Generar un objeto de cada tipo
        GenerateObject(bubblePrefabs, GetRandomPosition(minX, maxX, minZ, maxZ));
    }
    void GenerateObject(GameObject[] prefabs, Vector3 position)
    {
        // Elegir un objeto aleatorio
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject prefab = prefabs[randomIndex];

        // Instanciar el objeto en la posición dada
        Instantiate(prefab, position, Quaternion.identity);
    }
    Vector3 GetRandomPosition(float minX, float maxX, float minZ, float maxZ)
    {
        // Generar una posición aleatoria
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, 0, randomZ);
    }    
    public void StartSpawning(float interval, int validBubbleCount)
    {
        spawnCoroutine = StartCoroutine(SpawnBubbles(interval, validBubbleCount));
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnBubbles(float interval, int validBubbleCount)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            int randomType = Random.Range(0, validBubbleCount); // Genera solo burbujas válidas
            Instantiate(bubblePrefabs[randomType], spawnPoint.position, Quaternion.identity);
        }
    }
}
