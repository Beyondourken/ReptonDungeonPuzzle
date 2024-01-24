using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour
{

    [SerializeField] public AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Crush") {
            audioSource.Play();
        Destroy(gameObject, 1);}
    }
}
