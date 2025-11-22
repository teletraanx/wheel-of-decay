using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using FMODUnity;

public class Enemy : MonoBehaviour
{
    public GameObject enemyGO;

    [Header("Set in Inspector: Limbs")]
    [SerializeField] public GameObject head;
    [SerializeField] public float headHealth = 5.0f;
    [SerializeField] public GameObject arm;
    [SerializeField] public float armHealth = 5.0f;
    [SerializeField] public GameObject leg;
    [SerializeField] public float legHealth = 5.0f;
    [SerializeField] public GameObject tail;
    [SerializeField] public float tailHealth = 5.0f;

    [Header("Set in Inspector")]
    public CombatManager combatManager;
    public Manager manager;

    [Header("Enemy Start Stats")]
    public float enemyAcc = 70f;
    public float enemyMinDamage = 1.0f;
    public float enemyMaxDamage = 4.0f;
    public float enemySpeed = 90.0f;

    public float totalHealth;
    void Start()
    {
        totalHealth = headHealth + armHealth + legHealth + tailHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(string limb, float amount)
    {
        switch (limb)
        {
            case "head":
                Shake();
                headHealth -= amount;
                totalHealth = headHealth + armHealth + legHealth + tailHealth;
                if (headHealth <= 0)
                {
                    combatManager.attackHeadButton.interactable = false;
                    head.SetActive(true);
                    //Debug.Log("Enemy accuracy was " + enemyAcc);
                    enemyAcc -= enemyAcc * 0.25f;
                    //Debug.Log("Enemy accuracy is now " + enemyAcc);


                }
                CheckHealth();
                break;
            case "arm":
                Shake();
                armHealth -= amount;
                totalHealth = headHealth + armHealth + legHealth + tailHealth;
                if (armHealth <= 0)
                {
                    combatManager.attackArmButton.interactable = false;
                    arm.SetActive(true);
                    //Debug.Log("Enemy damage was: " + enemyMinDamage + " - " +  enemyMaxDamage);
                    enemyMinDamage -= enemyMinDamage * 0.33f;
                    enemyMaxDamage -= enemyMaxDamage * 0.33f;
                    //Debug.Log("Enemy damage is now: " + enemyMinDamage + " - " + enemyMaxDamage);
                }
                CheckHealth();
                break;
            case "leg":
                Shake();
                legHealth -= amount;
                totalHealth = headHealth + armHealth + legHealth + tailHealth;
                if (legHealth <= 0)
                {
                    combatManager.attackLegButton.interactable = false;
                    leg.SetActive(true);
                    //Debug.Log("Enemy damage was: " + enemyMinDamage + " - " + enemyMaxDamage);
                    enemyMinDamage -= enemyMinDamage * 0.33f;
                    enemyMaxDamage -= enemyMaxDamage * 0.33f;
                    //Debug.Log("Enemy damage is now: " + enemyMinDamage + " - " + enemyMaxDamage);
                }
                CheckHealth();
                break;
            case "tail":
                Shake();
                tailHealth -= amount;
                totalHealth = headHealth + armHealth + legHealth + tailHealth;
                if (tailHealth <= 0)
                {
                    combatManager.attackTailButton.interactable = false;
                    tail.SetActive(true);
                    //Debug.Log("Enemy speed was " + enemySpeed);
                    enemySpeed -= enemySpeed * 0.25f;
                    //Debug.Log("Enemy speed is now " + enemySpeed);
                }
                CheckHealth();
                break;
        }
    }

    void Death()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.deathSFX);
        Destroy(enemyGO);
        combatManager.EndBattle();
        if (manager.roomIndex == 24)
            manager.cinematicManager.GoToEndScene();
    }

    void CheckHealth()
    {
        if (totalHealth <= 0)
        {
            Death();
            combatManager.DisableButtons();

            manager.textBox.text = "";
        }
    }

    public void Shake(float duration = 0.15f, float magnitude = 0.2f)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    public IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // return to original position cleanly
        transform.localPosition = originalPos;
    }
}
