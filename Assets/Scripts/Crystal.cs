using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Materials")]
    public Material crystalUnlit;
    public Material crystalLit;

    [Header("IMPORTANT PUZZLE INDEX")]
    public string puzzleRef;

    PlayerSelect playerSelectScript;
    MeshRenderer myMesh;
    flameScript flameScript;

    [SerializeField]
    bool crystalActive;
    public int currentKeyNumber;

	void Start ()
    {
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        myMesh = GetComponent<MeshRenderer>();
        flameScript = GameObject.Find("BlueFlame").GetComponent<flameScript>();
    }
	
	void Update ()
    {
        ///////////////////IF INDEX IS == 1 THEN LIGHT UP BLUE///////////////////////
        if (currentKeyNumber == flameScript.currentKeyNumber)
        {
            myMesh.material = crystalLit;
        }
        else if(currentKeyNumber >= flameScript.currentKeyNumber)
        {
            currentKeyNumber = flameScript.currentKeyNumber;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ///////////////////////IF TEXT IS ENABLED THEN (E) WILL INCREASE INDEX/////////////////////
            playerSelectScript.crystalText.enabled = true;
            if (Input.GetKey(KeyCode.E))
            {
                currentKeyNumber++;
            }
        }
    } 
}
