using System.Collections;
using System.Xml.Serialization;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("Player Stats")]
    public float playerMaxDamage = 5.0f;
    public float playerMinDamage = 1.0f;
    public float playerDamage;
    public float playerAcc = 85f;
    public float hitAcc;
    public float playerHealth = 5.0f;
    public float maxHealth;
    public float wheyflaskHealing = 10.0f;
    public float wheyflaskQuantity = 3;
    public float maxWheyflaskQuantity = 3;

    [Header("Buttons")]
    public Button attackHeadButton;
    public Button attackArmButton;
    public Button attackLegButton;
    public Button attackTailButton;
    public Button wheyflaskButton;

    [Header("Set in Inspector")]
    public Manager manager;
    public Transform healthBar;
    public TMP_Text hpText;
    public TMP_Text wheyflaskText;

    [Header("Set by script")]
    public Enemy enemy;

    [Header("Death Animation")]
    public GameObject deathGO;

    bool playerTurn = false;
    bool battleActive = false;
    

    private string[] hitLines = { "A solid hit!", "Steel meets flesh!", "Your blow lands!", "Mold flees your blade!", "You cleave the rot!", "Your weapon sinks into soft decay!", "A clean cut through corruption!", "A sanctified strike!" };
    private string[] missLines = { "The mold mocks your aim.", "You strike only curdled air.", "Your weapon bites nothing but dust.", "Faith falters. The blow fails.", "A swing in vain.", "You stumble on your footing.", "The foe dances aside." };
    void Start()
    {
        maxHealth = playerHealth;
    }

    
    void Update()
    {
        float normalized = Mathf.Clamp01(playerHealth / maxHealth);

        Vector3 scale = healthBar.localScale;
        scale.x = normalized;
        healthBar.localScale = scale;

        hpText.text = $"{playerHealth:0.0}/{maxHealth:0.0}";
    }

    public void OnAttackHeadPressed()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        if (RollAcc())
        {
            RollDamage();
            enemy.TakeDamage("head", playerDamage);
            WriteHitLine();
        }
        else
        {
            WriteMissLine();
            //Debug.Log("Player missed!");
        }

        EndPlayerTurn();
    }
    public void OnAttackArmPressed()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        if (RollAcc())
        {
            RollDamage();
            enemy.TakeDamage("arm", playerDamage);
            WriteHitLine();
        }
        else
        {
            WriteMissLine();
            //Debug.Log("Player missed!");
        }

        EndPlayerTurn();
    }
    public void OnAttackLegPressed()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        if (RollAcc())
        {
            RollDamage();
            enemy.TakeDamage("leg", playerDamage);
            WriteHitLine();
        }
        else
        {
            WriteMissLine();
            //Debug.Log("Player missed!");
        }

        EndPlayerTurn();
    }
    public void OnAttackTailPressed()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        if (RollAcc())
        {
            RollDamage();
            enemy.TakeDamage("tail", playerDamage);
            WriteHitLine();
        }
        else
        {
            WriteMissLine();
            //Debug.Log("Player missed!");
        }

        EndPlayerTurn();
    }

    public void DisableButtons()
    {
        attackHeadButton.interactable = false;
        attackArmButton.interactable = false;
        attackLegButton.interactable = false;
        attackTailButton.interactable = false;
    }

    public void EnableButtons()
    {
        attackHeadButton.interactable = true;
        attackArmButton.interactable = true;
        attackLegButton.interactable = true;
        attackTailButton.interactable = true;
    }

    void RollDamage()
    {
        playerDamage = Random.Range(playerMinDamage, playerMaxDamage);
        //Debug.Log("Roll Damage: " + playerDamage);
    }

    bool RollAcc()
    {
        hitAcc = Random.Range(0f, 100f);
        if (hitAcc <= playerAcc)
        {
            //Debug.Log("Roll Accuracy: " + hitAcc + " <= " + playerAcc + ". HIT");
            return true;
        }
        else
        {
            //Debug.Log("Roll Accuracy: " + hitAcc);
            return false;
        }
    }

    void WriteHitLine()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.attackSFX);
        int randomInt = Random.Range(0, hitLines.Length);
        manager.textBox.text = hitLines[randomInt];
    }

    void WriteMissLine()
    {
        int randomInt = Random.Range(0, missLines.Length);
        manager.textBox.text = missLines[randomInt];
        RuntimeManager.PlayOneShot(FMODEvents.instance.swingMissSFX);
    }

    public void StartBattle(Enemy e)
    {
        enemy = e;
        battleActive = true;

        bool playerStarts = Random.value < 0.5f; //coin flip 
        if (playerStarts)
        {
            playerTurn = true;
            EnableButtons();
            manager.textBox.text = "You act first.";
        }
        else
        {
            playerTurn = false;
            DisableButtons();
            manager.textBox.text = "The foe moves first.";
            StartCoroutine(EnemyTurn());
        }
    }

    private void EndPlayerTurn()
    {
        playerTurn = false;
        DisableButtons();

        if (enemy == null) return;

        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        if (!battleActive || enemy == null)
            yield break;

        yield return new WaitForSeconds(2.0f); //brief pause

        if (RollEnemySpeed())
        {

            manager.textBox.text = "The foe strikes!";

            if (!battleActive || enemy == null)
                yield break;

            float acc = Random.Range(0f, 100f);
            if (acc < enemy.enemyAcc)
            {
                float dmg = Random.Range(enemy.enemyMinDamage, enemy.enemyMaxDamage);
                enemy.Shake();
                manager.textBox.text += $"\nYou take {dmg:0.0} damage!";
                RuntimeManager.PlayOneShot(FMODEvents.instance.attackSFX);
                playerHealth -= dmg;
                CheckPlayerHealth();
            }
            else
            {
                RuntimeManager.PlayOneShot(FMODEvents.instance.swingMissSFX);
                manager.textBox.text += "\nIt misses!";
            }

            yield return new WaitForSeconds(1.5f); //brief pause before player takes control

            if (!battleActive || enemy == null)
                yield break;

            playerTurn = true;
            EnableRemainingButtons();
            manager.textBox.text += "\nWhere will you strike?";
        }
        else
        {
            manager.textBox.text = "The enemy is too slow.";
            yield return new WaitForSeconds(1.5f);
            if (!battleActive || enemy == null)
                yield break;

            playerTurn = true;
            EnableRemainingButtons();
            manager.textBox.text += "\nWhere will you strike?";
        }
    }

    public void EndBattle()
    {
        battleActive = false;
        playerTurn = false;
        DisableButtons();
        StopAllCoroutines();
        manager.textBox.text = "The battle ends.";

        manager.continueButton.gameObject.SetActive(true);
    }

    public void EnableRemainingButtons()
    {
        if (enemy.headHealth > 0)
            attackHeadButton.interactable = true;
        if (enemy.armHealth > 0)
            attackArmButton.interactable = true;
        if (enemy.legHealth > 0)
            attackLegButton.interactable = true;
        if(enemy.tailHealth > 0)
            attackTailButton.interactable = true;
    }

    public void CheckPlayerHealth()
    {
        if (playerHealth <= 0)
        {
            StopAllCoroutines();
            manager.textBox.text = "YOU DIED";
            Instantiate(deathGO);
        }
    }

    bool RollEnemySpeed()
    {
        float speed = Random.Range(0f, 100f);

        if (speed <= enemy.enemySpeed)
            return true;
        else
            return false;
    }

    public void OnSipWheyflaskPress()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        if (wheyflaskQuantity > 0)
        {
            RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
            playerHealth += wheyflaskHealing;

            if (playerHealth > maxHealth)
                playerHealth = maxHealth;

            wheyflaskQuantity -= 1;
            wheyflaskText.text = "(" +wheyflaskQuantity + "/" + maxWheyflaskQuantity + ")";
        }
    }
}
