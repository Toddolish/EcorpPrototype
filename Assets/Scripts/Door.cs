using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnim;
    PlayerSelect playerSelectScript;

    void Start()
    {
        doorAnim = this.gameObject.GetComponent<Animator>();
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
    }
    void Update()
    {
        if(playerSelectScript.crystalActive == true)
        {
            doorAnim.SetBool("open", true);
            playerSelectScript.FlameCount = 0;
        }
    }
}
