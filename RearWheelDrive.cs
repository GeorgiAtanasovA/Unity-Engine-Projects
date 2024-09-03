using UnityEngine;
using UnityEngine.UI;

public class RearWheelDrive : MonoBehaviour
{
   private WheelCollider[] wheels;

   public float maxAngle = 30;
   public float maxTorque = 700;

   public GameObject wheelShapeLeft; //Left wheels - edited!!!
   public GameObject wheelShapeRight; //Right wheels - edited!!!
   public AudioSource EngineSound; // edited!!!
   public AudioSource HandbreakSound; // edited!!!

   //-----Georgi-----
   bool checkHandBrSound = true;
   float numbAccelerate;
   int gear = 1;
   public Text gearText;
   public Text pitch;
   public Text Rpm;


   // here we find all the WheelColliders down in the hierarchy
   public void Start()
   {
      EngineSound.Play();

      wheels = GetComponentsInChildren<WheelCollider>();

      for (int i = 0; i < wheels.Length; ++i)
      {
         var wheel = wheels[i];

         //Left and Rught Wheels
         if (wheel.transform.localPosition.x > 0)
         {
            // create wheel shapes only when needed
            if (wheelShapeRight != null)
            {
               var ws = GameObject.Instantiate(wheelShapeRight);
               ws.transform.parent = wheel.transform;
            }
         }
         else//edited!!!
         {
            // create wheel shapes only when needed
            if (wheelShapeLeft != null)
            {
               var ws = GameObject.Instantiate(wheelShapeLeft);
               ws.transform.parent = wheel.transform;
            }
         }
      }
   }

   // this is a really simple approach to updating wheels
   // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
   // this helps us to figure our which wheels are front ones and which are rear
   public void Update()
   {
      float angle = maxAngle * Input.GetAxis("Horizontal");
      float torque = maxTorque * Input.GetAxis("Vertical");

      foreach (WheelCollider wheel in wheels)
      {
         //--------------------------Methods - edited------------------------------
         #region
         //      Footbrake / Handbrake
         if (Input.GetKey("down") || Input.GetKey("s"))
         {
            // HandbreakSound.Play() - for playing only one time, start sound loop
            if (checkHandBrSound == true) { HandbreakSound.Play(); checkHandBrSound = false; }
            if (wheel.rpm > 0) { wheel.brakeTorque = 5000; } else { wheel.brakeTorque = 0; }

         }
         else if (Input.GetKey("space"))
         {
            // HandbreakSound.Play() - for playing only one time, start sound loop
            if (checkHandBrSound == true) { HandbreakSound.Play(); checkHandBrSound = false; }
            // Handbrake on rear wheels
            if (wheel.transform.localPosition.z < 0) { wheel.brakeTorque = 5000; }
         }
         else
         {
            wheel.brakeTorque = 0;

            if (checkHandBrSound == false)
            {
               HandbreakSound.Stop(); checkHandBrSound = true;
            }
         }

         // increase / decrease pitch on only one wheels
         if (wheel.transform.localPosition.z > 0 && wheel.transform.localPosition.x < 0)
         {
            numbAccelerate = 0.005f / gear;
            EngineSound.pitch = wheel.rpm * numbAccelerate;

            //Change Gear Up
            if (EngineSound.pitch > 3.6 && gear < 5)
            {
               EngineSound.pitch = 2.0f; gear += 1;
            }
            //Change Gear Down
            else if (EngineSound.pitch < 1 && gear > 1)
            {
               EngineSound.pitch = 3.2f; gear -= 1;
            }
         }


         // pitch min / max correction
         if (EngineSound.pitch < 0.4) { EngineSound.pitch = 0.4f; }
         else if (EngineSound.pitch > 3.6) { EngineSound.pitch = 3.6f; }


         Rpm.text = wheel.rpm.ToString();
         gearText.text =gear.ToString();
         pitch.text = EngineSound.pitch.ToString();
         #endregion
         //-----------------------------------------------------------------

         // a simple car where front wheels steer while rear ones drive
         if (wheel.transform.localPosition.z > 0) { wheel.steerAngle = angle; }


         //if (wheel.transform.localPosition.z > 0)  // front drive
         //if (wheel.transform.localPosition.z < 0)  // rear drive
            wheel.motorTorque = torque; // 4x4 drive


         //update visual wheels if any
         if (wheelShapeRight)  //Right wheels - edited!!!
         {
            Vector3 p;
            Quaternion q;
            wheel.GetWorldPose(out p, out q);

            // assume that the only child of the wheelcollider is the wheel shape
            Transform shapeTransform = wheel.transform.GetChild(0);
            shapeTransform.position = p;
            shapeTransform.rotation = q;
         }
         if (wheelShapeLeft)  //Left wheels - edited!!!
         {
            Vector3 p;
            Quaternion q;
            wheel.GetWorldPose(out p, out q);

            // assume that the only child of the wheelcollider is the wheel shape
            Transform shapeTransform = wheel.transform.GetChild(0);
            shapeTransform.position = p;
            shapeTransform.rotation = q;
         }
      }
   }
}
