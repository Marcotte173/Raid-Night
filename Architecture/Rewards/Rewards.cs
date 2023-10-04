using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Rewards : MonoBehaviour
{
    public static Rewards instance;
    public Encounter currentEncounter;
    public int reward;
    public TMP_Text itemName;
    public TMP_Text itemType;
    public TMP_Text stat;
    public TMP_Text statNumber;
    public Character character;
    public ItemSO item;
    public TMP_Text playerName;
    public TMP_Text characterName;
    public TMP_Text characterClass;
    public TMP_Text health;
    public TMP_Text healthNumber;
    public TMP_Text mana;
    public TMP_Text manaNumber;
    public TMP_Text crit;
    public TMP_Text critNumber;
    public TMP_Text spellPower;
    public TMP_Text spellPowerNumber;
    public TMP_Text damage;
    public TMP_Text damageNumber;
    public TMP_Text defence;
    public TMP_Text defenceNumber;
    public TMP_Text head;
    public TMP_Text body;
    public TMP_Text legs;
    public TMP_Text feet;
    public TMP_Text trinket;
    public TMP_Text weapon;
    public TMP_Text offHand;
    public TMP_Text headArmor;
    public TMP_Text bodyArmor;
    public TMP_Text legsArmor;
    public TMP_Text feetArmor;
    public TMP_Text trinketSlot;
    public TMP_Text weaponSlot;
    public TMP_Text offHandSlot;
    public TMP_Text gearScore;
    public TMP_Text gearScoreNumber;
    public List<Button> characterButton;
    public List<Button> itemButton;
    public Button theButton;
    public Button nullCharacter;
    public string infoString;
    public TMP_Text infoText;
    public string infoString1;
    public TMP_Text infoText1;
    public TMP_Text compare;

    private void Awake()
    {
        instance = this;
    }

    public void Begin()
    {
        infoString1 = "Select an Item\nEither select a character to give it to or Disenchant the item";
        infoText1.text = $"\n\n\n\n\n{infoString1}";
        ResetButtons();
        Display();
    }

    private void ResetButtons()
    {
        foreach (Button b in characterButton) Utility.instance.TurnOff(b.gameObject);
        foreach (Button b in itemButton) Utility.instance.TurnOff(b.gameObject);
        if (currentEncounter.drops.Count > 0)
        {
            for (int i = 0; i < currentEncounter.player.Count; i++)
            {
                Utility.instance.TurnOn(characterButton[i].gameObject);
                characterButton[i].GetComponentInChildren<TMP_Text>().text = currentEncounter.player[i].characterName;
                characterButton[i].GetComponentInChildren<TMP_Text>().color = Utility.instance.ClassColor(currentEncounter.player[i]);
            }
            for (int i = 0; i < currentEncounter.drops.Count; i++)
            {
                Utility.instance.TurnOn(itemButton[i].gameObject);
                itemButton[i].GetComponentInChildren<TMP_Text>().text = currentEncounter.drops[i].itemName;
                itemButton[i].GetComponentInChildren<TMP_Text>().color = Utility.instance.RarityColor(currentEncounter.drops[i]);
            }
        }        
    }

    public void TheButton()
    {
        if(item == null && currentEncounter.drops.Count==0)
        {
            DungeonManager.instance.currentDungeon.encountersCompleted++;
            if (DungeonManager.instance.currentDungeon.encountersCompleted >= DungeonManager.instance.currentDungeon.encounter.Count)
            {
                DungeonManager.instance.currentDungeon.dungeonCompleted++;
                EndMatch.instance.SendHome();
                UIManager.instance.Menu();
            }
            else EndMatch.instance.NextEncounter();    
        }
        else if(character == null)
        {
            infoString = $"The {item.itemName} has been disenchanted";
            currentEncounter.drops.Remove(item);
            item = null;
            character = null;
            Utility.instance.TurnOff(nullCharacter.gameObject);
            infoString1 = (currentEncounter.drops.Count == 0)?"The are no more items to loot!":"Select an Item\nEither select a character to give it to or Disenchant the item";
            ResetButtons();
            Display();
        }
        else if (Compatable(character, item))
        {
            Class c = (Class)character;
            if (item.type == ItemType.Head)
            {
                if (c.headSets.Count == 1) c.headSets[0] = item;
                else c.headSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.Chest)
            {
                if (c.chestSets.Count == 1) c.chestSets[0] = item;
                else c.chestSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.Legs)
            {
                if (c.legSets.Count == 1) c.legSets[0] = item;
                else c.legSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.Feet)
            {
                if (c.feetSets.Count == 1) c.feetSets[0] = item;
                else c.feetSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.Trinket)
            {
                if (c.trinketSets.Count == 1) c.trinketSets[0] = item;
                else c.trinketSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.Weapon)
            {
                if (c.weaponSets.Count == 1) c.weaponSets[0] = item;
                else c.weaponSets[c.specNumber] = item;
            }
            else if (item.type == ItemType.OffHand)
            {
                if (c.offHandSets.Count == 1) c.offHandSets[0] = item;
                else c.offHandSets[c.specNumber] = item;
            }
            c.SpecEquip();
            Utility.instance.TurnOff(nullCharacter.gameObject);
            infoString = $"The {item.itemName} has been given to {character.characterName}";
            infoString1 = "Select an Item\nEither select a character to give it to or Disenchant the item";
            currentEncounter.drops.Remove(item);
            item =  null;
            character = null;
            ResetButtons();
            Display();
        }
    }
    public void DisplayCharacter()
    {
        if (character == null)
        {
            playerName.text = "";
            characterName.text = "";
            characterClass.text = "";
            health.text = "";
            healthNumber.text = "";
            mana.text = "";
            manaNumber.text = "";
            crit.text = "";
            critNumber.text = "";
            defence.text = "";
            defenceNumber.text = "";
            damage.text = "";
            damageNumber.text = "";
            spellPower.text = "";
            spellPowerNumber.text = "";
            head.text = "";
            body.text = "";
            legs.text = "";
            feet.text = "";
            trinket.text = "";
            weapon.text = "";
            offHand.text = "";
            headArmor.color = Color.white;
            trinketSlot.color = Color.white;
            weaponSlot.color = Color.white;
            legsArmor.color = Color.white;
            headArmor.text = "";
            bodyArmor.text = "";
            legsArmor.text =  "";
            feetArmor.text = "";
            offHandSlot.text = "";
            weaponSlot.text = "";
            trinketSlot.text = "";
            gearScore.text = "";
            gearScoreNumber.text = "";
            theButton.GetComponentInChildren<TMP_Text>().text = "Disenchant";
        }
        else
        {
            characterClass.GetComponentInChildren<TMP_Text>().color = Utility.instance.ClassColor(character);
            playerName.text = character.player.playerName;
            characterName.text = character.characterName;
            characterClass.text = Utility.instance.SpecName(character);
            spellPower.text = "Spell Power";
            spellPowerNumber.text = character.spellpower.value.ToString();
            crit.text = "Crit";
            critNumber.text = character.crit.value.ToString();
            defence.text = "Defence";
            defenceNumber.text = character.defence.value.ToString();
            damage.text = "Damage";
            damageNumber.text = character.Damage().ToString();
            health.text = "Health";
            healthNumber.text = character.maxHealth.value + "/" + character.maxHealth.value;
            mana.text = (character.GetComponent<Beserker>()) ? "Rage" : (character.GetComponent<Rogue>()) ? "Energy" : "Mana";
            if (character.GetComponent<Beserker>()) manaNumber.color = Color.red;
            else if (character.GetComponent<Rogue>()) manaNumber.color = Color.yellow;
            else manaNumber.color = SpriteList.instance.mana;
            manaNumber.text = (character.GetComponent<Beserker>()) ? "0/100" : (character.GetComponent<Rogue>()) ? "100/100" : character.maxMana.value + "/" + character.maxMana.value;
            head.text = "Head";
            body.text = "Chest";
            legs.text = "Legs";
            feet.text = "Feet";
            trinket.text = "Trinket";
            weapon.text = "Weapon";
            offHand.text = "OffHand";
            headArmor.text = character.head.itemName;
            headArmor.color = Utility.instance.RarityColor(character.head);
            bodyArmor.text = character.chest.itemName;
            bodyArmor.color = Utility.instance.RarityColor(character.chest);
            legsArmor.text = character.legs.itemName;
            legsArmor.color = Utility.instance.RarityColor(character.legs);
            feetArmor.text = character.feet.itemName;
            feetArmor.color = Utility.instance.RarityColor(character.feet);
            trinketSlot.text = character.trinket.itemName;
            trinketSlot.color = Utility.instance.RarityColor(character.trinket);
            weaponSlot.text = character.weapon.itemName;
            weaponSlot.color = Utility.instance.RarityColor(character.weapon);
            offHandSlot.text = character.offHand.itemName;
            offHandSlot.color = Utility.instance.RarityColor(character.offHand);
            gearScore.text = "Gear Score";
            gearScoreNumber.text = (character.head.score + character.chest.score + character.legs.score + character.feet.score + character.trinket.score + character.weapon.score + character.offHand.score).ToString();
            if(item!= null) theButton.GetComponentInChildren<TMP_Text>().text = (Compatable(character,item))?$"Give to {character.characterName}": $"{character.characterName} cannot use {item.itemName}";
        }
    }

    public void DisplayItem()
    {
        if(currentEncounter.drops.Count ==0)
        {
            itemName.text = "";
            itemType.text = "";
            statNumber.text = "";
            infoText.text = $"\n\n\n\n\n{infoString}";
            infoText1.text = $"\n\n\n\n\n{infoString1}";
            compare.text = "";
            theButton.GetComponentInChildren<TMP_Text>().text = $"Move On";
            stat.text = "";
        }   
        else
        {
            if(item == null)
            {
                itemName.text = "";
                itemType.text = "";
                infoText.text = (character != null) ? "" : $"\n\n\n\n\n{infoString}";
                infoText1.text = (character != null) ? "" : $"\n\n\n\n\n{infoString1}";
                statNumber.text = "";
                compare.text = "";
                stat.text = "";
            }
            else
            {
                infoText.text = $"";
                infoText1.text = $"";
                itemName.text = item.itemName;
                itemType.text = Utility.instance.ItemType(item);
                itemName.color = (item.rarity == Rarity.Uncommon) ? SpriteList.instance.uncommon : (item.rarity == Rarity.Rare) ? SpriteList.instance.rare : (item.rarity == Rarity.Epic) ? SpriteList.instance.epic : SpriteList.instance.legendary;
                if (character == null) itemType.color = itemName.color;
                else
                {
                    if (Compatable(character, item)) itemType.color = itemName.color;
                    else itemType.color = SpriteList.instance.bad;
                }
                stat.text = "";
                stat.text += (item.health > 0) ? "Health:\n" : "";
                stat.text += (item.mana > 0) ? "Mana:\n" : "";
                stat.text += (item.manaRegenMod > 0) ? "Mana Regen:\n" : "";
                stat.text += (item.defence > 0) ? "Defence:\n" : "";
                stat.text += (item.damage > 0) ? "Damage:\n" : "";
                stat.text += (item.attackPower > 0) ? "Attack Power:\n" : "";
                stat.text += (item.spellpower > 0) ? "Spell Power:\n" : "";
                stat.text += (item.crit > 0) ? "Crit:\n" : "";
                stat.text += (item.vamp > 0) ? "Vamp:\n" : "";
                stat.text += (item.thorns > 0) ? "Thorns:\n" : "";
                stat.text += (item.haste > 0) ? "Haste:\n" : "";
                stat.text += (item.physicalDamageMod > 0) ? "Physical Damage:\n" : "";
                stat.text += (item.magicDamageMod > 0) ? "Magic Damage:\n" : "";
                stat.text += (item.energyCostMod > 0) ? "Energy Cost:\n" : "";
                stat.text += (item.damageTakenMod > 0) ? "Damage Taken:\n" : "";
                stat.text += (item.healingMod > 0) ? "Healing:\n" : "";
                stat.text += (item.movement > 0) ? "Movement:" : "";
                stat.text += "\n\n\n";
                stat.text += $"Score";
                statNumber.text = "";
                statNumber.text += (item.health > 0) ? $"+ {item.health}\n" : "";
                statNumber.text += (item.mana > 0) ? $"+ {item.mana}\n" : "";
                statNumber.text += (item.manaRegenMod > 0) ? $"+ {item.manaRegenMod}\n" : "";
                statNumber.text += (item.defence > 0) ? $"+ {item.defence}\n" : "";
                statNumber.text += (item.damage > 0) ? $"+ {item.damage}\n" : "";
                statNumber.text += (item.attackPower > 0) ? $"+ {item.attackPower}\n" : "";
                statNumber.text += (item.spellpower > 0) ? $"+ {item.spellpower}\n" : "";
                statNumber.text += (item.crit > 0) ? $"+ {item.crit}\n" : "";
                statNumber.text += (item.vamp > 0) ? $"+ {item.vamp}\n" : "";
                statNumber.text += (item.thorns > 0) ? $"+ {item.thorns}\n" : "";
                statNumber.text += (item.haste > 0) ? $"+ {item.haste}\n" : "";
                statNumber.text += (item.physicalDamageMod > 0) ? $"+ {item.physicalDamageMod}\n" : "";
                statNumber.text += (item.magicDamageMod > 0) ? $"+ {item.magicDamageMod}\n" : "";
                statNumber.text += (item.energyCostMod > 0) ? $"- {item.energyCostMod}\n" : "";
                statNumber.text += (item.damageTakenMod > 0) ? $"- {item.damageTakenMod}\n" : "";
                statNumber.text += (item.healingMod > 0) ? $"+ {item.healingMod}\n" : "";
                statNumber.text += (item.movement > 0) ? $"+ {item.movement}" : "";
                statNumber.text += "\n\n\n";
                statNumber.text += $"{item.score}";
                if (character != null)
                {
                    compare.text = "";
                    compare.text += (item.health > 0) ? $"{Compare(item.health, Item(character, item).health)}\n" : "";
                    compare.text += (item.mana > 0) ? $"{Compare(item.mana, Item(character, item).mana)}\n" : "";
                    compare.text += (item.manaRegenMod > 0) ? $"{Compare(item.manaRegenMod, Item(character, item).manaRegenMod)}\n" : "";
                    compare.text += (item.defence > 0) ? $"{Compare(item.defence, Item(character, item).defence)}\n" : "";
                    compare.text += (item.damage > 0) ? $"{Compare(item.damage, Item(character, item).damage)}\n" : "";
                    compare.text += (item.attackPower > 0) ? $"{Compare(item.attackPower, Item(character, item).attackPower)}\n" : "";
                    compare.text += (item.spellpower > 0) ? $"{Compare(item.spellpower, Item(character, item).spellpower)}\n" : "";
                    compare.text += (item.crit > 0) ? $"{Compare(item.crit, Item(character, item).crit)}\n" : "";
                    compare.text += (item.vamp > 0) ? $"{Compare(item.vamp, Item(character, item).vamp)}\n" : "";
                    compare.text += (item.thorns > 0) ? $"{Compare(item.thorns, Item(character, item).thorns)}\n" : "";
                    compare.text += (item.haste > 0) ? $"{Compare(item.haste, Item(character, item).haste)}\n" : "";
                    compare.text += (item.physicalDamageMod > 0) ? $"{Compare(item.physicalDamageMod, Item(character, item).physicalDamageMod)}\n" : "";
                    compare.text += (item.magicDamageMod > 0) ? $"{Compare(item.magicDamageMod, Item(character, item).magicDamageMod)}\n" : "";
                    compare.text += (item.energyCostMod > 0) ? $"{Compare(item.energyCostMod, Item(character, item).energyCostMod)}\n" : "";
                    compare.text += (item.damageTakenMod > 0) ? $"{Compare(item.damageTakenMod, Item(character, item).damageTakenMod)}\n" : "";
                    compare.text += (item.healingMod > 0) ? $"{Compare(item.healingMod, Item(character, item).healingMod)}\n" : "";
                    compare.text += (item.movement > 0) ? $"{Compare(item.movement, Item(character, item).movement)}\n" : "";
                    compare.text += "\n\n\n";
                    compare.text += (item.score > 0) ? $"{Compare(item.score, Item(character, item).score)}\n" : "";
                }
                else compare.text ="";
            }            
        }
    }

    private ItemSO Item(Character character, ItemSO item)
    {
        Class c = (Class)character;
        if (item.type == ItemType.Head) return (c.headSets.Count == 1) ? c.headSets[0]: c.headSets[c.specNumber];
        else if (item.type == ItemType.Chest) return (c.chestSets.Count == 1) ? c.chestSets[0] : c.chestSets[c.specNumber];
        else if (item.type == ItemType.Legs) return (c.legSets.Count == 1) ? c.legSets[0] : c.legSets[c.specNumber];
        else if (item.type == ItemType.Feet) return (c.feetSets.Count == 1) ? c.feetSets[0] : c.feetSets[c.specNumber];
        else if (item.type == ItemType.Trinket) return (c.trinketSets.Count == 1) ? c.trinketSets[0] : c.trinketSets[c.specNumber];
        else if (item.type == ItemType.Weapon) return (c.weaponSets.Count == 1) ? c.weaponSets[0] : c.weaponSets[c.specNumber];
        else if (item.type == ItemType.OffHand) return (c.offHandSets.Count == 1) ? c.offHandSets[0] : c.offHandSets[c.specNumber];
        return null;
    }
    private string Compare(float x, float y)
    {
        string good = "#05FF00";
        string bad = "#930600";
        string grey = "#BCBCBC";
        string a = (y > x) ? $"- {Mathf.Abs(x - y)}" : $"+ {x - y}";
        if (x - y == 0) return $"<color={grey}>{a}</color>";
        if (x - y > 0)  return $"<color={good}>{a}</color>";
        if (x - y < 0) return $"<color={bad}>{a}</color>";
        return "";
    }

    private bool Compatable(Character character, ItemSO item)
    {
        Debug.Log(character.characterName);
        if (item.armorType == ArmorType.Cloth) return true;
        else if (item.armorType == ArmorType.Leather)
        {
            if (character.GetComponent<Mage>()) return false;
            if (character.GetComponent<Druid>()) return false;
            return true;
        }
        else if (item.armorType == ArmorType.Plate)
        {
            if (character.GetComponent<Beserker>()) return true;
            if (character.GetComponent<ShieldBearer>()) return true;
        }
        else if (item.weaponType == WeaponType.Staff)
        {
            if (character.GetComponent<Mage>()) return true;
            if (character.GetComponent<Bard>()) return true;
            if (character.GetComponent<Druid>()) return true;
        }
        else if (item.hands == Hands.Two && item.weaponType != WeaponType.Staff)
        {
            if (character.GetComponent<Beserker>()) return true;
        }
        else if (item.weaponType == WeaponType.Mace)
        {
            if (character.GetComponent<Bard>()) return true;
            if (character.GetComponent<Druid>()) return true;
        }
        else if (item.weaponType == WeaponType.Sword)
        {
            if (character.GetComponent<ShieldBearer>()) return true;
        }
        else if (item.weaponType == WeaponType.Dagger)
        {
            if (character.GetComponent<Rogue>()) return true;
        }
        else if (item.weaponType == WeaponType.Wand)
        {
            if (character.GetComponent<Mage>()) return true;
        }
        else if (item.offHandType == OffHandType.Dagger)
        {
            if (character.GetComponent<Rogue>()) return true;
        }
        else if (item.offHandType == OffHandType.Book)
        {
            if (character.GetComponent<Mage>()) return true;
        }
        else if (item.offHandType == OffHandType.Orb)
        {
            if (character.GetComponent<Mage>()) return true;
            if (character.GetComponent<Bard>()) return true;
        }
        else if (item.offHandType == OffHandType.Shield)
        {
            if (character.GetComponent<ShieldBearer>()) return true;
        }
        else if (item.offHandType == OffHandType.Source)
        {
            if (character.GetComponent<Bard>()) return true;
            if (character.GetComponent<Druid>()) return true;
        }
        else if (item.offHandType == OffHandType.Relic)
        {
            if (character.GetComponent<Druid>()) return true;
        }
        return false;
    }

    public void BackButton()
    {
        character = null;
        Display();
        Utility.instance.TurnOff(nullCharacter.gameObject);
    }

    public void CharacterButton1()
    {
        character = currentEncounter.player[0];
        Utility.instance.TurnOn(nullCharacter.gameObject);
        Display();
    }
    public void CharacterButton2()
    {
        character = currentEncounter.player[1];
        Utility.instance.TurnOn(nullCharacter.gameObject);
        Display();
    }
    public void CharacterButton3()
    {
        character = currentEncounter.player[2];
        Utility.instance.TurnOn(nullCharacter.gameObject);
        Display();
    }
    public void CharacterButton4()
    {
        character = currentEncounter.player[3];
        Utility.instance.TurnOn(nullCharacter.gameObject);
        Display();
    }
    public void CharacterButton5()
    {
        character = currentEncounter.player[4];
        Utility.instance.TurnOn(nullCharacter.gameObject);
        Display();
    }
    public void ItemButton1()
    {
        item = currentEncounter.drops[0];
        Display();
    }
    public void ItemButton2()
    {
        item = currentEncounter.drops[1];
        Display();
    }
    public void ItemButton3()
    {
        item = currentEncounter.drops[2];
        Display();
    }
    public void ItemButton4()
    {
        item = currentEncounter.drops[3];
        Display();
    }
    public void ItemButton5()
    {
        item = currentEncounter.drops[4];
        Display();
    }

    public void Display()
    {
        DisplayCharacter();
        DisplayItem();
        if (item == null && currentEncounter.drops.Count > 0) Utility.instance.TurnOff(theButton.gameObject);
        else Utility.instance.TurnOn(theButton.gameObject);
    }
}