using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainScript : MonoBehaviour
{
    public bool offlineProgcheck;
    UpgradeManager upgradeManager;
    DiverManager diverManager;
    BoxManager boxManager;
    offlineManager offlineManager; 
    public double coins; 
    public Text coinsText;
    public double coinsPerSec;
    public Text coinsPerSecText;
    public double clickPower; 
    public double shells;
    public double shellPrice;

    public double item; 
    
    public GameObject ResearchPanel;
    private void Awake()
    {
        upgradeManager = GameObject.FindObjectOfType<UpgradeManager>();
        diverManager = GameObject.FindObjectOfType<DiverManager>();
        boxManager = GameObject.FindObjectOfType<BoxManager>();
        offlineManager = GameObject.FindObjectOfType<offlineManager>();
    }
    void Start()
    {
        Application.targetFrameRate = 30;
        Load();
        offlineManager.loadofflineProduction();
    }

    void Update()
    {
        coinsText.text = notationMethod(coins, "F0");
        boxManager.checkBoxCap();
        boxManager.updateBoxShellText();
        Save();
        diverManager.basicDiversDive();
        loadText();
    }

    public float saveTime; 
    void Save()
    {
        saveTime += Time.deltaTime; 
        if(saveTime >= 2)
        {
            PlayerPrefs.SetString("coins", coins.ToString());
            PlayerPrefs.SetString("coinsPerSec", coinsPerSec.ToString());
            PlayerPrefs.SetString("clickPower", clickPower.ToString());
            PlayerPrefs.SetString("shellPrice", shellPrice.ToString());

            PlayerPrefs.SetInt("shinyshellsLevel", upgradeManager.shinyshellsLevel);
            PlayerPrefs.SetInt("shinyshellsMaxLevel", upgradeManager.shinyshellsMaxlevel);
            PlayerPrefs.SetString("shinyshellsCost", upgradeManager.shinyshellsCost.ToString());
            PlayerPrefs.SetInt("moreshellstorageLevel", upgradeManager.moreshellstorageLevel);
            PlayerPrefs.SetInt("moreshellstorageMaxLevel", upgradeManager.moreshellstorageMaxLevel);
            PlayerPrefs.SetString("moreshellstorageCost", upgradeManager.moreshellstorageCost.ToString());
            PlayerPrefs.SetInt("clickpowerLevel", upgradeManager.clickpowerLevel);
            PlayerPrefs.SetInt("clickpowerMaxLevel", upgradeManager.clickpowerMaxLevel);
            PlayerPrefs.SetString("clickpowerCost", upgradeManager.clickpowerCost.ToString());
            PlayerPrefs.SetInt("flipperLevel", upgradeManager.flipperLevel);
            PlayerPrefs.SetInt("flipperMaxLevel", upgradeManager.flipperMaxLevel);
            PlayerPrefs.SetString("flipperCost", upgradeManager.flipperCost.ToString());

            PlayerPrefs.SetInt("basicDivers.amount", diverManager.basicDivers.amount);
            PlayerPrefs.SetInt("basicDivers.maxAmount", diverManager.basicDivers.maxAmount);
            PlayerPrefs.SetString("basicDivers.maintenance", diverManager.basicDivers.maintenance.ToString());
            PlayerPrefs.SetString("basicDivers.cost", diverManager.basicDivers.cost.ToString());
            PlayerPrefs.SetFloat("basicDiverSpeed", diverManager.basicDiverSpeed);
            PlayerPrefs.SetString("basicDivers.itemsPerDive", diverManager.basicDivers.itemsPerDive.ToString());

            PlayerPrefs.SetString("shellbox.Capacity", boxManager.shellBox.Capacity.ToString());
            PlayerPrefs.SetString("shellBox.Content", boxManager.shellBox.Content.ToString());
           
            saveTime = 0; 
        }
        PlayerPrefs.SetString("OfflineTime", DateTime.Now.ToBinary().ToString());
        offlineProgcheck = true;
    }
    void Load()
    {
        coins = double.Parse(PlayerPrefs.GetString("coins", "0"));
        coinsPerSec = double.Parse(PlayerPrefs.GetString("coinsPerSec", "0"));
        clickPower = double.Parse(PlayerPrefs.GetString("clickPower", "1"));
        shellPrice = double.Parse(PlayerPrefs.GetString("shellPrice", "0.25"));
        
        upgradeManager.shinyshellsLevel = PlayerPrefs.GetInt("shinyshellsLevel", 0);
        upgradeManager.shinyshellsMaxlevel = PlayerPrefs.GetInt("shinyshellsMaxLevel", 50);
        upgradeManager.shinyshellsCost = double.Parse(PlayerPrefs.GetString("shinyshellsCost", "2"));
        upgradeManager.moreshellstorageLevel = PlayerPrefs.GetInt("moreshellstorageLevel", 0);
        upgradeManager.moreshellstorageMaxLevel = PlayerPrefs.GetInt("moreshellstorageMaxLevel", 25);
        upgradeManager.moreshellstorageCost = double.Parse(PlayerPrefs.GetString("moreshellstorageCost", "8"));
        upgradeManager.clickpowerLevel = PlayerPrefs.GetInt("clickpowerLevel", 0);
        upgradeManager.clickpowerMaxLevel = PlayerPrefs.GetInt("clickpowerMaxLevel", 10);
        upgradeManager.clickpowerCost = double.Parse(PlayerPrefs.GetString("clickpowerCost", "20"));
        upgradeManager.flipperLevel = PlayerPrefs.GetInt("flipperLevel", 0);
        upgradeManager.flipperMaxLevel = PlayerPrefs.GetInt("flipperMaxLevel", 10);
        upgradeManager.flipperCost = double.Parse(PlayerPrefs.GetString("flipperCost", "30"));
        
        diverManager.basicDivers.amount = PlayerPrefs.GetInt("basicDivers.amount", 1);
        diverManager.basicDivers.maxAmount = PlayerPrefs.GetInt("basicDivers.maxAmount", 15);
        diverManager.basicDivers.maintenance = double.Parse(PlayerPrefs.GetString("basicDivers.maintenance", "20"));
        diverManager.basicDivers.cost = double.Parse(PlayerPrefs.GetString("basicDivers.cost", "60"));
        diverManager.basicDiverSpeed = PlayerPrefs.GetFloat("basicDiverSpeed", 8);
        diverManager.basicDivers.itemsPerDive = int.Parse(PlayerPrefs.GetString("basicDivers.itemsPerDive", "1"));

        boxManager.shellBox.Capacity = double.Parse(PlayerPrefs.GetString("shellBox.Capacity", "300"));
        boxManager.shellBox.Content = double.Parse(PlayerPrefs.GetString("shellBox.Content", "0"));
        loadText();
    }
    public void loadText()
    {
        upgradeManager.shinyshellsLvlText.text = upgradeManager.shinyshellsLevel + "x";
        upgradeManager.shinyshellsPriceText.text = "Research\n" + upgradeManager.shinyshellsCost.ToString("F2");
        upgradeManager.shinyshellsEffectText.text = "Increase Shell value by " + upgradeManager.shinyshellsLevel * 10 + "%";
        upgradeManager.moreshellstorageLvlText.text = upgradeManager.moreshellstorageLevel + "x";
        upgradeManager.moreshellstoragePriceText.text = "Research\n" + upgradeManager.moreshellstorageCost.ToString("F2");
        upgradeManager.moreshellstorageEffectText.text = "Increase Box Capacity by " + upgradeManager.moreshellstorageLevel * 20;
        upgradeManager.clickpowerLvlText.text = upgradeManager.clickpowerLevel + "x";
        upgradeManager.clickpowerPriceText.text = "Research\n" + upgradeManager.clickpowerCost.ToString("F2");
        upgradeManager.clickpowerEffectText.text = "Shells Per Click: +" + upgradeManager.clickpowerLevel;
        upgradeManager.flipperLvlText.text = upgradeManager.flipperLevel + "x"; 
        upgradeManager.flipperPriceText.text = "Research\n" + upgradeManager.flipperCost.ToString("F2");
        upgradeManager.flipperEffectText.text = "Increase diver's speed by " + upgradeManager.flipperLevel * 5 + "%";

        boxManager.shellboxText.text = shells + "/" + boxManager.shellBox.Capacity;
    }
    public void openPanel(GameObject a)
    {
        if( a != null)
        {
            bool isActive = a.activeSelf;
            a.SetActive(!isActive);
        }
    }
    public string notationMethod(double x, string y)
    {
        if (x > 1000)
        {
            var expoenent = Math.Floor(Math.Log10(Math.Abs(x)));
            var mantissa = x / Math.Pow(10, expoenent);
            return mantissa.ToString(format: "F2")+ "e" + expoenent;
        }
        return x.ToString(y);
    }
    public void openResearch()
    {

        openPanel(ResearchPanel);
    }
    public void addItem()
    {
        item = shells;
        item += clickPower;
        shells = item; 
    }
    public void fullReset()
    {
        offlineProgcheck = false; 
        coins = 0;
        coinsPerSec = 0;
        shells = 0;
        shellPrice = 0.25;
        clickPower = 1;
        upgradeManager.shinyshellsCost = 2;
        upgradeManager.shinyshellsLevel = 0;
        upgradeManager.moreshellstorageCost = 8;
        upgradeManager.moreshellstorageLevel = 0;
        upgradeManager.clickpowerCost = 20;
        upgradeManager.clickpowerLevel = 0;
        upgradeManager.flipperCost = 30;
        upgradeManager.flipperLevel = 0;

        diverManager.basicDivers.amount = 1;
        diverManager.basicDiverSpeed = 8; 
        diverManager.basicDivers.speed = diverManager.basicDiverSpeed;
        diverManager.basicDivers.cost = 60;
        diverManager.basicDivers.maintenance = 20;
        diverManager.basicDivers.itemsPerDive = 1;

        boxManager.shellBoxCap = 500;
        boxManager.shellBox.Capacity = boxManager.shellBoxCap;
        boxManager.shellBox.Content = 0;
    }
}