using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : Characters
{

    void Start()
    {
        base.characterClass = "Grenadier";
        base.moveSpeed = 20;
        base.maxHealthPoints = 100;
        base.ammoRegular = 10;
        base.specialAmmo = 20;
        base.aimRange = 10;
        base.moveRange = 5;
        healthPoints = maxHealthPoints;
        Introduction();
    }

    protected override void SpecialAttack()
    {

    }
}
