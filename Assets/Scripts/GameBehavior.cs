using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public int maxHP = 10;

    private int _maxShield = 0;
    private int _currentShield = 0;
    private float _shieldTimer = 20f;
    private int _shieldTimerDefault = 20;

    private bool _fireRateBuff = false;
    private int _fireRateBuff_Time = 20;

    private int _enemiesDefeated = 0;

    public Button WinButton;
    public int MaxItems = 4;
    public int MaxEnemies = 10;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public TMP_Text FireRateText;
    public TMP_Text ShieldText;
    public TMP_Text EnemyText;
    public Button LossButton;

    private string _state;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Level_Road");
            Time.timeScale = 1.0f;
        }
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items Collected: " + Items;
            /*if(_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You've Found all the items! (PlaceHolder task)";
                WinButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more! (Placeholder task)";
            }*/
        }
    }


    public int EnemiesDefeated
    {
        get { return _enemiesDefeated; }
        set
        {
            _enemiesDefeated = value;
            EnemyText.text = "Enemies Defeated: " + _enemiesDefeated;
            if (_enemiesDefeated >= MaxEnemies)
            {
                UpdateScene("You've killed " + MaxEnemies + " Enemies!");
                WinButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = (MaxEnemies - _enemiesDefeated) + " Enemies Left.";
            }
        }
    }

    public int FR_Buff_Time
    {
        get { return _fireRateBuff_Time; }
        set { _fireRateBuff_Time = value; }
    }

    public bool FireRate
    {
        get { return _fireRateBuff; }
        set
        {
            _fireRateBuff = value;
            if (_fireRateBuff == true)
            {
                FireRateText.text = "FireRate Buffed: Yes";
            }
            else
            {
                FireRateText.text = "FireRate Buffed: No";
            }
        }
    }
    public float ShieldTimer
    {
        get { return _shieldTimer; }
        set
        { 
            _shieldTimer = value;
            if (_currentShield > _maxShield)
            {
                _currentShield = _maxShield;
            }
            if (_currentShield < 1)
            {
                ShieldText.text = "Shield Recharging: " + (int)_shieldTimer;
            }
            else
            {
                ShieldText.text = "Shield: " + _currentShield;
            }
        }
    }

    public int ShieldTimerDefault
    {
        get { return _shieldTimerDefault; }
        set
        { _shieldTimerDefault = value; }
    }
    public int MaxShield
    {
        get { return _maxShield; }
        set { _maxShield = value; }
    }
    public int Shield
    {
        get { return _currentShield; }
        set
        {
            _currentShield = value;
            if(_currentShield > _maxShield)
            {
                _currentShield = _maxShield;
            }
            if(_currentShield < 1)
            {
                ShieldText.text = "Shield Recharging: " + _shieldTimer;
            }
            else
            {
                ShieldText.text = "Shield: " + _currentShield;
            }
        }
    }
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if(_playerHP > maxHP)
            {
                _playerHP = maxHP;
                Debug.Log("Health exceeded max, reverted health to maxHP.");
            }
            Debug.LogFormat("Lives: {0}", _playerHP);

            if (HP > 0)
            {
                HealthText.text = "Player Health: " + HP;
                Debug.LogFormat("Lives:{0}", _playerHP);
            }
            else
            {
                HealthText.text = "Health: You suck.";
                UpdateScene("--You blew it--");
                LossButton.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void RestartScene()
    {
        Utilities.RestartLevel(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
        Initialize();
    }

    public void Initialize()
    {
        _state = "Game Managager Initialized...";
        _state.FancyDebug();
        Debug.Log(_state);
    }
    // Update is called once per frame
}
