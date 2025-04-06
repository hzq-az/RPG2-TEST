using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;

    public SerializableDictionary<string, bool> skillTree;

    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentId;

    public SerializableDictionary<string, bool> checkPoints;

    public string cloestCheckpointId;

    public int lostCurrencyAmount;
    public float lostCurrencyX;
    public float lostCurrencyY;

    public SerializableDictionary<string, float> volunmeSetting;
    public GameData()
    {
        lostCurrencyAmount = 0;
        lostCurrencyX = 0;
        lostCurrencyY = 0;


        this.currency = 0;

        skillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();

        cloestCheckpointId = string.Empty;
        checkPoints = new SerializableDictionary<string, bool>();

        volunmeSetting = new SerializableDictionary<string, float>();
    }
}
