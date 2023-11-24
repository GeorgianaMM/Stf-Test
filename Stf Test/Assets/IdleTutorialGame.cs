using UnityEngine;
using UnityEngine.UI;
public class IdleTutorialGame : MonoBehaviour
{
    public Text coinsText;
    public double coins;

    void Start()
    {
        coins = 0;
    }

    
    void Update()
    {
        coinsText.text = "Coins " + coins;
    }

    public void Click()
    {
        coins +=1;
    }
}
