using UnityEngine;

public class test : MonoBehaviour
{
    public Renderer planeRenderer;    // El Renderer del plano donde se aplica la textura
    public Transform circleCenter;    // La posici�n de la esfera en el mundo
    public GameObject sphere;         // La esfera 3D en el mundo
    public Texture2D texture;         // La textura que se actualiza din�micamente
    public Color outsideColor = Color.white;   // Color fuera del c�rculo
    public Color circleColor = Color.red;      // Color dentro del c�rculo
    public float radius = 1f;                  // Radio del c�rculo en unidades del mundo
    public float smoothSpeed = 10f;            // Velocidad de interpolaci�n

    private Vector3 smoothedPosition;          // Posici�n suavizada para sincronizaci�n

    void Start()
    {
        // Inicializar la posici�n suavizada con la posici�n inicial de la esfera
        smoothedPosition = circleCenter.position;

        // Configurar la textura si es necesario
        if (texture == null)
        {
            texture = new Texture2D(256, 256);
            planeRenderer.material.mainTexture = texture;
        }
    }

    void Update()
    {
        // Interpolar suavemente la posici�n de la esfera
        smoothedPosition = Vector3.Lerp(smoothedPosition, circleCenter.position, Time.deltaTime * smoothSpeed);

        // Actualizar la posici�n de la esfera
        sphere.transform.position = circleCenter.position;
        sphere.transform.localScale = new Vector3(radius, radius, radius);

        // Actualizar la textura con la posici�n suavizada
        UpdateTextureWithSmoothedPosition(smoothedPosition);
    }

    void UpdateTextureWithSmoothedPosition(Vector3 smoothedPos)
    {
        // Convertir la posici�n del mundo a UV
        Vector2 centerUV = WorldToUV(smoothedPos, planeRenderer);

        // Calcular el radio UV relativo al tama�o y escala del plano
        Vector3 planeSize = planeRenderer.bounds.size;
        Vector3 planeScale = planeRenderer.transform.localScale;
        float uvRadius = radius / Mathf.Max(planeSize.x / planeScale.x, planeSize.z / planeScale.z);

        // Convertir el radio UV a p�xeles
        int uvRadiusInPixels = Mathf.CeilToInt(uvRadius * texture.width);

        // Calcular las coordenadas centrales en p�xeles
        int centerX = Mathf.RoundToInt(centerUV.x * texture.width);
        int centerY = Mathf.RoundToInt(centerUV.y * texture.height);

        // Obtener los p�xeles de la textura
        Color[] pixels = texture.GetPixels();

        // Recorrer solo los p�xeles afectados por el c�rculo
        for (int x = Mathf.Max(0, centerX - uvRadiusInPixels); x < Mathf.Min(texture.width, centerX + uvRadiusInPixels); x++)
        {
            for (int y = Mathf.Max(0, centerY - uvRadiusInPixels); y < Mathf.Min(texture.height, centerY + uvRadiusInPixels); y++)
            {
                Vector2 pixelUV = new Vector2((float)x / texture.width, (float)y / texture.height);
                float distance = Vector2.Distance(centerUV, pixelUV);

                if (distance <= uvRadius)
                {
                    float t = Mathf.Clamp01((uvRadius - distance) / uvRadius);
                    pixels[y * texture.width + x] = Color.Lerp(outsideColor, circleColor, t);
                }
            }
        }

        // Aplicar los cambios a la textura
        texture.SetPixels(pixels);
        texture.Apply();
    }

    Vector2 WorldToUV(Vector3 worldPosition, Renderer renderer)
    {
        // Convertir la posici�n del mundo a espacio local
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPosition);

        // Calcular las coordenadas UV relativas al tama�o del plano y su escala
        Vector3 planeScale = renderer.transform.localScale;
        Vector2 uv = new Vector2(
            (localPos.x / (renderer.bounds.size.x / planeScale.x)) + 0.5f,
            (localPos.z / (renderer.bounds.size.z / planeScale.z)) + 0.5f
        );

        return uv;
    }

    void OnDrawGizmos()
    {
        // Dibujar la posici�n y el radio del c�rculo para depuraci�n
        if (circleCenter != null && planeRenderer != null)
        {
            Gizmos.color = Color.red;

            // Centro del c�rculo
            Vector3 circlePos = circleCenter.position;
            Gizmos.DrawSphere(circlePos, 0.1f);

            // Proyecci�n del c�rculo en el plano
            Vector2 centerUV = WorldToUV(circlePos, planeRenderer);
            Vector3 projectedPos = planeRenderer.transform.TransformPoint(new Vector3(
                (centerUV.x - 0.5f) * planeRenderer.bounds.size.x,
                0,
                (centerUV.y - 0.5f) * planeRenderer.bounds.size.z
            ));

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(projectedPos, radius);
        }
    }
}
