using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Unity.VisualScripting;

public class Sphere_1 : MonoBehaviour
{
    public Transform circleCenter;
    public GameObject sphere;
    public Renderer EsferaTrigger;

    private float radius = 10f; 
    public Renderer planeRenderer; 
    public Color circleColor = Color.red; 
    public Color outsideColor = Color.green;

    public GameObject ActivarCanvas;
   

    public Text TextoPreguntas;
    public Button Respuesta1, Respuesta2, Respuesta3;
    public Text TextoBoton1, TextoBoton2, TextoBoton3;
    private int ValorAleatorio;
    private int ContadorPreguntas;
    private bool Estoy1, Estoy2, Estoy3,Terminado;


    public PostProcessVolume postProcessVolume;
    private Vignette vignette;


    private Texture2D texture; // Textura generada dinámicamente
    private int textureResolution = 256; // Resolución de la textura



    void Start()
    {
        //Esfera Creacion
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rb = sphere.AddComponent<Rigidbody>();

        rb.isKinematic = true;

        Collider collider = sphere.GetComponent<Collider>();

        collider.isTrigger = true;

        sphere.transform.SetParent(this.transform);

        TriggerSphera customScript = sphere.AddComponent<TriggerSphera>();






        texture = new Texture2D(textureResolution, textureResolution);
        texture.filterMode = FilterMode.Bilinear; // Suavizado en la textura
        planeRenderer.material.mainTexture = texture;
        UpdateTexture();
        ActivarCanvas.SetActive(false);

        /*int[] valores = { 1, 2, 3 };

        int indiceAleatorio = Random.Range(0, valores.Length);

        ValorAleatorio = valores[indiceAleatorio];*/
        ValorAleatorio = 1;
       

        ContadorPreguntas = 0;

        

        if (postProcessVolume == null)
        {
            postProcessVolume = GetComponent<PostProcessVolume>();

        }
        postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        vignette.intensity.value = 0.0f;
        Terminado = false;


    }

    void Update()
    {
        UpdateTexture();
        //Debug.Log(ContadorPreguntas);
    }
    void OnButtonPressed(string respuesta)
    {
        Debug.Log("Se presionó el botón: " + respuesta);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player") {

            ActivarCanvas.SetActive(true);
            if (ValorAleatorio == 1) { Preguta1(); }
            if (ValorAleatorio == 2) { Preguta2(); }
            if (ValorAleatorio == 3) { Preguta3(); }

            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;

        }
    }
    public void Empezar() {
        if (!Terminado) {
            ActivarCanvas.SetActive(true);
            if (ValorAleatorio == 1) { Preguta1(); }
            if (ValorAleatorio == 2) { Preguta2(); }
            if (ValorAleatorio == 3) { Preguta3(); }

            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;
        }
        
        
    }
    public void Acabar() 
    {
        if (!Terminado)
        {
            ActivarCanvas.SetActive(false);

            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.0f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivarCanvas.SetActive(false);

            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.0f;

        }

    }

    void Preguta1() {
        Estoy1 = true;
        if (ContadorPreguntas == 0)
        {
            TextoPreguntas.text = "Preguta1";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());
    

        }
        if (ContadorPreguntas == 1)
        {
            TextoPreguntas.text = "Preguta11";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";
           
            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());


        }
        if (ContadorPreguntas == 2)
        {
            TextoPreguntas.text = "Preguta111";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrectaFinal());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrectaFinal());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrectaFinal());


        }



    }
    void Preguta2() {
        Estoy2 = true;
        if (ContadorPreguntas == 0)
        {
            TextoPreguntas.text = "Preguta2";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());
        }
        if (ContadorPreguntas == 1)
        {
            
            TextoPreguntas.text = "Preguta22";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());


        }
        if (ContadorPreguntas == 2)
        {
            TextoPreguntas.text = "Preguta222";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrectaFinal());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrectaFinal());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrectaFinal());


        }
    }
    void Preguta3() {
        Estoy3 = true;
        if (ContadorPreguntas == 0)
        {
            TextoPreguntas.text = "Preguta3";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());
        }
        if (ContadorPreguntas == 1)
        {
            
            TextoPreguntas.text = "Preguta33";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrecta());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrecta());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrecta());


        }
        if (ContadorPreguntas == 2)
        {

            TextoPreguntas.text = "Preguta333";
            TextoBoton1.text = "1";
            TextoBoton2.text = "2";
            TextoBoton3.text = "3";

            Respuesta1.onClick.AddListener(() => RespuestaCorrectaFinal());
            Respuesta2.onClick.AddListener(() => RespuestaIncorrectaFinal());
            Respuesta3.onClick.AddListener(() => RespuestaIncorrectaFinal());


        }
    }


    public void RespuestaFinal()
    {
        radius = 0.0f;
        ActivarCanvas.SetActive(false);
    }
    public void RespuestaCorrecta()
    {
        ContadorPreguntas++;
        
        if (Estoy1) { Preguta1();  }
        if (Estoy2) { Preguta2();  }
        if (Estoy3) { Preguta3();  }


    }

    public void RespuestaIncorrecta()
    {
        ContadorPreguntas++;
        radius = radius + 0.3f;
        UpdateTexture();
        if (Estoy1) { Preguta1();  }
        if (Estoy2) { Preguta2();  }
        if (Estoy3) { Preguta3(); }
    }
    public void RespuestaCorrectaFinal() 
    {
        Terminado = true;

        radius = 0.0f; ActivarCanvas.SetActive(false);
        Destroy(this.gameObject);
        sphere.transform.localScale = new Vector3(radius, radius, radius);
        UpdateTexture();
        postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        vignette.intensity.value = 0.0f;

    }
    public void RespuestaIncorrectaFinal() 
    {
        Terminado = true;
        radius = 6.0f;
        UpdateTexture();
        ActivarCanvas.SetActive(false);
        sphere.transform.localScale = new Vector3(radius , radius  , radius );
        postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        vignette.intensity.value = 0.0f;
        //Destroy(this.gameObject);
    }

    void UpdateTexture()
    {

        Vector2 centerUV = WorldToUV(circleCenter.position, planeRenderer);
        sphere.transform.position = circleCenter.transform.position;
        sphere.transform.localScale = new Vector3(radius, radius, radius );


       
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Vector2 pixelUV = new Vector2((float)x / texture.width, (float)y / texture.height);

                float uvRadius = radius / Mathf.Max(planeRenderer.bounds.size.x, planeRenderer.bounds.size.z);

                float distance = Vector2.Distance(centerUV, pixelUV);

                float t = Mathf.Clamp01((uvRadius - distance) / uvRadius); 
                Color pixelColor = Color.Lerp(outsideColor, circleColor, t);

                texture.SetPixel(x, y, pixelColor);
            }
        }
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
