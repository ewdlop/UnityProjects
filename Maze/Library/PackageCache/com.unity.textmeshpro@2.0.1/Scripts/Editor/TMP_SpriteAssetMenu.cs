  a  58          2018.3.0f2 ţ˙˙˙      ˙˙3$řĚuńě˛e+ Í=   ^          7  ˙˙˙˙         Ś ˛            Đ                 Ś                Ś                Ś #               Ś +               H 3   ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ      	        Q  j     
        H <   ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ              Q  j             Ő I   ˙˙˙˙       1  1  ˙˙˙˙    Ŕ    Ţ               H j  ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ              Q  j              P             AssetMetaData guid data[0] data[1] data[2] data[3] pathName originalName labels assetStoreRef    ˙˙TMäb&H˛ăr˘3Ĺ!>   ß          7  ˙˙˙˙         Ś ˛               E            Ţ  #             . ,             5   a            Ţ  #             . ,              r            Ţ  #      	       . ,      
       H Ť ˙˙˙˙     1  1  ˙˙˙˙   @   Ţ             Q  j            ń  J   ˙˙˙˙       1  1  ˙˙˙˙        Ţ                j  ˙˙˙˙        \     ˙˙˙˙        H r   ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ              Q  j             H w   ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ              Q  j             H    ˙˙˙˙       1  1  ˙˙˙˙   @    Ţ              Q  j             y 
              Ţ  #      !       . ,      "       Ő    ˙˙˙˙#       1  1  ˙˙˙˙$    Ŕ    Ţ      %          j  ˙˙˙˙&        H   ˙˙˙˙'       1  1  ˙˙˙˙(   @    Ţ      )        Q  j     *        y 
    +         Ţ  #      ,       . ,      -               .    @    ž ¨      /    @    Ţ  #      0       . ,      1       H ­   ˙˙˙˙2      1  1  ˙˙˙˙3   @    Ţ      4        Q  j     5        H ¸   ˙˙˙˙6      1  1  ˙˙˙˙7   @    Ţ      8        Q  j     9        H Ę   ˙˙˙˙:      1  1  ˙˙˙˙;   @    Ţ      <        Q  j     =      MonoImporter PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_ExternalObjects SourceAssetIdentifier type assembly name m_DefaultReferences executionOrder icon m_UserData m_AssetBundleName m_AssetBundleVariant s    ˙˙ŁGń×ÜZ56 :!@iÁJ*          7  ˙˙˙˙         Ś ˛                E            Ţ               .              (   a            Ţ               .               r            Ţ        	       .       
       H Ť ˙˙˙˙     1  1  ˙˙˙˙   @   Ţ             Q  j            H ę ˙˙˙˙      1  1  ˙˙˙˙   @    Ţ              Q  j             ń  =   ˙˙˙˙      1  1  ˙˙˙˙       Ţ               j  ˙˙˙˙       H   ˙˙˙˙      1  1  ˙˙˙˙   @    Ţ              Q  j             y 
            Ţ               .              y Q               Ţ               .               Ţ  X      !        H i   ˙˙˙˙"      1  1  ˙˙˙˙#   @    Ţ      $        Q  j     %        H u   ˙˙˙˙&      1  1  ˙˙˙˙'   @    Ţ      (        Q  j     )      PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_DefaultReferences m_Icon m_ExecutionOrder m_ClassName m_Namespace                @              @   P      ŕyŻ        ¨$                                                                                                                                                                                                                                                                                                                                                                                                                             Ś3 v˛ČMś,Ż4%Š   Assets/Scripts/Player.cs                                                                                                                                           Player  K$  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Encumbered,
    Poison,
    Frost,
    ElementalDamage
};

public class Player : MonoBehaviour {

    #region Singleton
    /*
    public static Player player;
    
    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
    }
    */
    #endregion

    public int maxHealth = 100;
    private int health;

    public int preBattleDamage;
    int damage;
    int damageMin;
    int damageMax;

    public int elementalDamage;

    public int preBattleArmor;
    private int armor;

    private int weight;
    private Inventory inventory;
    private DialogManager dialogManager;
    public bool isPoisoned = false;
    public bool isEncumbered = false;
    public bool isDemoralized = false;

