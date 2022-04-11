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
    private Transform trsfPACMAN, trsfFANTOM1, trsfFANTOM2, trsfFANTOM3, trsfFANTOM4, trsfFANTOM5;

    public Memento Save(float t)
    {
        var curMemento = Instantiate(memento, Vector3.zero, Quaternion.identity, trsfParent);
        curMemento.name = "Memento " + t;
        return curMemento.GetComponent<Memento>();
    }

    public void Restore(Memento memento)
    {
        trsfPACMAN.position = memento.GetState()[0];
        trsfFANTOM1.position = memento.GetState()[1];
        trsfFANTOM2.position = memento.GetState()[2];
        trsfFANTOM3.position = memento.GetState()[3];
        trsfFANTOM4.position = memento.GetState()[4];
        trsfFANTOM5.position = memento.GetState()[5];
    }
}
