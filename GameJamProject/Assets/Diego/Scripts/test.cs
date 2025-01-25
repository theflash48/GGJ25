using UnityEngine;

public class test : MonoBehaviour
{
    public Transform circleCenter; // Centro del c�rculo
    public float radius = 10f; // Radio del c�rculo
    public Renderer planeRenderer; // Renderer del plano
    public Color circleColor = Color.red; // Color dentro del c�rculo
    public Color outsideColor = Color.green; // Color fuera del c�rculo

    private Texture2D texture; // Textura generada din�micamente
    private int textureResolution = 256; // Resoluci�n de la textura

    void Start()
    {
        // Crear una nueva textura din�mica
        texture = new Texture2D(textureResolution, textureResolution);
        texture.filterMode = FilterMode.Bilinear; // Suavizado en la textura
        planeRenderer.material.mainTexture = texture;
        UpdateTexture();
    }

    void Update()
    {
        UpdateTexture();
    }

    void UpdateTexture()
    {
        // Convertir la posici�n del c�rculo a coordenadas UV
        Vector2 centerUV = WorldToUV(circleCenter.position, planeRenderer);

        // Recorrer cada p�xel de la textura
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                // Calcular la posici�n UV del p�xel
                Vector2 pixelUV = new Vector2((float)x / texture.width, (float)y / texture.height);

                // Convertir el radio del mundo a proporci�n UV
                float uvRadius = radius / Mathf.Max(planeRenderer.bounds.size.x, planeRenderer.bounds.size.z);

                // Calcular la distancia entre el p�xel y el centro del c�rculo
                float distance = Vector2.Distance(centerUV, pixelUV);

                // Interpolaci�n suave del color basado en la distancia
                float t = Mathf.Clamp01((uvRadius - distance) / uvRadius); // Valor interpolado (0-1)
                Color pixelColor = Color.Lerp(outsideColor, circleColor, t);

                // Asignar el color al p�xel
                texture.SetPixel(x, y, pixelColor);
            }
        }

        // Aplicar los cambios en la textura
        texture.Apply();
    }

    Vector2 WorldToUV(Vector3 worldPosition, Renderer renderer)
    {
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPosition);

        Vector2 uv = new Vector2(
            (localPos.x / renderer.bounds.size.x) + 0.5f,
            (localPos.z / renderer.bounds.size.z) + 0.5f
        );


        uv.x = 1f - uv.x;
        uv.y = 1f - uv.y;

        return uv;
    }
}
