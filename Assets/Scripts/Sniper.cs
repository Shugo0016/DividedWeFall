using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Characters
{
    // Start is called before the first frame update
    void Start()
    {
        base.characterClass = "Sniper";
        base.moveSpeed = 20;
        base.maxHealthPoints = 100;
        base.ammoRegular = 10;
        base.specialAmmo = 20;
        base.aimRange = 30;
        base.moveRange = 20;
        healthPoints = maxHealthPoints;
        Introduction();
    }

    protected override void SpecialAttack()
    {

    }
}
