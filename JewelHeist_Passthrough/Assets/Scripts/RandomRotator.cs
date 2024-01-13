using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lasers;

public class RandomRotator : MonoBehaviour
{
 

    [SerializeField] private float _speed;

    [SerializeField] private float _time;
    [SerializeField] private float _rotInterval;


    public bool startSpinning;
    

    private float rotationDuration = 3f; // Adjust the total duration of rotation as needed
    private float rotationTimer;

    private Quaternion targetRotation;
    private Quaternion lastRotation;

    private List<Quaternion> _angles = new List<Quaternion>();

   

    // Start is called before the first frame update
    void Start()
    {
        lastRotation = Quaternion.Euler(10, 10, 0);
        _angles.Add(lastRotation);
        startSpinning = false;
        _time = 0;
      //  GenerateRandomRotation();
     
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

         NewAngle(lastRotation);

         if (_angles.Contains(targetRotation))
         {
             NewAngle(lastRotation);
             Debug.Log("newRotation");
         }
         else
         {
             Debug.Log("Rotation Reached");
             lastRotation = targetRotation;
             _angles.Add(targetRotation);
         }


         Debug.Log("target" + targetRotation);
         Debug.Log("Last" + lastRotation);
         // Update the last rotation
         lastRotation = targetRotation;

         // Reset the rotation timer
         rotationTimer = 0f;
     }

     private Quaternion NewAngle(Quaternion randAngle)
     {
         float minAngleBelow = 35f;
         float maxAngleBelow = 180f - minAngleBelow;


         float minAngleAbove = 25f;
         float maxAngleAbove = 360f - minAngleAbove; // Ensure the new rotation is outside the 25-degree cone above


         randAngle = Quaternion.Euler(
             Random.Range(maxAngleBelow, maxAngleAbove),
             Random.Range(0f, 360f),
             0f
         ) * lastRotation;

         targetRotation = randAngle;

         return targetRotation;
     }

    private void RotateSphere()
    {
        Debug.Log("RotateSphere");

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);

    }

}

