using UnityEngine;

public class CameraController : MonoBehaviour
{

    enum CameraState { Orbital, Close };
    CameraState cameraState;

    Vector3 cameraPosition;
    Vector3 cameraRotation;

    float[] limites = new float[4]; 

    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    void StateMachine()
    {
        switch(cameraState)
        {
            case CameraState.Orbital:
                Orbital();
                break;
            case CameraState.Close:
                Close();
                break;
        }
    }

    void Orbital()
    {
        /// Limites
        // Superior

        // Inferior
        // Izquierdo
        // Derecho



        transform.position = player.transform.position;
    }

    void Close()
    {

    }
}
