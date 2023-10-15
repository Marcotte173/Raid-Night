using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityList : MonoBehaviour
{
    public static AbilityList instance;
    private void Awake()
    {
        instance = this;
    }
    public Ability basicAttack;
    public List<Ability> beserkerStalwart;
    public List<Ability> beserkerFocused;
    public List<Ability> druidRedemptive;
    public List<Ability> druidWrath;
    public List<Ability> bardTranquil;
    public List<Ability> bardInspiring;
    public List<Ability> shieldStalwart;
    public List<Ability> shieldInspiring;
    public List<Ability> rogueFocused;
    public List<Ability> rogueWrathful;
    public List<Ability> mageExplosive;
    public List<Ability> mageFocused;
    public List<Ability> kwasi;
    public List<Ability> retha;
    public List<Ability> boss;
    public List<Ability> schar;
    public List<Ability> arcanos;
    public List<Ability> boros;
    public List<Ability> paladinOfCeres;
    public List<Ability> paladinOfAkalos;
    public List<Ability> paladinOfMaritae;
    public List<Ability> paladinOfDurona;
}
