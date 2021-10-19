using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    UnityEngine.Events.UnityEvent onDeath;
    [SerializeField]
    int hitPoints = 5;
    int currentHitPoints;

    private void Awake()
    {
        currentHitPoints = hitPoints;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;

        if(currentHitPoints <= 0)
        {
            if(onDeath != null)
            {
                onDeath.Invoke();
            }
            Destroy(gameObject);
        }
    }    
}
