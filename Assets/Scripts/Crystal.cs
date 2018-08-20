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

	void Start ()
    {
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        myMesh = GetComponent<MeshRenderer>();
        flameScript = GameObject.Find("BlueFlame").GetComponent<flameScript>();
    }
	
	void Update ()
    {
        if (puzzleRef == flameScript.puzzleRef)
        {
            if (playerSelectScript.crystalActive)
            {
                myMesh.material = crystalLit;
            }
        }

    }
}
