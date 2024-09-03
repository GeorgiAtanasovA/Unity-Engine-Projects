using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
   public List<GameObject> PushBreak;
   public List<GameObject> LightsBackFront;
   //public new AudioSource audio;
   bool lightsSwitch = true;

   //void Start() { audio.Play(); }

   void Update()
   {
      //---------------------------------------
      if (Input.GetKeyDown("g") && lightsSwitch == true)
      {
         lightsSwitch = false;
      }
      else if (Input.GetKeyDown("g") && lightsSwitch == false)
      {
         lightsSwitch = true;
      }

      foreach (var light in LightsBackFront)
      {
         if (Input.GetKeyDown("g") && lightsSwitch == true)
         {
            light.SetActive(false);
         }
         else if (Input.GetKeyDown("g") && lightsSwitch == false)
         {
            light.SetActive(true);
         }
      }

      //---------------------------------------
      foreach (var areaLights in PushBreak)
      {
         if (Input.GetKey("up") || Input.GetKey("w"))
         {
            //EngineSound_Up();
         }
         else if (Input.GetKey("down") || Input.GetKey("s"))
         {
            areaLights.SetActive(true);
            //EngineSound_Down();
         }
         else
         {
            areaLights.SetActive(false);

            //engine's audio without gas - edited
            //EngineSound_Down();
         }
      }
   }

   //public void EngineSound_Up()
   //{
   //   if (audio.pitch <= 3.5) { audio.pitch += 0.001f; }
   //}

   //public void EngineSound_Down()
   //{
   //   if (audio.pitch >= 0.5f) { audio.pitch -= 0.005f; }
   //}
}
