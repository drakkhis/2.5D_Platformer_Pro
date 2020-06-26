using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text _coinsText;
    private int _coins;
    private int _lives;
    private Text _livesText;

    // Start is called before the first frame update
    void Start()
    {
        _coinsText = this.transform.Find("CoinText").GetComponent<Text>();
        if (_coinsText == null)
        {
            Debug.LogError("No Coin Text");
        }
        _livesText = this.transform.Find("LivesText").GetComponent<Text>();
        if (_coinsText == null)
        {
            Debug.LogError("No Lives Text");
        }
        _coinsText.text = "Coins: " + _coins;
        _livesText.text = "Lives: " + _lives;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCoinText(int c)
    {
        _coins = c;
        _coinsText.text = "Coins: " + _coins;
    }
    public void SetLivesText(int l)
    {
        _lives = l;
        _livesText.text = "Lives: " + _lives;
    }
}

