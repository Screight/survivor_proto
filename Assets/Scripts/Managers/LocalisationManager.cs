using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalisationManager : Singleton<LocalisationManager>
{
    Dictionary<uint, string> m_dictionary;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Initialize()
    {
        m_dictionary = new Dictionary<uint, string>();

        TextAsset localisationFile = Resources.Load("localisation_upgrades") as TextAsset;

        string text = localisationFile.text;
        text = text.Replace("\r", "");

        string[] textInLines = text.Split("\n");

        for(int i = 0; i < textInLines.Length; i++)
        {
            string[] elementTxt = textInLines[i].Split(",");
            if(elementTxt.Length < 4 || elementTxt[1].Length == 0) { continue; }

            try
            {
                uint id = uint.Parse(elementTxt[1]);
                if (m_dictionary.ContainsKey(id)) { continue; }
                m_dictionary.Add(id, elementTxt[3]);
            }
            catch (FormatException e) {}
        }
    }

    public string GetText(uint p_ID)
    {
        if(m_dictionary.ContainsKey(p_ID)) { return m_dictionary[p_ID]; }
        return "ERROR! ID NOT FOUND.";
    }

}
