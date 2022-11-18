using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    protected string characterClass;

    protected int moveSpeed;

    protected int healthPoints;

    protected int maxHealthPoints;

    protected int ammoRegular;

    protected int damageRegular;

    protected int specialAmmo;

    protected int aimRange;

    protected int moveRange;
    // Start is called before the first frame update

    void Update()
    {
        if (healthPoints < 1)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Take Damage");
    }

    protected void Introduction()
    {
        Debug.Log("My class is " + characterClass + ", HP: " + healthPoints);
    }

    private void takeDamage(string type)
    {
        if (string.Equals(type, "medkit"))
        {
            if (healthPoints < 50)
            {
                healthPoints += 50;
            }
            else
            {
                healthPoints = maxHealthPoints;
            }
        }
        else if (string.Equals(type, "regular"))
        {
            if (healthPoints < 20)
            {
                healthPoints = 0;
                Dead();
            }
            else
            {
                healthPoints -= 30;
            }
        }
        else if (string.Equals(type, "sniper"))
        {
            if (healthPoints < 15)
            {
                healthPoints = 0;
                Dead();
            }
            else
            {
                healthPoints -= 15;
            }
        }
    }

    private Collider[] FindCharacters(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        return hitColliders;
    }

    private void Attack()
    {

    }

    protected virtual void SpecialAttack() { }

    private void Dead()
    {
        Debug.Log("Kill Object");
    }
}
