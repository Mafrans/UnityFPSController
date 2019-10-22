using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;
    public float velocityRequirement;
    public MouseInteract interact;
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(interact.pickedUpItem != this.gameObject) {
            if(other.relativeVelocity.magnitude > velocityRequirement) {
                GameObject go = Instantiate(explosionEffect);
                go.transform.position = transform.position;
                go.transform.up = other.contacts[0].normal;
                go.transform.localScale *= size;
                Destroy(gameObject);
            }
        }
    }
}
