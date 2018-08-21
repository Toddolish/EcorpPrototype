using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnim;
    PlayerSelect playerSelectScript;
    Crystal crystalScript;

    public int index = -1;
    public int currentKeyNumber;

    public enum state
    {
        closed, open
    }
    public state curState = state.closed;

    void Start()
    {
        doorAnim = this.gameObject.GetComponent<Animator>();
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        crystalScript = GameObject.Find("Crystal").GetComponent<Crystal>();
    }
    void Update()
    {
        if(currentKeyNumber == crystalScript.currentKeyNumber)
        {
            curState = state.open;
        }
        switch (curState)
        {
            case state.closed:
                //DOOR IS CLOSED
                doorAnim.SetBool("open", false);
                break;

            case state.open:
                //DOOR IS OPEN
                doorAnim.SetBool("open", true);
                break;
        }
    }
}
