using System;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

    void Start()
    {
        LeanTween.move(gameObject, transform.position + new Vector3(0, 0.5f, 0), 1f).setLoopPingPong(-1);
    }
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 30f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TemporaryHealth>().AddHealth(20);
            Destroy(gameObject);
        }
    }
}
