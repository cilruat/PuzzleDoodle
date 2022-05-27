using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObjectBase : MonoBehaviour
{    
    public abstract ELevelObjectType Type { get; }

    private bool _isDelay = false;

    public bool IsDelay => _isDelay;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Runner runner = collision.GetComponent<Runner>();

        if (runner == null)
            return;

        OnEnter(runner);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Runner runner = collision.GetComponent<Runner>();

        if (runner == null)
            return;

        OnExit(runner);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {        
        Runner runner = collision.gameObject.GetComponent<Runner>();

        if (runner == null)
            return;

        OnExit(runner);
    }

    public abstract void OnEnter(Runner runner);

    public virtual void OnExit(Runner runner) { }

    public virtual void OnInteraction() { }

    public void StartInteractionDelay()
    {
        StartCoroutine("DelayProc");
    }

    IEnumerator DelayProc()
    {
        _isDelay = true;

        yield return new WaitForSeconds(Define.OBJECT_INTERACTION_DELAY);

        _isDelay = false;
    }
}
