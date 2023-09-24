using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    public static CodexManager instance;
    public List<Codex> categories;
    public List<Codex> entries;
    public Codex targetCategory;
    public Codex targetEntry;
    public TMP_Text nullText;
    public GameObject entry;
    public int page;
    private void Start()
    {
        instance = this;
        
    }

    ////////////////////////////////////////////////////////////////////////
    /// 
    /// GENERATION
    ///
    public void Generate()
    {
        Dungeons();
    }

    private void Dungeons()
    {
        for (int i = 0; i < categories.Count; i++)
        {
            Codex c = Instantiate(categories[i], transform);
            c.name = c.codexName;
            categories[i] = c;
        }
        Codex main = categories[3];
        foreach(Dungeon d in DungeonManager.instance.pve)
        {
            if (d.id > 0)
            {
                Codex dc = Instantiate(GameObjectList.instance.entry, main.transform);
                dc.discovered = true;
                dc.name = dc.codexName = d.dungeonName;
                dc.entryNamePage.Add(d.dungeonName);
                dc.entry1Page.Add(d.description[0]);
                dc.entry2Page.Add(d.description.Count > 1 ? d.description[1]:"");
                dc.entryImagePage.Add(d.dungeonPic);
                foreach(Encounter e in d.encounter)
                {
                    Boss b = (Boss)e.bossSummon[0];
                    dc.entryNamePage.Add(b.characterName);                    
                    dc.entry1Page.Add(b.flavor);
                    dc.entry2Page.Add("");
                    dc.entryImagePage.Add(b.GetComponent<SpriteRenderer>().sprite);
                }
                main.entries.Add(dc);
            }
        }
    }
    /// 
    /// GENERATION END
    /// 
    /////////////////////////////////////////////////////////////////////////////////////////
    ///
    ///
    /// 
    /////////////////////////////////////////////////////////////////////////////////////////
    /// 
    /// Codex Manager
    /// 
    public void CodexShow()
    {
        Utility.instance.TurnOn(GuildUI.instance.codex);
        foreach (Image a in GuildUI.instance.codexEntryButtons) Utility.instance.TurnOff(a.gameObject);
        foreach (Image a in GuildUI.instance.codexCategoryButtons) Utility.instance.TurnOff(a.gameObject);
        //POPULATE BUTTONS PROPERLY
        //Category
        List<Codex> cat = AvailableCategories();
        for (int i = 0; i < cat.Count; i++)
        {
            Utility.instance.TurnOn(GuildUI.instance.codexCategoryButtons[i].gameObject);
            GuildUI.instance.codexCategoryButtons[i].GetComponent<ItemSprite>().button.GetComponentInChildren<TMP_Text>().text = cat[i].codexName;
        }
        //Events
        if (targetCategory != null)
        {
            List<Codex> ent = AvailableEntries();
            for (int i = 0; i < ent.Count; i++)
            {
                Utility.instance.TurnOn(GuildUI.instance.codexEntryButtons[i].gameObject);
                GuildUI.instance.codexEntryButtons[i].GetComponent<ItemSprite>().button.GetComponentInChildren<TMP_Text>().text = ent[i].codexName;
            }
        }
        //Show Information
        if (targetCategory == null)
        {
            nullText.text = "Please select a Category";
            Utility.instance.TurnOff(entry);
        }
        //Show Information
        else if (targetCategory != null && targetEntry == null)
        {
            nullText.text = "Please select an entry";
            Utility.instance.TurnOff(entry);
        }
        else if (targetCategory != null && targetEntry != null)
        {
            nullText.text = "";
            Utility.instance.TurnOn(entry);
            if (page == 0) Utility.instance.TurnOff(GuildUI.instance.codexBackForthButtons[0].gameObject);
            else Utility.instance.TurnOn(GuildUI.instance.codexBackForthButtons[0].gameObject);
            if (page == targetEntry.entry1Page.Count - 1) Utility.instance.TurnOff(GuildUI.instance.codexBackForthButtons[1].gameObject);
            else Utility.instance.TurnOn(GuildUI.instance.codexBackForthButtons[1].gameObject);
            targetEntry.entry1Page[page] = targetEntry.entry1Page[page].Replace("\\n", "\n");
            targetEntry.entry2Page[page] = targetEntry.entry2Page[page].Replace("\\n", "\n");
            GuildUI.instance.codexPic.sprite = targetEntry.entryImagePage[page];
            GuildUI.instance.codexText[0].text = targetEntry.entryNamePage[page];
            GuildUI.instance.codexText[1].text = targetEntry.entry1Page[page];
            GuildUI.instance.codexText[2].text = targetEntry.entry2Page[page];
        }
    }
    public void CategorySelect(int x)
    {
        targetEntry = null;
        targetCategory = categories[x];
        CodexShow();
    }
    public void EntrySelect(int x)
    {
        targetEntry = targetCategory.entries[x];
        page = 0;
        CodexShow();
    }
    public void PageUp()
    {
        Debug.Log("Poo");
        page++;
        CodexShow();
    }
    public void PageDown()
    {
        page--;
        CodexShow();
    }
    public void CloseCodex()
    {
        Utility.instance.TurnOff(GuildUI.instance.codex);
    }
    public List<Codex> AvailableCategories()
    {
        List<Codex> codices = new List<Codex>();
        foreach (Codex c in categories) if (c.discovered) codices.Add(c);
        return codices;
    }
    public List<Codex> AvailableEntries()
    {
        List<Codex> codices = new List<Codex>();
        foreach (Codex c in targetCategory.entries) if (c.discovered) codices.Add(c);
        return codices;
    }
}
