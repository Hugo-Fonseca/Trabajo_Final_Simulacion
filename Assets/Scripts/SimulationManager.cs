using UnityEngine;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    List<Animal> animales = new List<Animal>();

    public void RegisterAnimal(Animal a)
    {
        animales.Add(a);
    }

    public void RemoveAnimal(Animal a)
    {
        animales.Remove(a);
    }

    void FixedUpdate() // Aquí llamamos SIMULATE en vez de Update
    {
        float dt = Time.deltaTime;

        foreach (var a in animales)
        {
            a.Simulate(dt);
        }
    }
}
