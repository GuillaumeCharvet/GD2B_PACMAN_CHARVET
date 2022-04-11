using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caretaker : MonoBehaviour
{

    [SerializeField]
    private List<Memento> history;
    [SerializeField]
    private Originator originator;

    private bool doitEnregistrer = true;

    private float dt = 0.05f, time = 0f, totalTime = 0f;

    void Update()
    {
        if (doitEnregistrer)
        {
            var previousTime = time;
            totalTime += Time.deltaTime;
            time += Time.deltaTime;
            if (time >= dt)
            {
                Remember();
                time -= dt;
            }
        }        
    }

    public void Remember()
    {
        Memento memento = originator.Save(totalTime);
        history.Add(memento);
    }

    public void GoBackToFirstMemento()
    {
        Memento memento = history[0];
        history.RemoveAt(0);
        originator.Restore(memento);
    }

    IEnumerator RegardeLeFilm()
    {
        while (history.Count > 0)
        {
            GoBackToFirstMemento();
            yield return new WaitForSeconds(dt);
        }
    }

    public void LanceLeFilm()
    {
        doitEnregistrer = false;
        StartCoroutine(RegardeLeFilm());
    }
}
