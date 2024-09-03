using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

   public AudioSource accelerateSound;
   public AudioSource breakSound;

   // Start is called before the first frame update
   void Start()
    {
        
    }

   // Update is called once per frame
   void Update()
   {
      while (Input.anyKeyDown)
      {
         if (Input.GetKeyDown(KeyCode.UpArrow)) { accelerateSound.Play(); }
         if (Input.GetKeyDown(KeyCode.DownArrow)) { breakSound.Play(); }
      }
   }
}
