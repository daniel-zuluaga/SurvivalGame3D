using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public float noSleepHungerThirstDecay;

    public UnityEvent onTakeDamage;

    void Start()
    {
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        thirst.curValue = thirst.startValue;
        sleep.curValue = sleep.startValue;
    }

    void Update()
    {
        hunger.Substract(hunger.decayRate * Time.deltaTime);
        thirst.Substract(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);
        //sleep.Substract(sleep.decayRate * Time.deltaTime);

        if (hunger.curValue == 0.0f)
            health.Substract(noHungerHealthDecay * Time.deltaTime);
        if (thirst.curValue == 0.0f)
            health.Substract(noThirstHealthDecay * Time.deltaTime);
        //if (sleep.curValue == 0.0f) {
        //    hunger.Substract(noSleepHungerThirstDecay * Time.deltaTime);
        //    thirst.Substract(noSleepHungerThirstDecay * Time.deltaTime);
        //}

        // check if player is dead
        if(health.curValue == 0.0f)
        {
            Die();
        }

        // updating ui needs
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Substract(amount);
    }

    public void TakePhysicalDamage(int amount)
    {
        health.Substract(amount);
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Player is dead");
    }
}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;

    public Image uiBar;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
