using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SausagMan : MonoBehaviour
{
public List<AudioClip> clips;
    public bool hasCarrot, hasSausage, hasLemon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerMovement>())
        {
            return;
        }
        var pm = FindObjectOfType<PlayerMovement>();
        var ca = pm.GetCurrentActive();
        var ec = pm.eater;

        if (ca.name == "Sidrun" && !hasLemon)
        {
            hasLemon = true;
            
            ec.MorphBack(other.gameObject);
            // Destroy(other.gameObject);

        }
        else if (ca.name == "Vorst" && !hasSausage)
        {
            hasSausage = true;
            ec.MorphBack(other.gameObject);
            // Destroy(other.gameObject);

        }
        else if (ca.name == "Porg" && !hasCarrot)
        {
            hasCarrot = true;
            ec.MorphBack(other.gameObject);
            // Destroy(other.gameObject);

        }

        var findObjectOfType = FindObjectOfType<AudioSource>();
        if (pm.characters.Count == 1)
        {
            findObjectOfType.clip = clips[1];
            findObjectOfType.Play ();
        }

        if (hasCarrot && hasLemon && hasSausage)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Thank you for bringing me back my food :) And thank you for playing";
            findObjectOfType.clip = clips[2];
            findObjectOfType.Play ();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
