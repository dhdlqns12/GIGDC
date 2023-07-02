using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborA : MonoBehaviour
{
    AudioSource ad;
    CameraShake camera;
    public GameObject maincam;
    public GameObject redscreen;
    public GameObject dialogCollider12;
    public bool isEffect = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = maincam.GetComponent<CameraShake>();
        ad = GetComponent<AudioSource>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isEffect = true;
            collision.GetComponent<PlayerController>().Trap();
            ad.Play();            
            camera.onhit = true;
            camera.VibrateForTime(3f);
            redscreen.SetActive(true);

            Invoke("screenoff", 3f);
            Invoke("objoff", 3f);
        }
    }

    void screenoff()
    {
        redscreen.SetActive(false);
        dialogCollider12.SetActive(true);
        isEffect = false;
    }

    void objoff()
    {
        gameObject.SetActive(false);
    }
}
