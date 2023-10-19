using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Utility : MonoBehaviour
{
    public static Utility instance;
   
    private void Awake()
    {
        instance = this;
    }
    public string Class(Character a)
    {
        if (a.player.currentClass.GetComponent<Beserker>()) return "Beserker";
        if (a.player.currentClass.GetComponent<Bard>()) return "Bard";
        if (a.player.currentClass.GetComponent<Druid>()) return "Druid";
        if (a.player.currentClass.GetComponent<Mage>()) return "Mage";
        if (a.player.currentClass.GetComponent<Rogue>()) return "Rogue";
        if (a.player.currentClass.GetComponent<ShieldBearer>()) return "ShieldBearer";
        if (a.GetComponent<Orc>()) return "Orc";
        return null;
    }
    public Color32 ClassColor(Character a)
    {
        if (a.GetComponent<Beserker>()) return SpriteList.instance.beserker;
        else if (a.GetComponent<Mage>()) return SpriteList.instance.mage;
        else if (a.GetComponent<Druid>()) return SpriteList.instance.druid;
        else if (a.GetComponent<Bard>()) return SpriteList.instance.bard;
        else if (a.GetComponent<Rogue>()) return SpriteList.instance.rogue;
        else return SpriteList.instance.shieldBearer;
    }
    public string ClassColorString(Character a)
    {
        if (a.GetComponent<Beserker>()) return TextColor(SpriteList.instance.beserker);
        else if (a.GetComponent<Mage>()) return TextColor(SpriteList.instance.mage);
        else if (a.GetComponent<Druid>())  return TextColor(SpriteList.instance.druid);
        else if (a.GetComponent<Bard>()) return TextColor(SpriteList.instance.bard);
        else if (a.GetComponent<Rogue>()) return TextColor(SpriteList.instance.rogue);
        else return TextColor(SpriteList.instance.shieldBearer);
    }
    public Color32 RarityColor(ItemSO a)
    {
        if (a.rarity == Rarity.Uncommon) return SpriteList.instance.uncommon;
        else if (a.rarity == Rarity.Rare)return SpriteList.instance.rare;
        else if (a.rarity == Rarity.Epic)return SpriteList.instance.epic;
        else return SpriteList.instance.legendary;
    }
    public string RarityColorString(ItemSO a)
    {
        if (a.rarity == Rarity.Uncommon)  return TextColor(SpriteList.instance.uncommon);
        else if (a.rarity == Rarity.Rare) return TextColor(SpriteList.instance.rare);
        else if (a.rarity == Rarity.Epic) return TextColor(SpriteList.instance.epic);
        else return TextColor(SpriteList.instance.legendary);
    }
    public Color32 RarityColor(Item a)
    {
        if (a.rarity == Rarity.Uncommon) return SpriteList.instance.uncommon;
        else if (a.rarity == Rarity.Rare) return SpriteList.instance.rare;
        else if (a.rarity == Rarity.Epic) return SpriteList.instance.epic;
        else return SpriteList.instance.legendary;
    }
    public string RarityColorString(Item a)
    {
        if (a.rarity == Rarity.Uncommon)  return TextColor(SpriteList.instance.uncommon);
        else if (a.rarity == Rarity.Rare) return TextColor(SpriteList.instance.rare);
        else if (a.rarity == Rarity.Epic) return TextColor(SpriteList.instance.epic);
        else return TextColor(SpriteList.instance.legendary);
    }
    public string TextColor(Color32 c)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(c);
    }
   
    public Spec ReturnSpec(Character a)
    {
        if (a.player.currentClass.GetComponent<Beserker>()) return a.player.currentClass.GetComponent<Beserker>().spec ;
        if (a.player.currentClass.GetComponent<Bard>()) return a.player.currentClass.GetComponent<Bard>().spec;
        if (a.player.currentClass.GetComponent<Druid>()) return a.player.currentClass.GetComponent<Druid>().spec;
        if (a.player.currentClass.GetComponent<Mage>()) return a.player.currentClass.GetComponent<Mage>().spec;
        if (a.player.currentClass.GetComponent<Rogue>()) return a.player.currentClass.GetComponent<Rogue>().spec;
        if (a.player.currentClass.GetComponent<ShieldBearer>()) return a.player.currentClass.GetComponent<ShieldBearer>().spec;
        return Spec.Tranquil;
    }
    public string SpecName(Character a)
    {
        if (a.player.currentClass.GetComponent<Beserker>()&& a.player.currentClass.GetComponent<Beserker>().spec == Spec.Stalwart) return "Stalwart Beserker";
        if (a.player.currentClass.GetComponent<Beserker>() && a.player.currentClass.GetComponent<Beserker>().spec == Spec.Focused) return "Focused Beserker";
        if (a.player.currentClass.GetComponent<Bard>() && a.player.currentClass.GetComponent<Bard>().spec == Spec.Tranquil) return "Tranquil Bard";
        if (a.player.currentClass.GetComponent<Bard>() && a.player.currentClass.GetComponent<Bard>().spec == Spec.Inspiring) return "Inspiring Bard";
        if (a.player.currentClass.GetComponent<Druid>() && a.player.currentClass.GetComponent<Druid>().spec == Spec.Wrathful) return "Wrathful Druid";
        if (a.player.currentClass.GetComponent<Druid>() && a.player.currentClass.GetComponent<Druid>().spec == Spec.Redemptive) return "Redemptive Druid";
        if (a.player.currentClass.GetComponent<Mage>() && a.player.currentClass.GetComponent<Mage>().spec == Spec.Focused) return "Focused Mage";
        if (a.player.currentClass.GetComponent<Mage>() && a.player.currentClass.GetComponent<Mage>().spec == Spec.Explosive) return "Explosive Mage";
        if (a.player.currentClass.GetComponent<Rogue>() && a.player.currentClass.GetComponent<Rogue>().spec == Spec.Focused) return "Focused Rogue";
        if (a.player.currentClass.GetComponent<Rogue>() && a.player.currentClass.GetComponent<Rogue>().spec == Spec.Wrathful) return "Wrathful Rogue";
        if (a.player.currentClass.GetComponent<ShieldBearer>() && a.player.currentClass.GetComponent<ShieldBearer>().spec == Spec.Inspiring) return "Inspiring Shield Bearer";
        if (a.player.currentClass.GetComponent<ShieldBearer>() && a.player.currentClass.GetComponent<ShieldBearer>().spec == Spec.Stalwart) return "Stalwart Shield Bearer";
        if (a.GetComponent<Orc>()) return "Orc";
        return null;
    }
    public string SpecNameShort(Character a)
    {
        if (a.player.currentClass.GetComponent<Beserker>() && a.player.currentClass.GetComponent<Beserker>().spec == Spec.Stalwart) return "Stalwart";
        if (a.player.currentClass.GetComponent<Beserker>() && a.player.currentClass.GetComponent<Beserker>().spec == Spec.Focused) return "Focused";
        if (a.player.currentClass.GetComponent<Bard>() && a.player.currentClass.GetComponent<Bard>().spec == Spec.Tranquil) return "Tranquil";
        if (a.player.currentClass.GetComponent<Bard>() && a.player.currentClass.GetComponent<Bard>().spec == Spec.Inspiring) return "Inspiring";
        if (a.player.currentClass.GetComponent<Druid>() && a.player.currentClass.GetComponent<Druid>().spec == Spec.Wrathful) return "Wrathful";
        if (a.player.currentClass.GetComponent<Druid>() && a.player.currentClass.GetComponent<Druid>().spec == Spec.Redemptive) return "Redemptive";
        if (a.player.currentClass.GetComponent<Mage>() && a.player.currentClass.GetComponent<Mage>().spec == Spec.Focused) return "Focused";
        if (a.player.currentClass.GetComponent<Mage>() && a.player.currentClass.GetComponent<Mage>().spec == Spec.Explosive) return "Explosive";
        if (a.player.currentClass.GetComponent<Rogue>() && a.player.currentClass.GetComponent<Rogue>().spec == Spec.Focused) return "Focused";
        if (a.player.currentClass.GetComponent<Rogue>() && a.player.currentClass.GetComponent<Rogue>().spec == Spec.Wrathful) return "Wrathful";
        if (a.player.currentClass.GetComponent<ShieldBearer>() && a.player.currentClass.GetComponent<ShieldBearer>().spec == Spec.Inspiring) return "Inspiring";
        if (a.player.currentClass.GetComponent<ShieldBearer>() && a.player.currentClass.GetComponent<ShieldBearer>().spec == Spec.Stalwart) return "Stalwart";
        if (a.GetComponent<Orc>()) return "Orc";
        return null;
    }
    public void TurnOn(GameObject g)
    {
        if (!g.activeSelf) g.SetActive(true);
    }
    public void TurnOff(GameObject g)
    {
        if (g.activeSelf) g.SetActive(false);
    }
    public string ItemType(Item item)
    {
        string a = item.type.ToString();
        string b = (item.type == global::ItemType.Head || item.type == global::ItemType.Chest || item.type == global::ItemType.Legs || item.type == global::ItemType.Feet) ? item.armorType.ToString() : "";
        if (item.hands == Hands.Two) a = "Two handed " + a;
        return (b == "") ? a : $"{b} {a}";
    }
    public string ItemType(ItemSO item)
    {
        string a = item.type.ToString();
        string b = (item.type == global::ItemType.Head || item.type == global::ItemType.Chest || item.type == global::ItemType.Legs || item.type == global::ItemType.Feet) ? item.armorType.ToString() : "";
        return (b == "") ? a : $"{b} {a}";
    }
        
    public void DamageNumber(Character a,string message, Color32 color)
    {
        if(a!= null && !a.ko)
        {
            DamageNumbers dn = Instantiate(GameObjectList.instance.damageNumbers, a.transform);
            dn.transform.position = new Vector2(a.transform.position.x + Random.Range(-.6f, .61f), a.transform.position.y + 1);
            dn.message = message;
            dn.displayNumber.color = color;
        }        
    }
    public void Aggro(Boss b, Character attacker,float aggroNumber)
    {
        //Aggro
        bool aggroThere = false;
        if (b.aggro.Count > 0)
        {
            for (int i = 0; i < b.aggro.Count; i++)
            {
                if (b.aggro[i].agent == attacker)
                {
                    aggroThere = true;
                    b.aggro[i].ChangeAggro(aggroNumber);
                    break;
                }
            }
        }
        if (aggroThere == false) b.CreateAggro(attacker, aggroNumber);
        AggroBelief( b,  (Class)attacker,  aggroNumber);
    }
    public void AggroBelief(Boss b, Class attacker, float aggroNumber)
    {
        List<float> belief = new List<float> { .4f, .3f, .25f, 1.5f, .05f };
        float beliefNumber = belief[System.Convert.ToInt32(attacker.player.currentSkill)-1];
        float accuracy = Random.Range(beliefNumber * -1, beliefNumber);
        aggroNumber *= (1 + accuracy);
        //Aggro
        bool aggroThere = false;
        if (attacker.aggroBelief != null && attacker.aggroBelief.Count > 0)
        {
            for (int i = 0; i < attacker.aggroBelief.Count; i++)
            {
                if (attacker.aggroBelief[i].boss == b)
                {
                    aggroThere = true;
                    attacker.aggroBelief[i].ChangeAggro(aggroNumber);
                    break;
                }
            }
        }
        if (aggroThere == false) attacker.CreateAggroBelief(b, aggroNumber);
    }
    public float Threat(float damage, float threat)
    {
        return damage * threat / 100;
    }
    public void SortAggro(List<Aggro> list)
    {
        Aggro temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].aggro < list[i + 1].aggro)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public void SortAggro(List<AggroBelief> list)
    {
        AggroBelief temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].aggroBelief < list[i + 1].aggroBelief)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public Character NeedsHeal(List<Character> list)
    {
        if (list.Count == 1) return list[0];
        else
        {
            Character c = list[0];
            float x = list[0].maxHealth.value - list[0].health;
            for (int i = 1; i < list.Count; i++)
            {
                if ((list[i].maxHealth.value - list[i].health) < x)
                {
                    c = list[i];
                    x = list[i].maxHealth.value - list[i].health;
                }
            }
            return c;
        }        
    }
    public bool AggroCheck(Character character)
    {
        Boss b = (Boss)character.target;
        Class c = (Class)character;
        if (b.aggro.Count >0)
        {
            float perceivedTopAggro = b.aggro[0].aggro;
            foreach (AggroBelief a in c.aggroBelief)
            {
                if (a.boss == b)
                {
                    if (c.aggroBelief[0].aggroBelief > perceivedTopAggro) return false;
                }
            }            
        }
        return true;
    }    
}
