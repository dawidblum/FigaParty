using System;
using UnityEngine;

public class PizzaSlot : MonoBehaviour {
    public GameObject cheese;
    public GameObject fig;
    public GameObject salami;
    public GameObject ketchup;
    public GameObject mushroom;
    public GameObject olives;
    public GameObject onion;

    
    public GameObject cheeseCheck;
    public GameObject figCheck;
    public GameObject salamiCheck;
    public GameObject ketchupCheck;
    public GameObject mushroomCheck;
    public GameObject olivesCheck;
    public GameObject onionCheck;

    public bool isCorrect = false;


    private PizzaController.Type selected;
    
    public bool Check() {
        switch (selected) {
            case PizzaController.Type.Cheese:
                if (cheeseCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Fig:
                if (figCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Salami:
                if (salamiCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Ketchup:
                if (ketchup.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Mushroom:
                if (mushroomCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Olives:
                if (olivesCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.Onion:
                if (onionCheck.activeInHierarchy)
                    isCorrect = true;
                break;
            case PizzaController.Type.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return isCorrect;
    }
    
    private void ResetAllSlots() {
        cheese.SetActive(false);

        fig.SetActive(false);

        salami.SetActive(false);

        ketchup.SetActive(false);

        mushroom.SetActive(false);

        olives.SetActive(false);

        onion.SetActive(false);
        isCorrect = false;
    }
    
    public void SetSlot(PizzaController.Type _type) {
        ResetAllSlots();
        selected = _type;
        switch (_type) {
            case PizzaController.Type.Cheese:
                cheese.SetActive(true);
                break;
            case PizzaController.Type.Fig:
                fig.SetActive(true);
                break;
            case PizzaController.Type.Salami:
                salami.SetActive(true);
                break;
            case PizzaController.Type.Ketchup:
                ketchup.SetActive(true);
                break;
            case PizzaController.Type.Mushroom:
                mushroom.SetActive(true);
                break;
            case PizzaController.Type.Olives:
                olives.SetActive(true);
                break;
            case PizzaController.Type.Onion:
                onion.SetActive(true);
                break;
            case PizzaController.Type.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
        }
    }
    
}