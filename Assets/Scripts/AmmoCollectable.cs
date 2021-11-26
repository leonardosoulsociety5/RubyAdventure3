using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    public AudioClip collectedClip;
    public GameObject Particle;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        print("***Entering trigger by " + other.gameObject.name);
        if (controller != null)
        {

                controller.cogs+=4;
                controller.uptadecogstext();
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            
        }
    }
}




