using System;
using System.Collections;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] public GameObject Door;
    [SerializeField] public AudioSource SFX;
    SpriteRenderer sp;
    CircleCollider2D coll;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(Door);
            SFX.Play();
            sp.enabled = false;
            coll.enabled = false;
            Destroy(gameObject,SFX.clip.length);

        }
        
    }

    
    
}

