using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    public Transform target,MainTarget;
    public float mSmooth;
    public bool PlayerIn, PlayerIn1,PlayerIn2,PlayerIn3,PlayerIn4;
    public Transform Point,Point1,Point2,Point3,Point4;

    private Vector3 mOffset, mTargetCamPosition;

    private Vector3 originalPosition; // Posición original de la cámara
    private Quaternion originalRotation; // Rotación original de la cámara

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = target.transform.position;
        originalRotation = transform.rotation;

        PlayerIn = true;
        PlayerIn1 = true;
        PlayerIn2 = true;
        PlayerIn3 = true;
        PlayerIn4 = true;

        mOffset = transform.position - target.position;
        

        //mOffset = transform.position - Point.position;
    }

    // Update is called once per frame
    void Update()
    {
        //mTargetCamPosition = target.position + mOffset;


        //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);


        if (PlayerIn)
        {
            mTargetCamPosition = target.position + mOffset;


            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        else
        {
            //mTargetCamPosition = Point.position;
           
            //transform.position = Point.transform.position;
            //transform.rotation= Point.transform.rotation;

            mTargetCamPosition = Point.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            /*Quaternion targetRotation = Point.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, mSmooth * Time.deltaTime);*/

        }

        if (PlayerIn1)
        {
            //mTargetCamPosition = target.position + mOffset;


            //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        else
        {
            mTargetCamPosition = Point1.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);
        }
        if (PlayerIn2)
        {
            //mTargetCamPosition = target.position + mOffset;


            //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        else
        {
            mTargetCamPosition = Point2.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);
        }
        if (PlayerIn3)
        {
            //mTargetCamPosition = target.position + mOffset;


            //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        else
        {
            mTargetCamPosition = Point3.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);
        }
        if (PlayerIn4)
        {
            //mTargetCamPosition = target.position + mOffset;


            //transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        else
        {
            mTargetCamPosition = Point4.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);
        }

    }
}
