using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Unity.VisualScripting;
using System.Collections;

public class RayCast1 : MonoBehaviour
{
    public GameObject[] objects; // Los objetos que lanzarán los raycasts
    public float[] objectBrushRadii; // Radios de pincel para cada objeto
    public Color defaultColor = Color.white; // Color base del plano
    public Color paintColor = Color.red; // Color de pintura
    public int textureSize = 256; // Tamaño de la textura
    public GameObject Trigger1,Trigger2,Trigger3, Trigger4, Trigger5;
    private bool Terminado1, Terminado2, Terminado3, Terminado4,Terminado5, Persona1,Persona2;
    private int ContadorPreguntas, ContadorPreguntas1;
    public int Contador1, Contador2, Contador3, Contador4, Contador5,ContadorCinematica;
    private CameraFolllow FollowCamera;
    public GameObject Barrera, Barrera1, Barrera2, Barrera3, Barrera4;
    private float speed = 0.1f;
    public bool FinalBueno, FinalMalo;

    public Animator animatorIra;
    public Animator animatorSoledad;
    public Animator animatorAnsiedad;
    public Animator animatorToc;
    public Animator animatorAprensivo;

    public Animator PlayerA;

    public int contadorParaFinal = 0;

    public Texture2D baseTexture;



    private bool EstoyPregunta1, EstoyPregunta2, EstoyPregunta3, EstoyPregunta4, EstoyPregunta5;

    private Texture2D texture;
    private Renderer planeRenderer;

    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    public GameObject ActivarCanvas;
    public Text TextoPreguntas;
    public Button Respuesta1, Respuesta2, Respuesta3;
    public Text TextoBoton1, TextoBoton2, TextoBoton3;

    public float scaleSpeed = 1f;

    private PlayerController PlayerC;

