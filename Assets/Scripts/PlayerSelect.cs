using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public Text flameText;
    public Text crystalText;
    public GameObject Player;
    public GameObject mainCam;

    [SerializeField]
    public int FlameCount = 0;
    flameScript flamerScript;
    Crystal crystalScript;
    Enemy enemyScript;

    [SerializeField]
    float timer;
    public bool FlamePickedUp;
    public bool neerCrystalLock;
    public bool crystalActive;

    [Header("Lights")]
    public Light lanternLight;
    public bool lightIsOn;

    Door doorScript;

	void Start ()
    {
        Player = GameObject.Find("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        flamerScript = GameObject.Find("BlueFlame").GetComponent<flameScript>();
        flameText =  GameObject.Find("Flame_Text").GetComponent<Text>();
        flameText.enabled = false;

        crystalScript = GameObject.Find("Crystal").GetComponent<Crystal>();
        crystalText = GameObject.Find("Crystal_Text").GetComponent<Text>();
        crystalText.enabled = false;
        crystalActive = false;

        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();

        #region Light
        lanternLight = GameObject.Find("Lantern_Light").GetComponent<Light>();
        lanternLight.enabled = false;
        lightIsOn = false;
        #endregion

    }
	
	void Update ()
    {
        Light();
        //collectFlame();
        //activateCrystal();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "KEY")
        {
            flameText.enabled = true;
        }
        if(other.gameObject.tag =="LOCK")
        {
            neerCrystalLock = true;
        }
        if (other.gameObject.tag == "CRYSTAL")
        {
            crystalText.enabled = true;
            //neerCrystalLock = true;
        }
        if(other.gameObject.tag == "chapter1")
        {
            enemyScript.currentState = Enemy.State.Patrol;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "KEY")
        {
            flameText.enabled = false;
        }
        if (other.gameObject.tag == "LOCK")
        {
            neerCrystalLock = false;
        }
        if (other.gameObject.tag == "CRYSTAL")
        {
            crystalText.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "KEY")
        {
            flameText.enabled = false;
        }
        if (other.gameObject.tag == "LOCK")
        {
            neerCrystalLock = false;
        }
        if (other.gameObject.tag == "CRYSTAL")
        {
            crystalText.enabled = false;
        }
    }
    void Raycasting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;

            if (Physics.Raycast(interact, out hitInfo, 10f))
            {
                #region NPC
                if (hitInfo.collider.tag == "NPC")
                {
                    Debug.Log("ooooo Npc");
                }
                #endregion

                #region CHEST
                if (hitInfo.collider.tag == "CHEST")
                {
                    Debug.Log("Open Chest");
                }
                #endregion

                #region ITEM
                if (hitInfo.collider.tag == "ITEM")
                {
                    Debug.Log("Pick up Item");
                }
                #endregion
            }
        }
    }
    void Light()
    {
        if(Input.GetKeyDown(KeyCode.L) && !lightIsOn)
        {
            lanternLight.enabled = true;
            lightIsOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.L) && lightIsOn)
        {
            lanternLight.enabled = false;
            lightIsOn = false;
        }
    }
    /*void collectFlame()
    {
        if (FlamePickedUp)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                FlamePickedUp = false;
            }
        }
        if (flameText.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Collect Flame
                FlameCount ++;
                flameText.enabled = false;
                flamerScript.followingPlayer = true;
                FlamePickedUp = true;
            }
        }
    }*/
    /*void activateCrystal()
    {
        if(crystalText.enabled == true && flamerScript.followingPlayer)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                crystalScript.ActiveIndex++;
            }
        }
    }*/
}
