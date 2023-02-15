using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager;
    public int ItemID = 999;
    // Start is called before the first frame update

    void getID()
    {
        switch (name)
        {
            case "Health_Pickup":
                ItemID = 0;
                break;
            case "Shield_Pickup":
                ItemID = 1;
                break;
            case "Firerate_Pickup":
                ItemID = 2;
                break;
            default:
                ItemID = 999;
                break;
        }
    }

    void activateItem()
    {
        switch (ItemID)
        {
            case 0:
                GameManager.HP += 3;
                break;
            case 1:
                GameManager.MaxShield += 1;
                GameManager.Shield += 1;
                break;
            case 2:
                GameManager.FireRate |= true;
                break;
            default:
                break;
        }
    }
    void Start()
    {
        GameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        getID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            activateItem();
            Destroy(this.transform.gameObject);
            Debug.Log("Item collected! Item = " + name);
            GameManager.Items += 1;
        }
    }
}
