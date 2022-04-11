using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caretaker : MonoBehaviour
{

    [SerializeField]
    private List<Memento> history;
    [SerializeField]
    private Originator originator;
    [SerializeField]
    private SpriteRenderer filtre;

    private bool doitEnregistrer = true;

    private float dt = 0.025f, time = 0f, totalTime = 0f;

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

    public bool GetEnregistre() { return doitEnregistrer; }

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
        filtre.color = new Color(filtre.color.r, filtre.color.g, filtre.color.b, 0.3f); 
    }
}
