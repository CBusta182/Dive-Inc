using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class offlineManager : MonoBehaviour
{
    MainScript mainScript;
    public Text timeAwayTxt;
    public Text amountEarnedTxt;
    public GameObject offlinePopUp;

    private void Awake()
    {
        mainScript = GameObject.FindObjectOfType<MainScript>();
    }
    public void loadofflineProduction()
    {
        if (mainScript.offlineProgcheck)
        {
            var tempOfflineTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = DateTime.Now; 

            var TimeSpan  = currentTime.Subtract(oldTime);
            float rawTime = (float)TimeSpan.TotalSeconds;
            float offlineTime  = rawTime / 10;

            offlinePopUp.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayTxt.text = $"You were away for\n<color=#00FFFF>{timer:dd\\:hh\\:mm\\:ss}</color>";

            double coinsGained = mainScript.coinsPerSec * offlineTime;
            amountEarnedTxt.text = $"You earned:\n<color=red>{coinsGained}</color> Coins";
            mainScript.coins += coinsGained;
        }
    }

    public void closePopUp()
    {
        offlinePopUp.SetActive(false);
    }
}
