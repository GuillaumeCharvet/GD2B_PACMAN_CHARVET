using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Originator : MonoBehaviour
{
    [SerializeField]
    private GameObject memento;
    [SerializeField]
    private Transform trsfParent;

    // ***** STATE *****
    [SerializeField]
    private Transform trsfPACMAN;

    public Memento Save(float t)
    {
        var curMemento = Instantiate(memento, Vector3.zero, Quaternion.identity, trsfParent);
        curMemento.name = "Memento " + t;
        return curMemento.GetComponent<Memento>();
    }

    public void Restore(Memento memento)
    {
        trsfPACMAN.position = memento.GetState()[0];
    }
}
