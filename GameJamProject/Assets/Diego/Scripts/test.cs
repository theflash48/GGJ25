using UnityEngine;

public class test : MonoBehaviour
{
    public Renderer planeRenderer;    // El Renderer del plano donde se aplica la textura
    public Transform circleCenter;    // La posición de la esfera en el mundo
    public GameObject sphere;         // La esfera 3D en el mundo
    public Texture2D texture;         // La textura que se actualiza dinámicamente
    public Color outsideColor = Color.white;   // Color fuera del círculo
    public Color circleColor = Color.red;      // Color dentro del círculo
    public float radius = 1f;                  // Radio del círculo en unidades del mundo
    public float smoothSpeed = 10f;            // Velocidad de interpolación

    private Vector3 smoothedPosition;          // Posición suavizada para sincronización

    void Start()
    {
        // Inicializar la posición suavizada con la posición inicial de la esfera
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
        // Interpolar suavemente la posición de la esfera
        smoothedPosition = Vector3.Lerp(smoothedPosition, circleCenter.position, Time.deltaTime * smoothSpeed);

        // Actualizar la posición de la esfera
        sphere.transform.position = circleCenter.position;
        sphere.transform.localScale = new Vector3(radius, radius, radius);

        // Actualizar la textura con la posición suavizada
        UpdateTextureWithSmoothedPosition(smoothedPosition);
    }

    void UpdateTextureWithSmoothedPosition(Vector3 smoothedPos)
    {
        // Convertir la posición del mundo a UV
        Vector2 centerUV = WorldToUV(smoothedPos, planeRenderer);

        // Calcular el radio UV relativo al tamaño y escala del plano
        Vector3 planeSize = planeRenderer.bounds.size;
        Vector3 planeScale = planeRenderer.transform.localScale;
        float uvRadius = radius / Mathf.Max(planeSize.x / planeScale.x, planeSize.z / planeScale.z);

        // Convertir el radio UV a píxeles
        int uvRadiusInPixels = Mathf.CeilToInt(uvRadius * texture.width);

        // Calcular las coordenadas centrales en píxeles
        int centerX = Mathf.RoundToInt(centerUV.x * texture.width);
        int centerY = Mathf.RoundToInt(centerUV.y * texture.height);

        // Obtener los píxeles de la textura
        Color[] pixels = texture.GetPixels();

        // Recorrer solo los píxeles afectados por el círculo
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
        // Convertir la posición del mundo a espacio local
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPosition);

        // Calcular las coordenadas UV relativas al tamaño del plano y su escala
        Vector3 planeScale = renderer.transform.localScale;
        Vector2 uv = new Vector2(
            (localPos.x / (renderer.bounds.size.x / planeScale.x)) + 0.5f,
            (localPos.z / (renderer.bounds.size.z / planeScale.z)) + 0.5f
        );

        return uv;
    }

    void OnDrawGizmos()
    {
        // Dibujar la posición y el radio del círculo para depuración
        if (circleCenter != null && planeRenderer != null)
        {
            Gizmos.color = Color.red;

            // Centro del círculo
            Vector3 circlePos = circleCenter.position;
            Gizmos.DrawSphere(circlePos, 0.1f);

            // Proyección del círculo en el plano
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
