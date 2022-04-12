using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public static soundmanager Instance;// here we make  a instance of soundmanager
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;// we have to cotinue the time that we stop when the game is over
        if(Instance==null)// check if instance is present or not
        {
            Instance = this;// if not present that make sure to assign it.
        }
        else
        {
            Destroy(this.gameObject);// if we have more than of 1 instance then destroy this one
        }
        
    }

    public void PlaySound()
    {
        audioSource.Play();// simply play the sound
    }
}
