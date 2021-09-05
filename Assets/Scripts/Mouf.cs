using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouf : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var fruit = other.gameObject.GetComponent<Fruit>();
        if (fruit)
        {
            var c = FindObjectOfType<EaterController>(true);
            c.character.gameObject.SetActive(true);
            c.character.GiveControl(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
