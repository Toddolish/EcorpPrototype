using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnim;
    PlayerSelect playerSelectScript;

    [Header("IMPORTANT PUZZLE INDEX")]
    public string puzzleRef;

    Crystal crystalScript;

    void Start()
    {
        doorAnim = this.gameObject.GetComponent<Animator>();
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();

        crystalScript = GameObject.Find("Crystal").GetComponent<Crystal>();
    }
    void Update()
    {
        if (puzzleRef == crystalScript.puzzleRef)
        {
            if (playerSelectScript.crystalActive == true)
            {
                doorAnim.SetBool("open", true);
                playerSelectScript.FlameCount = 0;
            }
        }
    }
}
