using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : Characters
{
    // Start is called before the first frame update
    void Start()
    {
        base.characterClass = "Medic";
        base.moveSpeed = 20;
        base.maxHealthPoints = 100;
        base.ammoRegular = 10;
        base.specialAmmo = 10;
        base.aimRange = 1;
        base.moveRange = 10;
        healthPoints = maxHealthPoints;
        Introduction();
    }
    // Update is called once per frame
    protected override void SpecialAttack()
    {

    }
}
