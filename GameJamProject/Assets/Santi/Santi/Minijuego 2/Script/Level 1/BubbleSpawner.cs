using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Prefabs de burbujas
    public Transform spawnPoint; // Punto donde aparecen las burbujas
    private Coroutine spawnCoroutine; // Para iniciar/detener generación

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
