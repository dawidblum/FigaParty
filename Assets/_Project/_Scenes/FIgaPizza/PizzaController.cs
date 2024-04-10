using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class PizzaController : MonoBehaviour {
    [SerializeField] private List<PizzaOrderData> pizzaOrders;
    [SerializeField] private RectTransform orderRect;
    [SerializeField] private Transform pizza;
    
    public PizzaSlot first;
    public PizzaSlot second;
    public PizzaSlot third;
    public PizzaSlot fourth;
    
    public GameObject cheese;
    public GameObject fig;
    public GameObject salami;
    public GameObject ketchup;
    public GameObject mushroom;
    public GameObject olives;
    public GameObject onion;

    private bool canOrder;

    private bool isOverPizza;
    private bool isOverBell;

    private Type heldIngredient;
    private Type hoverIngredient;
    
    public GameObject cheesePizza;
    public GameObject figPizza;
    public GameObject salamiPizza;
    public GameObject ketchupPizza;
    public GameObject mushroomPizza;
    public GameObject olivesPizza;
    public GameObject onionPizza;

    private bool checkingOrder;
    private bool isOrderCorrect;

    private int pizzasMade;
    
    public enum Type {
        Cheese,
        Fig,
        Salami,
        Ketchup,
        Mushroom,
        Olives,
        Onion,
        None
    }

    private void Start() {
        hoverIngredient = Type.None;
        heldIngredient = Type.None;
        //FinishOrder();
        Invoke(nameof(GenerateOrder),7f);
    }

    private void Update() {
        if (pizzasMade == 4) {
            SceneLoader.Instance.LoadScene(4);
        }
        if(GameManager.Instance.gameStopped) return;
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (heldIngredient == Type.None && hoverIngredient != Type.None) {
                Pick(hoverIngredient);
            }
            else if (heldIngredient == Type.None && hoverIngredient == Type.None && isOverBell) {
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Bell_Press);
                if (checkingOrder == false) {
                    FinishOrder();
                    checkingOrder = true;
                }                
            }
            else if (heldIngredient != Type.None) {
                if (isOverPizza) {
                    AddToPizza(heldIngredient);
                }
                else {
                    Drop();
                }
            }
        }
    }

    private void GenerateOrder() {
        var randomOrder = pizzaOrders[Random.Range(0, pizzaOrders.Count - 1)];
        
        SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.New_Order);
        orderRect.DOLocalMoveY(16.4f, .5f).SetEase(Ease.InBounce);
        first.SetSlot(randomOrder.first);
        second.SetSlot(randomOrder.second);
        third.SetSlot(randomOrder.third);
        fourth.SetSlot(randomOrder.fourth);
        canOrder = false;
    }

    private void FinishOrder() {
        isOrderCorrect = true;
        pizzasMade++;
        first.Check();
        second.Check();
        third.Check();
        fourth.Check();
        
        if (!first.isCorrect) {
            isOrderCorrect = false;
        }
        else if (!second.isCorrect) {
            isOrderCorrect = false;
        }
        else if (!third.isCorrect) {
            isOrderCorrect = false;
        }
        else if (!fourth.isCorrect) {
            isOrderCorrect = false;
        }
        if(isOrderCorrect)
            SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Correct_Order);
        else {
            SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Wrong_Order);
        }
        orderRect.DOLocalMoveY(116, .5f).SetEase(Ease.OutExpo).SetDelay(1f).OnComplete(()=> {
            pizza.DOLocalMoveX(-9, .5f).SetEase(Ease.OutExpo).OnComplete(() => {
                GenerateOrder();
                ClearPizza();
                checkingOrder = false;
            });
        });
    }

    private void AddToPizza(Type _type) {
        Drop();
        switch (_type) {
            case Type.Cheese:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                cheesePizza.SetActive(true);
                break;
            case Type.Fig:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                figPizza.SetActive(true);
                break;
            case Type.Salami:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                salamiPizza.SetActive(true);
                break;
            case Type.Ketchup:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Sauce);

                ketchupPizza.SetActive(true);
                break;
            case Type.Mushroom:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                mushroomPizza.SetActive(true);
                break;
            case Type.Olives:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                olivesPizza.SetActive(true);
                break;
            case Type.Onion:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                onionPizza.SetActive(true);
                break;
            case Type.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pizza")
            isOverPizza = true;
        if (other.gameObject.tag == "Bell")
            isOverBell = true;
        
        var ingredient = other.GetComponent<PizzaIngredient>();
        if(ingredient == null) return;

        hoverIngredient = ingredient.type;

    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Pizza")
            isOverPizza = false;
        if (other.gameObject.tag == "Bell")
            isOverBell = false;    
        
        var ingredient = other.GetComponent<PizzaIngredient>();
        if(ingredient == null) return;
        hoverIngredient = Type.None;
    }

    private void Pick(Type _type) {
        heldIngredient = _type;
        switch (_type) {
            case Type.Cheese:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                cheese.SetActive(true);
                break;
            case Type.Fig:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                fig.SetActive(true);
                break;
            case Type.Salami:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                salami.SetActive(true);
                break;
            case Type.Ketchup:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Sauce);

                ketchup.SetActive(true);
                break;
            case Type.Mushroom:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                mushroom.SetActive(true);
                break;
            case Type.Olives:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                olives.SetActive(true);
                break;
            case Type.Onion:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Pick_Ingredient);

                onion.SetActive(true);
                break;
            case Type.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
        }
    }

    private void ClearPizza() {
        pizza.DOLocalMoveX(0, .5f).SetEase(Ease.OutExpo);

        cheesePizza.SetActive(false);
        figPizza.SetActive(false);
        salamiPizza.SetActive(false);
        ketchupPizza.SetActive(false);
        mushroomPizza.SetActive(false);
        olivesPizza.SetActive(false);
        onionPizza.SetActive(false);
    }
    private void Drop() {
        heldIngredient = Type.None;
        cheese.SetActive(false);
        fig.SetActive(false);
        salami.SetActive(false);
        ketchup.SetActive(false);
        mushroom.SetActive(false);
        olives.SetActive(false);
        onion.SetActive(false);
    }
    
}