    void Start()
    {
        FinalBueno=true;   FinalMalo = false;


        // Crear una nueva textura para el plano
        texture = new Texture2D(textureSize, textureSize);
        planeRenderer = GetComponent<Renderer>();
        FollowCamera = FindAnyObjectByType<CameraFolllow>();
        PlayerC = FindAnyObjectByType<PlayerController>();

        // Inicializar la textura con el color predeterminado
        ClearTexture();
        planeRenderer.material.mainTexture = texture;


        ActivarCanvas.SetActive(false);
        if (postProcessVolume == null)
        {
            postProcessVolume = GetComponent<PostProcessVolume>();

        }
        postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        vignette.intensity.value = 0.0f;


        //postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        //vignette.intensity.value = 0.0f;

        ContadorPreguntas = 0;
        ContadorPreguntas1 = 0;

        if (Trigger1 != null)
        {
            var triggerNotifier = Trigger1.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger;
            }
        }
        if (Trigger2 != null)
        {
            var triggerNotifier = Trigger2.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger1;
            }
        }
        if (Trigger3 != null)
        {
            var triggerNotifier = Trigger3.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger2;
            }
        }
        if (Trigger4 != null)
        {
            var triggerNotifier = Trigger4.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger3;
            }
        }
        if (Trigger5 != null)
        {
            var triggerNotifier = Trigger5.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger4;
            }
        }

    }
    void DoSomethingOnCustomTrigger()
    {
        //objectBrushRadii[0] = 100.0f;
        //Debug.Log("Acción personalizada ejecutada en el trigger externo.");
        // Aquí puedes agregar la lógica adicional que desees
        if (!Terminado1)
        {
            ActivarCanvas.SetActive(true);
            PlayerC.DentroCirculo = true;
            EstoyPregunta1 = true;
            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;

            //PlayerA.Play("ANI_Robot_01_Idle");

            FollowCamera.PlayerIn = false;
        }
       



     
    }
    /*public IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            this.GetComponent<Text>().text = TextoPreguntas;
            yield return new WaitForSeconds(speed);
        }
    }*/
    public void CargaTextosDialogo1()
    {
        if (EstoyPregunta1) {
            if (Contador1 == 0)
            {
                //StartCoroutine(ShowText());
                TextoPreguntas.text = "¡Todo me saca de quicio! Siempre hay algo que arruina mi día.";
                TextoBoton1.text = "Entiendo que estés molesto. Esa energía puede usarse de otra manera.";
                TextoBoton2.text = "Tal vez no deberías darle tanta importancia a esas cosas.";
                TextoBoton3.text = "Si te enfadas por todo, nunca vas a estar tranquilo.";


            }
            if (Contador1 == 1)
            {
                TextoPreguntas.text = "¿Por qué siempre soy yo el que tiene que aguantar estas cosas?";
                TextoBoton1.text = "Quizás porque no te haces respetar lo suficiente.";
                TextoBoton2.text = "Bueno, alguien tiene que hacerlo, ¿no?";
                TextoBoton3.text = "Debe ser agotador. A veces soltar un poco ayuda.";
            }
            if (Contador1 == 2)
            {
                TextoPreguntas.text = "No puedo más con tanta gente incompetente. ¡Me pone enfermo!";
                TextoBoton1.text = "A veces hay que aceptar que no todos son como tú.";
                TextoBoton2.text = "Es normal frustrarse. Puede que enfocarte en otra cosa te ayude.";
                TextoBoton3.text = "Quizás deberías hacer todo tú mismo si eso te molesta tanto.";
            }
            if (Contador1 == 3)
            {
                EstoyPregunta1 = false;
                ActivarCanvas.SetActive(false);
                Contador1 = 4;
                Terminado1 = true;
                FollowCamera.PlayerIn = true;
                postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn = true;
            }

            if (objectBrushRadii[0] == 20.0f) {  EstoyPregunta1 = false; ActivarCanvas.SetActive(false); postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn = true;  PlayerC.DentroCirculo = false; ContadorCinematica++; contadorParaFinal++;
            }
            if (objectBrushRadii[0] < 20.0f && Contador1 == 4) { objectBrushRadii[0] = 0.0f; EstoyPregunta1 = false; Destroy(Trigger1);  PlayerC.DentroCirculo = false; contadorParaFinal++; }
        }
        if (EstoyPregunta2) {
            if (Contador2 == 0)
            {
                TextoPreguntas.text = "Por más que lo intente, siempre siento que no encajo.";
                TextoBoton1.text = "Eso nos pasa a todos en algún momento.";
                TextoBoton2.text = "Con el tiempo encontrarás tu espacio.";
                TextoBoton3.text = "Quizás no estás buscando en el lugar correcto.";
            }
            if (Contador2 == 1)
            {
                TextoPreguntas.text = "Siempre termino sintiéndome vacío, como si nada tuviera sentido.";
                TextoBoton1.text = "Encontrar algo pequeño que te haga sentir bien podría ayudarte.";
                TextoBoton2.text = "Quizás simplemente necesitas algo nuevo en tu vida.";
                TextoBoton3.text = "Eso es normal cuando piensas demasiado.";
            }
            if (Contador2 == 2)
            {
                TextoPreguntas.text = "Es como si estuviera rodeado de gente, pero siempre estoy solo.";
                TextoBoton1.text = "Bueno, al menos tienes compañía. Eso ya es algo.";
                TextoBoton2.text = "Quizás te estás esforzando demasiado en encajar.";
                TextoBoton3.text = "Eso puede pasar. Conectar de verdad lleva tiempo.";
            }
            if (Contador2 == 3)
            {
                EstoyPregunta2 = false;
                ActivarCanvas.SetActive(false);
                Contador2 = 4;
                Terminado2 = true;
                FollowCamera.PlayerIn1 = true;
                postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f;
            }

            if (objectBrushRadii[1] == 20.0f) {  EstoyPregunta2 = false; ActivarCanvas.SetActive(false); postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn1 = true;  PlayerC.DentroCirculo = false; ContadorCinematica++; contadorParaFinal++;
            }
            if (objectBrushRadii[1] < 20.0f && Contador2 == 4) { objectBrushRadii[1] = 0.0f; EstoyPregunta2 = false; Destroy(Trigger2);  PlayerC.DentroCirculo = false; contadorParaFinal++; }
        }

        ///////////////////////


        if (EstoyPregunta3)
        {
            if (Contador3 == 0)
            {
                TextoPreguntas.text = "Todo lo que hago parece estar mal. No importa cuánto lo intente.";
                TextoBoton1.text = "A veces equivocarse también es parte del proceso.";
                TextoBoton2.text = "Quizás simplemente no lo estás pensando bien.";
                TextoBoton3.text = "Eso es porque estás siendo demasiado duro contigo mismo.";
            }
            if (Contador3 == 1)
            {
                TextoPreguntas.text = "Tengo miedo de hacer algo y que salga mal. No puedo moverme.";
                TextoBoton1.text = "Es mejor no pensar tanto y simplemente actuar.";
                TextoBoton2.text = "Seguro que no es tan grave como crees.";
                TextoBoton3.text = "Es normal sentir miedo. Lo importante es seguir adelante poco a poco.";
            }
            if (Contador3 == 2)
            {
                TextoPreguntas.text = "No puedo parar de pensar en todo lo que podría ir mal. Es agotador.";
                TextoBoton1.text = "Todos tenemos pensamientos así. No es nada nuevo.";
                TextoBoton2.text = "Pensar en el presente puede ayudarte a calmarte un poco.";
                TextoBoton3.text = "Eso es porque te concentras demasiado en los problemas.";
            }
            if (Contador3 == 3)
            {
                EstoyPregunta3 = false;
                ActivarCanvas.SetActive(false);
                Contador3 = 4;
                Terminado3 = true;
                FollowCamera.PlayerIn2 = true;
                postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f;
            }

            if (objectBrushRadii[2] == 20.0f) {  EstoyPregunta3 = false; ActivarCanvas.SetActive(false); postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn2 = true;  PlayerC.DentroCirculo = false; ContadorCinematica++; contadorParaFinal++;
            }
            if (objectBrushRadii[2] < 20.0f && Contador3 == 4) { objectBrushRadii[2] = 0.0f; EstoyPregunta3 = false; Destroy(Trigger3);  PlayerC.DentroCirculo = false; contadorParaFinal++; }
        }
        if (EstoyPregunta4)
        {
            if (Contador4 == 0)
            {
                TextoPreguntas.text = "Si no hago las cosas de cierta manera, siento que todo se va a descontrolar.";
                TextoBoton1.text = "Parece que esas rutinas te dan tranquilidad, pero podrías intentar cambiar pequeños detalles para reducir la presión.";
                TextoBoton2.text = "No necesitas ser tan estricto contigo mismo. Relájate un poco.";
                TextoBoton3.text = "Es solo una manía. No pasa nada si te saltas una vez tus reglas.";
            }
            if (Contador4 == 1)
            {
                TextoPreguntas.text = "Me paso horas asegurándome de que todo esté perfecto, y aun así siento que no es suficiente.";
                TextoBoton1.text = "Es mejor que aceptes que la perfección no existe.";
                TextoBoton2.text = "tal vez podrías definir un límite para no desgastarte tanto.";
                TextoBoton3.text = "Si te esfuerzas tanto, seguro que está bien. No te obsesiones.";
            }
            if (Contador4 == 2)
            {
                TextoPreguntas.text = "No puedo dejar de revisar si cerré la puerta. Si no lo hago, no puedo concentrarme en nada más.";
                TextoBoton1.text = "Es solo una puerta. Si lo comprobaste una vez, ya está bien.";
                TextoBoton2.text = "Eso le pasa a todos. Seguro que lo cerraste, no te preocupes.";
                TextoBoton3.text = "Intentar distraerte con otra actividad puede ayudarte.";
            }
            if (Contador4 == 3)
            {
                EstoyPregunta4 = false;
                ActivarCanvas.SetActive(false);
                Contador4 = 4;
                Terminado4 = true;
                FollowCamera.PlayerIn3 = true;

                postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f;

            }

            if (objectBrushRadii[3] == 20.0f) {  EstoyPregunta4 = false; ActivarCanvas.SetActive(false); postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn3 = true; PlayerC.DentroCirculo = false; ContadorCinematica++; contadorParaFinal++;
            }
            if (objectBrushRadii[3] < 20.0f && Contador4 == 4) { objectBrushRadii[3] = 0.0f; EstoyPregunta4 = false; Destroy(Trigger4);  PlayerC.DentroCirculo = false; contadorParaFinal++; }
        }
        if (EstoyPregunta5)
        {
            if (Contador5 == 0)
            {
                TextoPreguntas.text = "Siempre siento que algo malo va a pasar si intento hacer algo nuevo.";
                TextoBoton1.text = "Es normal tener dudas, pero no dejes de preocuparte.";
                TextoBoton2.text =  "Dar un paso pequeño puede ayudarte a ganar confianza.";
                TextoBoton3.text = "Si no estás seguro, mejor no lo intentes.";
            }
            if (Contador5 == 1)
            {
                TextoPreguntas.text = "No puedo dejar de pensar en todo lo que podría salir mal. Cada escenario parece peor que el anterior.";
                TextoBoton1.text = "Quizás deberías dejar de pensar tanto y enfocarte en ti mismo.";
                TextoBoton2.text = "Enfócate en lo que sí puedes controlar para aliviar ese peso.";
                TextoBoton3.text = "Eso es solo tu imaginación jugando contigo. Pero...¿Y si no lo és?";
            }
            if (Contador5 == 2)
            {
                TextoPreguntas.text = "Cuando algo no está bajo mi control, siento que no puedo ni respirar.";
                TextoBoton1.text = "Fácil, asegúrate de que todo esté bajo control y listo.";
                TextoBoton2.text = "Eso le pasa a todos, pero no dejes de intentarlo.";
                TextoBoton3.text = "Dejarlo ir poco a poco puede ser liberador";
            }

            if (Contador5 == 3)
            {
                EstoyPregunta5 = false;
                ActivarCanvas.SetActive(false);
                Contador5 = 4;
                Terminado5 = true;
                FollowCamera.PlayerIn4 = true;
                postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f;
            }

            if (objectBrushRadii[4] == 20.0f) {  EstoyPregunta5 = false; ActivarCanvas.SetActive(false); postProcessVolume.profile.TryGetSettings(out Vignette vignette);
                vignette.intensity.value = 0.0f; FollowCamera.PlayerIn4 = true; PlayerC.DentroCirculo = false; ContadorCinematica++; contadorParaFinal++;
            }
            if (objectBrushRadii[4] < 20.0f && Contador5 == 4) { objectBrushRadii[4] = 0.0f; EstoyPregunta5 = false; Destroy(Trigger5); PlayerC.DentroCirculo = false; contadorParaFinal++; }

        }

    }
    public void Boton1()
    { 
        if (EstoyPregunta1) 
        {
            if (Contador1 == 2) { objectBrushRadii[0] += 5.0f;  Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            if (Contador1 == 1) { objectBrushRadii[0] += 5.0f;  Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorIra.SetTrigger(triggerName);

            int randomStateS = Random.Range(1, 4);

            string triggerNameS = "Talk " + randomState;

            PlayerA.SetTrigger(triggerName);
            PlayerA.Play("ANI_Robot_02_Talk1_1");
            Contador1++;


        }
        if (EstoyPregunta2)
        {
            if (Contador2 == 2) { objectBrushRadii[1] += 5.0f; Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            if (Contador2 == 0) { objectBrushRadii[1] += 5.0f; Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorSoledad.SetTrigger(triggerName);
            Contador2++;

        }
        if (EstoyPregunta3)
        {
            if (Contador3 == 2) { objectBrushRadii[2] += 5.0f;  Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            if (Contador3 == 1) { objectBrushRadii[2] += 5.0f; Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorAnsiedad.SetTrigger(triggerName);
            Contador3++;

        }
        if (EstoyPregunta4)
        {
            if (Contador4 == 2) { objectBrushRadii[3] += 5.0f;  Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            if (Contador4 == 1) { objectBrushRadii[3] += 5.0f; Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorToc.SetTrigger(triggerName);
            Contador4++;

        }
        if (EstoyPregunta5)
        {
            
           
            if (Contador5 == 2) { objectBrushRadii[4] += 5.0f; Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            if (Contador5 == 0) { objectBrushRadii[4] += 5.0f;  Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorAprensivo.SetTrigger(triggerName);
            Contador5++;

        }



    }
    public void Boton2()
    {
        if (EstoyPregunta1)
        {
            if (Contador1 == 1) { objectBrushRadii[0] += 5.0f; Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            if (Contador1 == 0) { objectBrushRadii[0] += 5.0f;  Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorIra.SetTrigger(triggerName);
            Contador1++;


        }
        if (EstoyPregunta2)
        {
            if (Contador2 == 2) { objectBrushRadii[1] += 5.0f;  Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            if (Contador2 == 1) { objectBrushRadii[1] += 5.0f; Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorSoledad.SetTrigger(triggerName);
            Contador2++;

        }
        if (EstoyPregunta3)
        {
            if (Contador3 == 0) { objectBrushRadii[2] += 5.0f;  Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            if (Contador3 == 1) { objectBrushRadii[2] += 5.0f;  Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorAnsiedad.SetTrigger(triggerName);
            Contador3++;

        }
        if (EstoyPregunta4)
        {
            if (Contador4 == 2) { objectBrushRadii[3] += 5.0f; Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            if (Contador4 == 0) { objectBrushRadii[3] += 5.0f;  Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorToc.SetTrigger(triggerName);


            Contador4++;

        }
        if (EstoyPregunta5)
        {

            if (Contador5 == 2) { objectBrushRadii[4] += 5.0f;  Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            if (Contador5 == 1) { objectBrushRadii[4] += 5.0f;  Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorAprensivo.SetTrigger(triggerName);
            Contador5++;

        }
    }
    public void Boton3()
    {
        if (EstoyPregunta1)
        {
            if (Contador1 == 0) { objectBrushRadii[0] += 5.0f;  Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            if (Contador1 == 2) { objectBrushRadii[0] += 5.0f;  Trigger1.transform.localScale = Trigger1.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6); 

            string triggerName = "Talk " + randomState;

            animatorIra.SetTrigger(triggerName);

            Contador1++;

        }
        if (EstoyPregunta2)
        {
            if (Contador2 == 1) { objectBrushRadii[1] += 5.0f; Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            if (Contador2 == 0) { objectBrushRadii[1] += 5.0f; Trigger2.transform.localScale = Trigger2.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorSoledad.SetTrigger(triggerName);
            Contador2++;

        }
        if (EstoyPregunta3)
        {
            if (Contador3 == 2) { objectBrushRadii[2] += 5.0f; Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            if (Contador3 == 0) { objectBrushRadii[2] += 5.0f;  Trigger3.transform.localScale = Trigger3.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorSoledad.SetTrigger(triggerName);
            Contador3++;

        }
        if (EstoyPregunta4)
        {
            if (Contador4 == 1) { objectBrushRadii[3] += 5.0f;  Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            if (Contador4 == 0) { objectBrushRadii[3] += 5.0f;  Trigger4.transform.localScale = Trigger4.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 5);

            string triggerName = "Talk " + randomState;

            animatorToc.SetTrigger(triggerName);
            Contador4++;

        }
        if (EstoyPregunta5)
        {
           
           
            if (Contador5 == 0) { objectBrushRadii[4] += 5.0f; Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            if (Contador5 == 1) { objectBrushRadii[4] += 5.0f;  Trigger5.transform.localScale = Trigger5.transform.localScale * 1.5f; }
            int randomState = Random.Range(1, 6);

            string triggerName = "Talk " + randomState;

            animatorAprensivo.SetTrigger(triggerName);
            Contador5++;

        }
    }

    void DoSomethingOnCustomTrigger1()
    {
        
        //postProcessVolume.profile.TryGetSettings(out Vignette vignette);
        //vignette.intensity.value = 0.7f;
        if (!Terminado2)
        {
            ActivarCanvas.SetActive(true);
            EstoyPregunta2 = true;
            FollowCamera.PlayerIn1 = false;
            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;
            PlayerC.DentroCirculo = true;


        }

    }



    void DoSomethingOnCustomTrigger2()
    {
        if (!Terminado3)
        {
            ActivarCanvas.SetActive(true);
            EstoyPregunta3 = true;
            FollowCamera.PlayerIn2 = false;
            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;
            PlayerC.DentroCirculo = true;

        }
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger3()
    {
        if (!Terminado4)
        {
            ActivarCanvas.SetActive(true);
            EstoyPregunta4 = true;
            FollowCamera.PlayerIn3 = false;
            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;
            PlayerC.DentroCirculo = true;
        }
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger4()
    {
        if (!Terminado5)
        {
            ActivarCanvas.SetActive(true);
            EstoyPregunta5 = true;
            FollowCamera.PlayerIn4 = false;
            postProcessVolume.profile.TryGetSettings(out Vignette vignette);
            vignette.intensity.value = 0.7f;
            PlayerC.DentroCirculo = true;

        }
        // Aquí puedes agregar la lógica adicional que desees

    }


    void Update()
    {
        FuntionActive();
        CargaTextosDialogo1();
        if (ContadorCinematica >= 3) { FinalMalo = true; FinalBueno = false; }
        if (contadorParaFinal == 5) { Destroy(Barrera4); }
       
    }

    public void FuntionActive()
    {
        
        
            // Limpiar la textura al inicio de cada frame
            ClearTexture();

            // Lanzar raycasts desde cada objeto
            for (int i = 0; i < objects.Length; i++)
            {
                var obj = objects[i];
                if (obj == null) continue;

                // Obtener el radio correspondiente al objeto
                float brushRadius = objectBrushRadii[i];

                Ray ray = new Ray(obj.transform.position, Vector3.down);
                //Debug.DrawRay(obj.transform.position, Vector3.down * 50f, Color.green); // Dibuja el raycast

                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        //Debug.Log($"El jugador ha pasado por el raycast del objeto: {obj.name}");

                        // Incrementar el radio del pincel para este raycast
                        //objectBrushRadii[i] += 0.01f; // Incremento, ajusta este valor según sea necesario
                        //Debug.Log($"Nuevo radio del pincel para {obj.name}: {objectBrushRadii[i]}");
                    }

                    if (hit.collider.gameObject == gameObject) // Asegurar que impacta el plano
                    {
                        Vector2 textureCoord = hit.textureCoord; // Coordenadas UV
                        PaintOnTexture(textureCoord, brushRadius); // Usar radio específico
                    }
                }
            }

            // Aplicar los cambios a la textura
            texture.Apply();
        
    }


    void ClearTexture()
    {
        if (baseTexture != null)
        {
            // Copiar los píxeles de la textura base
            Color[] basePixels = baseTexture.GetPixels();
            texture.SetPixels(basePixels);
        }
        else
        {
            // Si no hay textura base, rellenar con el color predeterminado
            Color[] pixels = new Color[textureSize * textureSize];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = defaultColor;
            }
            texture.SetPixels(pixels);
        }
    }

    void PaintOnTexture(Vector2 uv, float brushRadius)
    {
        int x = Mathf.RoundToInt(uv.x * textureSize);
        int y = Mathf.RoundToInt(uv.y * textureSize);

        for (int i = -Mathf.CeilToInt(brushRadius); i <= Mathf.CeilToInt(brushRadius); i++)
        {
            for (int j = -Mathf.CeilToInt(brushRadius); j <= Mathf.CeilToInt(brushRadius); j++)
            {
                int px = x + i;
                int py = y + j;

                if (px >= 0 && px < textureSize && py >= 0 && py < textureSize)
                {
                    float distance = Mathf.Sqrt(i * i + j * j);
                    if (distance <= brushRadius)
                    {
                        float intensity = 1 - (distance / brushRadius);
                        Color currentColor = texture.GetPixel(px, py);
                        Color blendedColor = Color.Lerp(currentColor, paintColor, intensity);
                        texture.SetPixel(px, py, blendedColor);
                    }
                }
            }
        }
    } 


}