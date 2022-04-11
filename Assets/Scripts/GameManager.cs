using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int difficulty = 3;

    public void MoinsDifficile()
    {
        if (difficulty > 1)
        {
            difficulty--;
            var fantom = GameObject.Find("FANTOM" + (difficulty + 1));
            fantom.SetActive(false);
        }
    }
        public void PlusDifficile()
    {
        if (difficulty < 5)
        {
            difficulty++;
            var fantom = GameObject.Find("FANTOM" + (difficulty));
            fantom.SetActive(true);
        }
    }

    /*
    public interface Strategy
    {
        object DoAlgorithm(object data);
    }

    // Concrete Strategies implement the algorithm while following the base
    // Strategy interface. The interface makes them interchangeable in the
    // Context.
    class ConcreteStrategyA : Strategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : Strategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }*/
}
