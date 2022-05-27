using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Runner runner = collision.GetComponent<Runner>();

        if (runner == null)
            return;

        runner.Die();
    }
}
