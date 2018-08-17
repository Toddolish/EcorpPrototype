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

    [SerializeField]
    float timer;
    public bool FlamePickedUp;
    public bool FlameFollow;
    public bool neerCrystalLock;
    public bool crystalActive;

    [Header("Lights")]
    public Light lanternLight;
    public bool lightIsOn;

	void Start ()
    {
        Player = GameObject.Find("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        flameText =  GameObject.Find("Flame_Text").GetComponent<Text>();
        flameText.enabled = false;

        crystalText = GameObject.Find("Crystal_Text").GetComponent<Text>();
        crystalText.enabled = false;
        crystalActive = false;

        #region Light
        lanternLight = GameObject.Find("Lantern_Light").GetComponent<Light>();
        lanternLight.enabled = false;
        lightIsOn = false;
        #endregion

    }
	
	void Update ()
    {
        Light();
        collectFlame();
        activateCrystal();
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
            neerCrystalLock = true;
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
    void collectFlame()
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
                FlameFollow = true;
                FlamePickedUp = true;
            }
        }
    }
    void activateCrystal()
    {
        if(crystalText.enabled == true && FlameCount == 1)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                crystalActive = true;
            }
        }
    }
}
