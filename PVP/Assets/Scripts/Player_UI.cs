using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_UI : NetworkBehaviour {

    public GameObject Player;
    public GameObject Target;

    [SyncVar] private string playerUniqueName;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public string playerPublicName;

    public GameObject Canvas;

    public GameObject PlayerFrame;    
    public Image PlayerHealth;    
    public float maxHealth;
    [SyncVar(hook = "UpdateHealth")]
    public float currentHealth = 25;
    public float healthFill;

    public GameObject TargetFrame;
    public Image TargetHealth;
    public float TargetMaxHealth;
    public float TargetCurrentHealth;    
    public float TargetHealthFill;

    public Texture2D cursorMain;
    public Texture2D cursorAttack;
    public Texture2D cursorEnemy;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;    

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
        playerPublicName = playerUniqueName;
        Canvas.gameObject.SetActive(true);
        TargetFrame.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        currentHealth = 100; //Initiate value
        maxHealth = 50;//Initiate value
        SetHealth();
    }

    private void Update()
    {
        if (myTransform.name == "" || myTransform.name == "Player(Clone)")
            SetIdentity();

        if (isLocalPlayer)
        {
            changeCursor();

            if (Input.GetMouseButtonDown(0))
                TargetSelect();

            if (Input.GetKeyDown(KeyCode.Escape))
                Target = null;

            if (Target != null)
                TargetHealthUpdate(Target);

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (Target != null)
                {
                    CmdServerDamage(Target, 10);
                }
                else
                {
                    print("You need to select a target!");
                }
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                if (Target != null)
                {
                    CmdServerDamage(Target, -10);
                }
                else
                {
                    print("You need to select a target!");
                }
            }
        }
    }

    void HealthValidate()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } else if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("The target is dead!");
        }
    }

    void SetHealth()
    {
        if (isLocalPlayer)
        {
            HealthValidate();
            healthFill = currentHealth / maxHealth;
            PlayerHealth.fillAmount = healthFill;
        }
    }

    public void HealthDamage(float amount)
    {
        currentHealth -= amount;
        HealthValidate();
    }

    void UpdateHealth(float health)
    {
        currentHealth = health;
        HealthValidate();
        SetHealth();
    }

    public string GetPlayerUniqueName()
    {
        return playerUniqueName;
    }

    void Awake()
    {
        myTransform = transform;
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string uniqueName = "Player" + playerNetID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueName = name;
    }

    void TargetSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.transform.name == playerUniqueName)
            {
                print("You can't select yourself");
                Target = null;
            }
            else if (hit.transform.tag == "Player")
            {
                Target = hit.transform.gameObject;
                TargetFrame.gameObject.SetActive(true);
                //CmdTellServerTargetName(target);
                TargetHealthUpdate(Target);
            }
            else
            {
                Target = null;
                TargetFrame.gameObject.SetActive(false);
            }
        }
    }

    void changeCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.transform.name == playerUniqueName)
            {
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
            }
            else if (hit.transform.tag == "Player")
            {
                Cursor.SetCursor(cursorEnemy, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
            }
        }
    }

    void TargetHealthUpdate(GameObject selectedTarget)
    {
        TargetCurrentHealth = selectedTarget.GetComponent<Player_UI>().currentHealth;
        TargetMaxHealth = selectedTarget.GetComponent<Player_UI>().maxHealth;
        TargetHealthFill = TargetCurrentHealth / TargetMaxHealth;
        TargetHealth.fillAmount = TargetHealthFill;
    }

    [Command]
    void CmdServerDamage(GameObject damageTarget, float damage)
    {
        //GameObject selectedTarget = GameObject.Find(target);
        //selectedTarget.GetComponent<Player_Health>().OnChangeValue(damage);
        damageTarget.GetComponent<Player_UI>().HealthDamage(damage);
    }

}
