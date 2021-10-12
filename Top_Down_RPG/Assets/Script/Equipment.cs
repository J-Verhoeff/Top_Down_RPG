using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    [SerializeField] private string type;
    [SerializeField] private string description;
    [SerializeField] private int defense;
    [SerializeField] private int attack;
    [SerializeField] private Sprite icon;

    public Sprite getIcon() {
        return icon;
    }

    public string getDescription() {
        return description;
    }

    public string getType() {
        return type;
    }

    public void setDescription(string desc) {
        description = desc;
    }
}
