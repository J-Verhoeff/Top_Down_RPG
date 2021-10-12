using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {
    [SerializeField] int Inventory_Size = 6;

    private Equipment armour; // currently equiped armour
    private Equipment weapon; // currently equiped weapon
    private List<Equipment> unequiped;
    private Equipment selected;

    private GameObject inventory;
    private GameObject weapon_slot;
    private GameObject armour_slot;
    private GameObject[] inventorySlots;
    private int inventory_count = 0;

    private void Awake() {
        unequiped = new List<Equipment>();
        inventorySlots = GameObject.FindGameObjectsWithTag("inventory_button");
        foreach(GameObject i in inventorySlots) {
            i.transform.GetChild(0).gameObject.SetActive(false);
        }
        weapon_slot = GameObject.FindGameObjectWithTag("weapon_button");
        weapon_slot.transform.GetChild(0).gameObject.SetActive(false);
        armour_slot = GameObject.FindGameObjectWithTag("armour_button");
        armour_slot.transform.GetChild(0).gameObject.SetActive(false);

        inventory = GameObject.Find("Inventory");
        inventory.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "equipment" && inventory_count <= Inventory_Size) {
            Equipment newItem = collision.gameObject.GetComponent<Equipment>();
            unequiped.Add(newItem);
            Destroy(collision.gameObject);
            inventory_count++;
            // Debug.Log(newItem.getDescription());
            int count = 1;
            foreach(GameObject i in inventorySlots) {
                if(count == inventory_count) {
                    GameObject child = i.transform.GetChild(0).gameObject;
                    // Debug.Log(child.GetComponent<Image>().sprite);
                    child.GetComponent<Image>().sprite = newItem.getIcon();
                    child.SetActive(true);
                    break;
                }
                count++;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            inventory.SetActive(!inventory.activeSelf);
        }
    }

    public void equip_weapon() {
        // equip an item
        if(selected.getDescription() != "null") {
            if (selected.getType() == "weapon") {
                weapon = selected;
                //Debug.Log(weapon.getDescription());
                weapon_slot.transform.GetChild(0).gameObject.SetActive(true);
                weapon_slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = weapon.getIcon();
                
                //Debug.Log(weapon.getDescription());
                //Debug.Log(selected.getDescription());
            }
            selected.setDescription("null");
        } else {
            // weapon.setDescription("null");
            weapon_slot.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void equip_armour() {
        // equip an item
        //Debug.Log(selected.getType());
        if (selected.getDescription() != "null") {
            if (selected.getType() == "armour") {
                armour = selected;
                armour_slot.transform.GetChild(0).gameObject.SetActive(true);
                armour_slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = armour.getIcon();
            }
            selected.setDescription("null");
        }
        else {
            // armour.setDescription("null"); ;
            armour_slot.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void select() {
        // select an item from the inventory
        foreach(GameObject i in inventorySlots) {
            if(EventSystem.current.currentSelectedGameObject == i) {
                Debug.Log(i.name);
                foreach(Equipment j in unequiped) {
                    if(i.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == j.getIcon()) {
                        selected = j;
                        break;
                    }
                }
                break;
            }
        }
    }
}
