using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    public Transform target;
    public float mSmooth;
    public bool PlayerIn;
    public Transform Point;

    private Vector3 mOffset, mTargetCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        PlayerIn = true;
        mOffset = transform.position - target.position;
        

        //mOffset = transform.position - Point.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIn)
        {
            mTargetCamPosition = target.position + mOffset;


            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, mSmooth * Time.deltaTime);


        }
        /*else {
            mTargetCamPosition = Point.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mTargetCamPosition.x, mTargetCamPosition.y, mTargetCamPosition.z), mSmooth * Time.deltaTime);

            Quaternion targetRotation = Point.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, mSmooth * Time.deltaTime);
        }*/

    }
}
