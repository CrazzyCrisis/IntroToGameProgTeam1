using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ShopScript : MonoBehaviour
{
    public int cookiesPerClick = 1; // Cookies given per click
    public float currentCookies; // Current amount of cookies the player has
    public float cookiesPerSecond; // Cookies given per second

    public int currentBatter; // Current amount of batter that the player has
    public int maxBatter; // Max amount of batter the player can hold at once
    public int batterRate; // How much batter per second the player gets

    public int chocolateBuff = 0; // While this number is over 0, increase cookies per second by 50%
    public int chunkBuff = 0; // While this number is over 0, increase cookies per click by 100%
    public int oatmealBuff = 0; // While this number is over 0, get 25% cashback on all purchaces

    public List<string> boostedBuffs; // If a buff is on this list, it is boosted by White Chocolate Cookie

    float timer = 0;

    public int extraHandLevel = 0; // Level for Extra Hand Upgrades
    public float extraHandCost = 10; // Cost for next Extra Hand Level
    public int powerGloveLevel = 0; // Level for Power Glove Upgrades
    public float powerGloveCost = 10; // Cost for next Power Glove Level
    public int mixerLevel = 0; // Level for Improved Mixer Upgrades
    public float mixerCost = 10; // Cost for next Improved Mixer Level
    public int storageLevel = 0; // Level for Improved Storage Upgrades
    public float storageCost = 10; // Cost for next Improved Storage Level
    public int flavorLevel = 0; // Level for Artifical Flavors Upgrades
    public float flavorCost = 10; // Cost for next Artificial Flavors Level
    public int unlocksLevel = 0; // Level for Unclocks Upgrades
    public float unlocksCost = 10; // Cost for next Unlocks Level

    public string currentSkin = "standard"; // Current skin being used

    public GameObject upgradeScreen;
    public GameObject bakeryScreen;
    public GameObject skinsScreen;

    public GameObject chocolatePopup;
    public GameObject chunkPopup;
    public GameObject oatmealPopup;

    public TextMeshProUGUI CPCText; // Cookies Per Click text
    public TextMeshProUGUI CCText; // Current Cookies text
    public TextMeshProUGUI CPSText; // Cookies Per Second text
    public TextMeshProUGUI batterText; // Displays current amount of batter compared to max amount of batter

    // Extra Hand Text
    public TextMeshProUGUI EHLevel;
    public TextMeshProUGUI EHCost;
    public TextMeshProUGUI EHStats;

    // Power Glove Text
    public TextMeshProUGUI PGLevel;
    public TextMeshProUGUI PGCost;
    public TextMeshProUGUI PGStats;

    // Unlocks Text
    public TextMeshProUGUI ULevel;
    public TextMeshProUGUI UCost;
    public TextMeshProUGUI UDescription;

    // Artificial Flavors Text
    public TextMeshProUGUI AFLevel;
    public TextMeshProUGUI AFCost;
    public TextMeshProUGUI AFStats;

    // Improved Mixer Text
    public TextMeshProUGUI IMLevel;
    public TextMeshProUGUI IMCost;
    public TextMeshProUGUI IMStats;

    // Improved Storage Text
    public TextMeshProUGUI ISLevel;
    public TextMeshProUGUI ISCost;
    public TextMeshProUGUI ISStats;

    public void purchase (string upgrade)
    {
        if (upgrade == "extraHand")
        {
            if (currentCookies >= extraHandCost)
            {
                cookiesPerSecond += 1;
                currentCookies -= extraHandCost;
                extraHandLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(extraHandCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(extraHandCost * 0.25f);
                    }
                }
                extraHandCost = Mathf.FloorToInt(extraHandCost * 1.5f);
            }
            
        }
        else if (upgrade == "powerGlove")
        {
            if (currentCookies >= powerGloveCost)
            {
                cookiesPerClick += 1;
                currentCookies -= powerGloveCost;
                powerGloveLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(powerGloveCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(powerGloveCost * 0.25f);
                    }
                }
                powerGloveCost = Mathf.FloorToInt(powerGloveCost * 1.4f);
            }

        }
        else if (upgrade == "unlocks")
        {
            if (currentCookies >= unlocksCost)
            {
                currentCookies -= unlocksCost;
                unlocksLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(unlocksCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(unlocksCost * 0.25f);
                    }
                }
                unlocksCost = Mathf.FloorToInt(unlocksCost * 1.5f);
            }

        }
        else if (upgrade == "flavors")
        {
            if (currentCookies >= flavorCost)
            {
                currentCookies -= flavorCost;
                flavorLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(flavorCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(flavorCost * 0.25f);
                    }
                }
                flavorCost = Mathf.FloorToInt(flavorCost * 1.5f);
            }

        }
        else if (upgrade == "mixer")
        {
            if (currentCookies >= mixerCost)
            {
                batterRate += 1;
                currentCookies -= mixerCost;
                mixerLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(mixerCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(mixerCost * 0.25f);
                    }
                }
                mixerCost = Mathf.FloorToInt(mixerCost * 1.5f);
            }

        }
        else if (upgrade == "storage")
        {
            if (currentCookies >= storageCost)
            {
                maxBatter += 10;
                currentCookies -= storageCost;
                storageLevel += 1;
                if (oatmealBuff > 0)
                {
                    if (boostedBuffs.Contains("oatmeal"))
                    {
                        currentCookies += Mathf.FloorToInt(storageCost * 0.5f);
                    }
                    else
                    {
                        currentCookies += Mathf.FloorToInt(storageCost * 0.25f);
                    }
                }
                storageCost = Mathf.FloorToInt(storageCost * 1.5f);
            }

        }
    }

    public void eatCookie(string type)
    {
        if (type == "chocolate")
        {
            if (currentBatter >= 100)
            {
                chocolateBuff += Mathf.FloorToInt(30 + (30 * flavorLevel * 0.1f));
                currentBatter -= 100;
            }
        }
        else if (type == "chunk")
        {
            if (currentBatter >= 100)
            {
                chunkBuff += Mathf.FloorToInt(30 + (30 * flavorLevel * 0.1f));
                currentBatter -= 100;
            }
        }
        else if (type == "sugar")
        {
            if (currentBatter >= 100)
            {
                currentCookies += (cookiesPerSecond * 60);
                currentBatter -= 100;
            }
        }
        else if (type == "oatmeal")
        {
            if (currentBatter >= 100)
            {
                oatmealBuff += Mathf.FloorToInt(15 + (15 * flavorLevel * 0.1f));
                currentBatter -= 100;
            }
        }
        else if (type == "white")
        {
            if (currentBatter >= 1)
            {
                if (chocolateBuff > 0)
                {
                    boostedBuffs.Add("chocolate");
                }
                if (chunkBuff > 0)
                {
                    boostedBuffs.Add("chunk");
                }
                if (oatmealBuff > 0)
                {
                    boostedBuffs.Add("oatmeal");
                }
                currentBatter -= 1;
            }
        }
        else if (type == "gliss")
        {
            if (currentBatter >= 100)
            {
                // Put golden cookie spawn code here
                currentBatter -= 100;
            }
        }
    }

    public void clickCookie()
    {
        if (chunkBuff > 0)
        {
            if (boostedBuffs.Contains("chunk"))
            {
                currentCookies += cookiesPerClick * 4;
            }
            else
            {
                currentCookies += cookiesPerClick * 2;
            }
            
        }
        else
        {
            currentCookies += cookiesPerClick;
        }
    }

    public void swapScreen(string buttonType)
    {
        if (buttonType == "upgrades")
        {
            upgradeScreen.SetActive(true);
            bakeryScreen.SetActive(false);
            skinsScreen.SetActive(false);
        }
        else if (buttonType == "bakery" && unlocksLevel >= 1)
        {
            upgradeScreen.SetActive(false);
            bakeryScreen.SetActive(true);
            skinsScreen.SetActive(false);
        }
        else if (buttonType == "skins" && unlocksLevel >= 3)
        {
            upgradeScreen.SetActive(false);
            bakeryScreen.SetActive(false);
            skinsScreen.SetActive(true);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCookies = 10;
        cookiesPerSecond = 1;
        currentBatter = 100;
        maxBatter = 100;
        batterRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (chocolateBuff > 0)
            {
                if (boostedBuffs.Contains("chocolate"))
                {
                    currentCookies += Mathf.FloorToInt(cookiesPerSecond * 2f);
                    chocolateBuff -= 1;
                }
                else
                {
                    currentCookies += Mathf.FloorToInt(cookiesPerSecond * 1.5f);
                    chocolateBuff -= 1;
                }
            }
            else
            {
                currentCookies += cookiesPerSecond;
            }

            if (currentBatter < maxBatter)
            {
                currentBatter += batterRate;
                if (currentBatter > maxBatter)
                {
                    currentBatter = maxBatter;
                }
            }

            if (chunkBuff > 0)
            {
                chunkBuff -= 1;
            }

            if (oatmealBuff > 0)
            {
                oatmealBuff -= 1;
            }
            
            if (chocolateBuff == 0 && boostedBuffs.Contains("chocolate"))
            {
                boostedBuffs.Remove("chocolate");
            }
            if (chunkBuff == 0 && boostedBuffs.Contains("chunk"))
            {
                boostedBuffs.Remove("chunk");
            }
            if (oatmealBuff == 0 && boostedBuffs.Contains("oatmeal"))
            {
                boostedBuffs.Remove("oatmeal");
            }

            timer = 0;
        }

        // Stats Text
        CPCText.text = "Cookies Per Click: " + cookiesPerClick.ToString();
        CCText.text = "Current Cookies: " + currentCookies.ToString();
        CPSText.text = "Cookies Per Second: " + cookiesPerSecond.ToString();
        batterText.text = "Batter: " + currentBatter.ToString() + "/" + maxBatter.ToString();

        // Extra Hand Text
        EHLevel.text = "Extra Hands - LVL " + extraHandLevel.ToString();
        EHCost.text = "Cost: " + extraHandCost.ToString();
        EHStats.text = "Current Increase: +" + extraHandLevel.ToString();

        // Power Glove Text
        PGLevel.text = "Power Glove - LVL " + powerGloveLevel.ToString();
        PGCost.text = "Cost: " + powerGloveCost.ToString();
        PGStats.text = "Current Increase: " + powerGloveLevel.ToString();

        // Unlocks Text
        UCost.text = "Cost: " + unlocksCost.ToString();
        if (unlocksLevel == 0)
        {
            ULevel.text = "Unlocks - LVL " + unlocksLevel.ToString();
            UDescription.text = "Unlocks the bakery, where you can eat special cookies for temporary effects";
        }
        else if (unlocksLevel == 1)
        {
            ULevel.text = "Unlocks - LVL " + unlocksLevel.ToString();
            UDescription.text = "Unlocks tic-tac-toe, where you can gamble cookies to earn more";
        }
        else if (unlocksLevel == 2)
        {
            ULevel.text = "Unlocks - LVL " + unlocksLevel.ToString();
            UDescription.text = "Unlocks skins, which make your cookie look kind of cool";
        }
        else
        {
            ULevel.text = "Unlocks - LVL MAX";
            UDescription.text = "MAX LEVEL REACHED";
        }

        // Artificial Flavors Text
        AFLevel.text = "Artificial Flavors - LVL " + flavorLevel.ToString();
        AFCost.text = "Cost: " + flavorCost.ToString();
        AFStats.text = "Current Increase: +" + (flavorLevel * 10).ToString() + "%";

        // Improved Mixer Text
        IMLevel.text = "Improved Mixer - LVL " + mixerLevel.ToString();
        IMCost.text = "Cost: " + mixerCost.ToString();
        IMStats.text = "Current Increase: +" + mixerLevel.ToString();

        // Improved Storage Text
        ISLevel.text = "Improved Stoarge - LVL " + storageLevel.ToString();
        ISCost.text = "Cost: " + storageCost.ToString();
        ISStats.text = "Current Increase: " + (storageLevel * 10).ToString();

        if (chocolateBuff > 0)
        {
            chocolatePopup.SetActive(true);
        }
        else
        {
            chocolatePopup.SetActive(false);
        }

        if (chunkBuff > 0)
        {
            chunkPopup.SetActive(true);
        }
        else
        {
            chunkPopup.SetActive(false);
        }

        if (oatmealBuff > 0)
        {
            oatmealPopup.SetActive(true);
        }
        else
        {
            oatmealPopup.SetActive(false);
        }
    }




}
