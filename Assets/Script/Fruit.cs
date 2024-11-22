using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    bool canMove;
    float speed;
    bool right;
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
        canMove = true;
    }
    private void Update()
    {
        if (!canMove)
            return;
        //transform.Translate(-transform.forward * speed * 2 * Time.deltaTime);
    }
    public bool Right
    {
        get { return right; }
        set { right = value; }
    }
}
