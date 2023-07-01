using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborA : MonoBehaviour
{
    AudioSource ad;
    CameraShake camera;
    public GameObject maincam;
    public GameObject redscreen;

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
            collision.GetComponent<PlayerController>().Trap();
            ad.Play();
            camera.onhit = true;
            camera.VibrateForTime(3f);
            redscreen.SetActive(true);
            Invoke("screenoff", 3f);
        }
        Invoke("objoff", 5f);
    }

    void screenoff()
    {
        redscreen.SetActive(false);
    }

    void objoff()
    {
        gameObject.SetActive(false);
    }
}
