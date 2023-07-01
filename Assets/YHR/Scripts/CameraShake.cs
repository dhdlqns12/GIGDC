using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float ShakeAmount;
    //public Canvas canvas;
    float ShakeTime = 0;
   public bool onhit = false;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onhit)
        {

            if (ShakeTime > 0)

            {

                transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;

                ShakeTime -= Time.deltaTime;

            }

            else

            {

                ShakeTime = 0.0f;

                transform.position = initialPosition;

                //canvas.renderMode = RenderMode.ScreenSpaceCamera;
            }
        }
    }

    public void VibrateForTime(float time)

    {
        initialPosition = new Vector3(241.72f, -46.61f, -10f);
        ShakeTime = time;

        //canvas.renderMode = RenderMode.ScreenSpaceCamera;

        //canvas.renderMode = RenderMode.WorldSpace;

    }
}
