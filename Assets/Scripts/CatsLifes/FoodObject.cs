using UnityEngine;

public class FoodObject : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Animal animal = other.GetComponent<Animal>();

        if (animal != null)
        {
            animal.TocarComida();
        }
    }
}
