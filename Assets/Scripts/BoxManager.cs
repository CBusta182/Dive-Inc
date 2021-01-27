using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BoxManager : MonoBehaviour
{
    private MainScript mainScript; 
    public Text shellboxText;
    public double shellBoxCap;
    void Awake()
    {
        mainScript = GameObject.FindObjectOfType<MainScript>();
    }
    public Boxes shellBox = new Boxes(); 
    void Start()
    {
        shellBox.Capacity = shellBoxCap;
        shellBoxCap = 500;
        shellBox.Content = mainScript.shells; 
    }
    public void sellBoxContent()
    {
        mainScript.coins += mainScript.shells * mainScript.shellPrice;
        mainScript.shells = 0;
        updateBoxShellText();
    }
    public void updateBoxShellText()
    {
        shellboxText.text = mainScript.shells + "/" + shellBox.Capacity;
    }
    public void checkBoxCap()
    {
        shellBox.Capacity = shellBoxCap;
        if (mainScript.shells >= shellBox.Capacity)
        {
            mainScript.shells = shellBox.Capacity;
        }
    }
    public class Boxes
    {
        public double Capacity { get; set; }
        public double Content { get; set; }
    }
}
