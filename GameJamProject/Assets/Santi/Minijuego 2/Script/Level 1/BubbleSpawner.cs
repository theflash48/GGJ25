using System.Collections;
using Unity.VisualScripting;
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

            int randomType = Random.Range(0, 4); // Genera solo burbujas válidas
            Vector2 spawnPosition = new Vector2(Random.Range(spawnPoint.position.x-10f, spawnPoint.position.x+10f), spawnPoint.position.y);
            Instantiate(bubblePrefabs[randomType], spawnPosition, Quaternion.identity);
        }
    }
    
}
