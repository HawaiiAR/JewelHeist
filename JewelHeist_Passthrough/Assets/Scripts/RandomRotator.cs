using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    [SerializeField] private float _speed;

    [SerializeField] private float _time;
    [SerializeField] private float _rotInterval;

    private int _dirX;
    private int _dirY;
    private int _dirZ;

    public bool startSpinning;
    bool _switchDirections;

    private float rotationDuration = 3f; // Adjust the total duration of rotation as needed
    private float rotationTimer;

    private Quaternion targetRotation;
    private Quaternion lastRotation;


    // Start is called before the first frame update
    void Start()
    {
        lastRotation = Quaternion.Euler(35, 35, 0);
        _switchDirections = true;
        startSpinning = false;
        _time = 0;
        GenerateRandomRotation();
        //  NewRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpinning)
        {
            RotateSphere();

     
        }
    }


    public void GenerateRandomRotation()
    {
        // Generate a new rotation that is at least 35 degrees from the previous rotation
        float minAngleBelow = 35f;
        float maxAngleBelow = 180f - minAngleBelow; // Ensure the new rotation is outside the 45-degree cone below

        // Generate a new rotation that is at least 25 degrees from the previous rotation
        float minAngleAbove = 25f;
        float maxAngleAbove = 360f - minAngleAbove; // Ensure the new rotation is outside the 25-degree cone above


        targetRotation = Quaternion.Euler(
            Random.Range(maxAngleBelow, maxAngleAbove),
            Random.Range(0f, 360f),
            0f
        ) * lastRotation;

        Debug.Log("target" + targetRotation);
        Debug.Log("Last" + lastRotation);
        // Update the last rotation
        lastRotation = targetRotation;

        // Reset the rotation timer
        rotationTimer = 0f;
    }

    private void RotateSphere()
    {
        Debug.Log("RotateSphere");
        // Update the rotation timer
        // rotationTimer += Time.deltaTime;

        // Interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);

        // Check if the rotation duration has passed, then generate a new random rotation
        /* if (rotationTimer >= rotationDuration)
         {
             GenerateRandomRotation();
         }*/
    }
}