    private int gold = 10;
    private int itemCap;


    // Four status effects: 
    // 1) encumbered, 2) poison, 3) frost, 4) elemental damage
    private int[] statusEffects = new int[4];

    public void AddStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] += statusAmount;
    }

    public void RemoveStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] -= statusAmount;
        // Set to zero to avoid going negative (resulting in a buff)
        if (statusEffects[(int)status] <= 0)
        {
            statusEffects[(int)status] = 0;
        }
    }

    public void SetStatusEffect(Status status, int statusAmount)
    {
        statusEffects[(int)status] = statusAmount;
    }

    public void RemoveStatusEffect(Status status)
    {
        statusEffects[(int)status] = 0;
    }

    public int GetStatusEffect(Status status)
    {
        return statusEffects[(int)status];
    }

    private void Awake()
    {
        health = maxHealth;
        weight = 0;
        //Debug.Log("Player health: " + health);
    }
    public void Start()
    {
        damageMin = damage - 3;
        damageMax = damage + 3;
        dialogManager = FindObjectOfType<DialogManager>();
        ApplyStatusEffects();
    }

    private void Update()
    {
        ApplyStatusEffects();
    }

    public void ApplyStatusEffects()
    {
        SetStatusEffect(Status.Encumbered, ((GetWeight() - 50) / 10));
        armor = preBattleArmor - GetStatusEffect(Status.Frost);
        damage = preBattleDamage - GetStatusEffect(Status.Encumbered);
        damageMin = damage - 3;
        damageMax = damage + 3;
        elementalDamage = GetStatusEffect(Status.ElementalDamage);
    }

    public void ApplyHealthDebuffs()
    {
        DecrementHealth(GetStatusEffect(Status.Poison));
    }

    // Attack the enemy that is set in the combat encounter in gamecontroller
    public void Attack(Enemy enemy)
    {
        // Set encumbered
        //SetStatusEffect(Status.Encumbered, ((GetWeight() - 50) % 10));
        //ApplyStatusEffects();
         
        if (enemy == null)
        {
            Debug.LogError("ERROR: there is no enemy to battle");
            return;
        }
        int damageDealt;
        if (isEncumbered)
        {
            damageDealt = Mathf.Clamp(damage + elementalDamage - enemy.armor - 5, 0, 99999);
        }
        else
        if (enemy.name == "Ghost")
        {
            damageDealt = Mathf.Clamp(elementalDamage - enemy.armor, 0, 99999);
        }
        else
        if (enemy.name == "Dragon")
        {
            damageDealt = Mathf.Clamp(damage + elementalDamage * 2 - enemy.armor, 0, 99999);
        }
        else
            damageDealt = Mathf.Clamp(damage + elementalDamage - enemy.armor, 0, 99999);
        if (damageDealt > 0)
        {
            SoundController.Play((int)SFX.Attack, 0.5f);
        }
        Dictionary<float, int> damageTable = new Dictionary<float, int>();
        damageTable.Add(0.05f, damageDealt - 3);
        damageTable.Add(0.15f, damageDealt - 2);
        damageTable.Add(0.35f, damageDealt - 1);
        damageTable.Add(0.65f, damageDealt);
        damageTable.Add(0.85f, damageDealt + 1);
        damageTable.Add(0.95f, damageDealt + 2);
        damageTable.Add(1f, damageDealt + 3);
        damageDealt = RollDamage(damageTable);
        enemy.health = Mathf.Clamp(enemy.health - damageDealt, 0, enemy.maxHealth);

        if (enemy.name == "Ancient Golem" && damageDealt > 0 && itemCap <= 5)
        {
            itemCap++;
            enemy.armor--;
            string[] sentences =
            {
                "çś Adventurerĺł's turn",
                "çś Adventurerĺł deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
                "Obtain an iron ore!"
            };
            Inventory.instance.AddItem(enemy.itemDropList[0]);
            PrintNextSentence(sentences);
        }
        else if (enemy.name == "Ancient Golem" && damageDealt > 0 && itemCap >= 5)
        {
            string[] sentences =
            {
                "çś Adventurerĺł's turn",
                "çś Adventurerĺł deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
            };
            PrintNextSentence(sentences);
        }
        else
        if (enemy.name == "Bandit" && damageDealt > 0)
        {
            string[] sentences =
            {
                "çś Adventurerĺł's turn",
                "çś Adventurerĺł deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy),
                "Obtain an gold ore!"
            };
            Inventory.instance.AddItem(enemy.itemDropList[0]);
            PrintNextSentence(sentences);
        }
        else
        {
            string[] sentences =
            {
                "çś Adventurerĺł's turn",
                "çś Adventurerĺł deals " + damageDealt + " damage.",
                CheckEnemyHealth(enemy.health, enemy)
            };
            PrintNextSentence(sentences);
        }

        ApplyHealthDebuffs();
    }
    public int RollDamage(Dictionary<float, int> damageTable)
    {
        float roll = Random.Range(0f, 1f);  
        foreach(KeyValuePair<float, int> pair in damageTable)
        {
            if(roll < pair.Key)
            {
                return Mathf.Clamp(pair.Value,0,9999);
            }
        }
        return 0;
    }

    public void PrintNextSentence(string[] sentences)
    {
        Dialog playerTurn = new Dialog("enemy turn", sentences);
        dialogManager.isInDialog = true;
        dialogManager.StartDialog(playerTurn);
        dialogManager.ContinueToNextSentence();
    }

    public string CheckEnemyHealth(int enemyHealth, Enemy enemy)
    {
        if (enemyHealth <= 0)
            return enemy.name + " is defeated.";
        return enemy.name + " has " + enemy.health + " health left.";
    }

    public void AddItemsToInventory(List<Item> droppedItems)
    {
        inventory.AddItems(droppedItems);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetDamageMin()
    {
        return damageMin;
    }

    public int GetDamageMax()
    {
        return damageMax;
    }

    public void IncrementArmor(int addArmor)
    {
        armor += addArmor;
    }

    public void IncrementElemental(int addElemental)
    {
        elementalDamage += addElemental;
    }

    public int GetArmor()
    {
        return armor;
    }

    public void DecrementArmor(int decArmor)
    {
        armor -= decArmor;
    }

    public void IncrementHealth(int recovery)
    {
        health = Mathf.Clamp((health + recovery), 0, maxHealth);
    }

    public int DecrementHealth(int damage)
    {
        //making sure armor calculation is included when this method is called
        int totalDamage = Mathf.Clamp(damage, 0, 99999);
        if(totalDamage == 0)
            SoundController.Play((int)SFX.Defend, 0.5f);
        health = Mathf.Clamp((health - damage), 0, maxHealth);
        if (isPoisoned)
        {
            health = Mathf.Clamp((health - 2), 0, maxHealth);
            return totalDamage + 2;
        }
        return totalDamage;
    }

    public void AddPermanentWeight(int itemWeight)
    {
        weight += itemWeight;
    }

    public int GetWeight()
    {
        int bagWeight = 0;
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {        
            foreach (KeyValuePair<Item, int> entry in inventory.inventory)
            {
                bagWeight += entry.Key.weight * entry.Value;
            }
        }
        return bagWeight + weight;
    }

    public int GetGold()
    {
        return gold;
    }

    public void IncrementGold(int gold)
    {
        this.gold += gold;
    }

    public void DecrementGold(int gold)
    {
        this.gold =(int)Mathf.Clamp((this.gold - gold), 0f, int.MaxValue);
    }
}                        Player                                                                                                                                                                                                              acevE26FDefaultDelegateUserPolicyJEE10GetUObjectEv .rel.ARM.exidx.text._ZN20FNetworkPlatformFile16PerformHeartbeatEv .rel.text._ZN20FNetworkPlatformFile16PerformHeartbeatEv .rel.ARM.exidx.text._ZNK13FArchiveState23UseToResolveEnumeratorsEv _ZN13FArchiveState19ResetCustomVersionsEv _ZNK13FArchiveState17GetCustomVersionsEv .rel.ARM.exidx.text._ZN13IPlatformFile21DoesCreatePublicFilesEv _ZN13FArchiveState8SetErrorEv _ZN6FDebug17ProcessFatalErrorEv _ZN