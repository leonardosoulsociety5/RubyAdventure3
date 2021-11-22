using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
  public AudioClip collectedClip;
  public GameObject Ruby;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {       
            Destroy(gameObject);
            
        }
    }
}
