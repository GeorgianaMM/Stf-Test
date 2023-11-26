using UnityEngine;
using UnityEngine.UI;
public class IdleTutorialGame : MonoBehaviour
{
    public Text coinsText;
    public double coins;


    public Text coinsPerSecText;
    public Text clickUpgrade1Text;
    public Text productionUpgrade1Text;


    public double coinsPerSecond;
    public double clickUpgrade1Cost; //Cost of upgrade power-up
    public int clickUpgrade1Level;   //Level of upgrade power-up
    public double clickUpgrade1Power; //Power of upgrade power-up


    public double productionUpgrade1Cost; //Cost of production power-up
    public int productionUpgrade1Level; ////Level of production power-up
    //public double productionUpgrade1Power; //Power of production power-up


    void Start()
    {
        coins = 0;
        clickUpgrade1Cost = 10;
        productionUpgrade1Cost = 25;

    }
    

    void Update()
    {
        coinsPerSecond = productionUpgrade1Level;

        coinsText.text = "Coins " + coins;
        coinsPerSecText.text = coinsPerSecond + " coins/s";
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1Cost + " coins\nPower: +1 Click\nLevel: " + clickUpgrade1Level;
        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1Cost + " coins\nPower: +1 coins/s\nLevel: " + productionUpgrade1Level;
    }


    //Buttons
    public void Click()
    {
        coins +=1;
    }

    
}
