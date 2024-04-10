using UnityEngine;


[CreateAssetMenu(menuName = "_Data/PizzaOrder")]
public class PizzaOrderData : ScriptableObject {
    public PizzaController.Type first;
    public PizzaController.Type second;
    public PizzaController.Type third;
    public PizzaController.Type fourth;

}