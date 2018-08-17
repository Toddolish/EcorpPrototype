using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameScript : MonoBehaviour {

    public PlayerSelect PlayerPickupScript;
    public Transform HolsterTarget;
    public Transform crystalTarget;
    BoxCollider FlameCollider;

    Crystal crystalScript;

    [Header("Flame Follow Speed")]
    public float speed;

    public enum State
    {
        seekCrystal, seekPlayer, noTarget
    }
    public State currentState = State.noTarget;
    

    void Start()
    {
        PlayerPickupScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        HolsterTarget = GameObject.Find("FlameHolster").GetComponent<Transform>();
        FlameCollider = GetComponent<BoxCollider>();
        crystalScript = GameObject.Find("Crystal").GetComponent<Crystal>();
        crystalTarget = GameObject.Find("CrystalHolster").GetComponent<Transform>();
    }

    void Update()
    {
        //transform.RotateAround(Vector3.up, new Vector3(0,0,0), speed * Time.deltaTime * 2);
        switch (currentState)
        {
            case State.noTarget:
                FlameCollider.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
                break;

            case State.seekPlayer:
                FlameCollider.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, HolsterTarget.position, speed * Time.deltaTime);
                break;

            case State.seekCrystal:
                PlayerPickupScript.FlameFollow = false;
                FlameCollider.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, crystalTarget.position, speed * Time.deltaTime);
                break;
        }

        if (PlayerPickupScript.FlameFollow)
        {
            currentState = State.seekPlayer;
        }
        if (PlayerPickupScript.crystalActive)
        {
            currentState = State.seekCrystal;
        }

    }
}
