using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
   [Range(1f, 50)]
   public float SpeedButtons;

   [Range(1f, 100)]
   public float SpeedMouse;

   void Start()
   {
      SpeedButtons =  20;
      SpeedMouse = 50;
   }

   void Update()
   {
      // Axes Position - .Translate (X,Y,Z); 
      transform.Translate(SpeedButtons * Time.deltaTime * Input.GetAxis("Horizontal"), 0, SpeedButtons * Time.deltaTime * Input.GetAxis("Vertical"));

      // Axes Rotation - .Translate (X,Y,Z); 
      transform.Rotate(-SpeedMouse * Time.deltaTime * Input.GetAxis("Mouse Y"), SpeedMouse * Time.deltaTime * Input.GetAxis("Mouse X"), 0);

      // The Camera stands at zero on the z-axis
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

      if (Input.GetKey("space")) { transform.Translate(0, SpeedButtons * Time.deltaTime, 0); }
      if (Input.GetKey("c")) { transform.Translate(0, -SpeedButtons * Time.deltaTime, 0); }

   }
}
