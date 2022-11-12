using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Characters
{
    // Start is called before the first frame update
    void Start()
    {
        base.characterClass = "Soldier";
        base.moveSpeed = 20;
        base.maxHealthPoints = 100;
        base.ammoRegular = 10;
        healthPoints = maxHealthPoints;
        Introduction();
    }

    protected override void SpecialAttack()
    {

    }
}
