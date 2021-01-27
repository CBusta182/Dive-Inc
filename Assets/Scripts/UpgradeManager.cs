using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    MainScript mainScript;
    DiverManager diverManager;
    BoxManager boxManager;
    public int shinyshellsLevel;
    public int shinyshellsMaxlevel;
    public double shinyshellsCost;
    public Text shinyshellsLvlText;
    public Text shinyshellsPriceText;
    public Text shinyshellsEffectText; 
    public int moreshellstorageLevel;
    public int moreshellstorageMaxLevel;
    public double moreshellstorageCost;
    public Text moreshellstorageLvlText;
    public Text moreshellstoragePriceText;
    public Text moreshellstorageEffectText; 
    public int clickpowerLevel;
    public int clickpowerMaxLevel;
    public double clickpowerCost;
    public Text clickpowerLvlText;
    public Text clickpowerPriceText;
    public Text clickpowerEffectText; 
    public int flipperLevel;
    public int flipperMaxLevel;
    public double flipperCost;
    public Text flipperLvlText;
    public Text flipperPriceText;
    public Text flipperEffectText; 
    private void Awake()
    {
        mainScript = GameObject.FindObjectOfType<MainScript>();
        diverManager = GameObject.FindObjectOfType<DiverManager>();
        boxManager = GameObject.FindObjectOfType<BoxManager>();
    }
    private void Start()
    {
    }

    void buyUpgrade(ref int level, int maxLevel, Text levelTxt, ref double cost, double costScale, ref double affectedValue,
                           double UpGScale, Text PriceTxt)
    {
        if (level >= maxLevel)
        {
            level = maxLevel;
        }
        else if (level < maxLevel)
        {
            mainScript.coins -= cost;
            affectedValue *= UpGScale;
            cost *= costScale;
            PriceTxt.text = "Research\n" + cost.ToString("F2");
            level++;
            levelTxt.text = level + "x";
        }
    }
    void buyUpgradeDecrement(ref int level, int maxLevel, Text levelTxt, ref double cost, double costScale, ref float affectedValue,
                       float UpGScale, Text PriceTxt)
    {
        if (level >= maxLevel)
        {
            level = maxLevel;
        }
        else if (level < maxLevel)
        {
            mainScript.coins -= cost;
            affectedValue -=(float) affectedValue*UpGScale;
            cost *= costScale;
            PriceTxt.text = "Research\n" + cost.ToString("F2");
            level++;
            levelTxt.text = level + "x";
        }
    }
    void buyUpgradeIncrement(ref int level, int maxLevel, Text levelTxt,  ref double cost, double costScale, ref double affectedValue,
                       double UpGScale, Text PriceTxt)
    {
        if (level >= maxLevel)
        {
            level = maxLevel;
        }
        else if (level < maxLevel)
        {
            mainScript.coins -= cost;
            affectedValue += UpGScale;
            cost *= costScale;
            PriceTxt.text = "Research\n" + cost.ToString("F2");
            level++;
            levelTxt.text = level + "x";
        }
    }
    void updateEffectText(int level, int maxLevel, Text effectText, string baseEffectString, string effectString, int scale, string present)
    {
        if(level == 0)
        {
            effectText.text = baseEffectString; 
        }
        else if(level < maxLevel)
        {
            effectText.text = effectString + level * scale + present; 
        }
    }
    public void buyShinyShell()
    {
        if(mainScript.coins >= shinyshellsCost)
        {
            buyUpgrade(ref shinyshellsLevel, shinyshellsMaxlevel, shinyshellsLvlText, ref shinyshellsCost, 1.05, ref mainScript.shellPrice, 1.25, shinyshellsPriceText);
        }
        updateEffectText(shinyshellsLevel, shinyshellsMaxlevel, shinyshellsEffectText, "Increase Shell Value by 10%", "Increase Shell Value by ", 10, "%");
    }
    public void buyMoreStorage()
    {
        if (mainScript.coins >= moreshellstorageCost)
        {
            buyUpgradeIncrement(ref moreshellstorageLevel, moreshellstorageMaxLevel, moreshellstorageLvlText, ref moreshellstorageCost, 1.02, ref boxManager.shellBoxCap, 20, moreshellstoragePriceText);
        }
        updateEffectText(moreshellstorageLevel, moreshellstorageMaxLevel, moreshellstorageEffectText, "Increase Box Capacity by 20", "Increase Box Capacity by ", 20, "");
    }
    public void buyClickPower()
    {
        if(mainScript.coins >= clickpowerCost)
        {
            buyUpgradeIncrement(ref clickpowerLevel, clickpowerMaxLevel, clickpowerLvlText, ref clickpowerCost, 1.09, ref mainScript.clickPower, 1, clickpowerPriceText);
        }
        updateEffectText(clickpowerLevel, clickpowerMaxLevel, clickpowerEffectText, "Shells Per Click: +1", "Shells Per Click: +", 1, "");
    }
    public void buyFlippers()
    {
        if (mainScript.coins >= flipperCost)
        {
            buyUpgradeDecrement(ref flipperLevel, flipperMaxLevel, flipperLvlText, ref flipperCost, 1.12, ref diverManager.basicDiverSpeed, (float)0.05, flipperPriceText);
        }
        updateEffectText(flipperLevel, flipperMaxLevel, flipperEffectText, "Increase diver's speed by 5%", "Increase diver's speed by ", 5, "%");
    }
}
