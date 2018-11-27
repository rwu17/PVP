using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_UI : NetworkBehaviour {

    public GameObject Canvas;

    public GameObject Player;
    public GameObject Target;

    [SyncVar] private string playerUniqueName;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public string playerPublicName; 

    public GameObject PlayerFrame;    
    public Image PlayerHealth;    
    [SyncVar(hook = "UpdateMaxHealth")]public float maxHealth;
    [SyncVar(hook = "UpdateHealth")]public float currentHealth;
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
        maxHealth = 50;//Initiate value
        currentHealth = maxHealth; //Initiate value
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
            {
                TargetSelect();
                
                if (Target != null)
                {
                    //TargetHealthUpdate(Target);
                    CmdTargetSelect(Target);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Target = null;
                TargetFrame.gameObject.SetActive(false);
            }
                
            if (Target != null)
            {
                TargetFrame.gameObject.SetActive(true);
                //TargetHealthUpdate(Target);
                CmdTargetSelect(Target);
            }
            else
            {
                TargetFrame.gameObject.SetActive(false);
                TargetCurrentHealth = 0;
                TargetMaxHealth = 0;
                TargetHealthFill = 0;
            }
                
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

    void SetHealth()
    {
        if (isLocalPlayer)
        {
            healthFill = currentHealth / maxHealth;
            PlayerHealth.fillAmount = healthFill;
        }
    }

    public void HealthDamage(float amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        HealthValidate();
        SetHealth();
    }

    void UpdateHealth(float health)
    {
        currentHealth = health;
        SetHealth();
    }

    void UpdateMaxHealth(float health)
    {
        maxHealth = health;
        /*
        HealthValidate();
        SetHealth();
        */
    }

    void HealthValidate()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("The target is dead!");
        }
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
                TargetHealthUpdate(Target);
                //CmdTellServerTargetName(target);
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
        TargetHealthFill = 0;
        TargetCurrentHealth = selectedTarget.GetComponent<Player_UI>().currentHealth;
        TargetMaxHealth = selectedTarget.GetComponent<Player_UI>().maxHealth;
        TargetHealthFill = TargetCurrentHealth / TargetMaxHealth;
        TargetHealth.fillAmount = TargetHealthFill;
    }

    [Command]
    void CmdTargetSelect(GameObject selectTarget)
    {
        selectTarget.GetComponent<Player_UI>().TargetHealthUpdate(selectTarget);
        //TargetHealthUpdate(selectTarget);
    }

    [Command]
    void CmdServerDamage(GameObject damageTarget, float damage)
    {
        //GameObject selectedTarget = GameObject.Find(target);
        //selectedTarget.GetComponent<Player_Health>().OnChangeValue(damage);
        damageTarget.GetComponent<Player_UI>().HealthDamage(damage);
    }

}
