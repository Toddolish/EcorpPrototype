using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Materials")]
    public Material crystalUnlit;
    public Material crystalLit;

    PlayerSelect playerSelectScript;
    MeshRenderer myMesh;

	void Start ()
    {
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        myMesh = GetComponent<MeshRenderer>();
    }
	
	void Update ()
    {
        if(playerSelectScript.crystalActive)
        {
            myMesh.material = crystalLit;

        }

    }
}
