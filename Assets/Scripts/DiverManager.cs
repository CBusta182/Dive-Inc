using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class DiverManager : MonoBehaviour
{
    public Divers basicDivers = new Divers(); 
    MainScript mainScript;
    BoxManager boxManager;
    public int shellsPerDive;
    public double diveTime;
    public Text basicDiversText;
    public float basicDiverSpeed;

    private void Awake()
    {
        mainScript = GameObject.FindObjectOfType<MainScript>();
        boxManager = GameObject.FindObjectOfType<BoxManager>();
    }
    //start function only used for testing 
    void Start()
    {
        basicDivers.amount = 1;
        basicDivers.maxAmount = 15;
        basicDivers.maintenance = 20; 
        basicDivers.cost = 60;
        basicDiverSpeed = 8;
        basicDivers.speed = basicDiverSpeed;
        basicDivers.itemsPerDive = 1;
    }
    public List<GameObject> basicDiversObjects = new List<GameObject>();
    public void buybasicDiver()
    {
        if (mainScript.coins >= basicDivers.cost)
        {
            int tempAmount = basicDivers.amount;
            buyDiver(tempAmount, basicDivers.maxAmount, basicDivers.cost, basicDiversText);
            int index = Random.Range(0, basicDiversObjects.Count);
            GameObject BDspawn= Instantiate(basicDiversObjects[index], new Vector2(-200, -200), transform.rotation) as GameObject;
            BDspawn.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        }
    }
    void buyDiver(int amount, int maxAmount, double cost, Text diverText)
    {
        if(amount >= maxAmount)
        {
            amount = maxAmount;
        }
        else if(amount < maxAmount)
        {
            amount++;
            mainScript.coins -= cost;
            diverText.text = amount + " Divers";
        }
    }
    public void basicDiversDive()
    { 
        basicDivers.speed = basicDiverSpeed;
        diveTime += Time.deltaTime;
        if (diveTime >= basicDivers.speed)
        {
           boxManager.checkBoxCap();
            if (mainScript.shells < boxManager.shellBox.Capacity)
            {
                mainScript.shells += basicDivers.amount * basicDivers.itemsPerDive;
            }
            diveTime = 0;
        }
    }

    public int randID()
    {
        System.Random randID = new System.Random();
        return randID.Next(1, 4);
    }
}

