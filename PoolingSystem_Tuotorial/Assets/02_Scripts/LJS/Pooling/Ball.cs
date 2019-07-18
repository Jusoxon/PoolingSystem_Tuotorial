using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global_define;

public class Ball : MonoBehaviour,IPoolObject
{
    public Rigidbody rb;

    public void OnSpawnObject(Vector3 _direction)
    {
        transform.position = _direction;
        gameObject.SetActive(true);
        StartCoroutine(ActiveFalseTime(3));
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator ActiveFalseTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        this.gameObject.SetActive(false);
    }
}
