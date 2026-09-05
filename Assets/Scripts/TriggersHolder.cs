using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersHolder : MonoBehaviour
{
    public Dictionary<Direction, bool> directions = new Dictionary<Direction, bool>();
    public Dictionary<Direction, GameObject> triggersGameObjects = new Dictionary<Direction, GameObject>();
    public GameObject Tforward, Tback, Tright, Tleft, Tup, Tdown;

    public bool f,b,u,d,r,l;

    public Code code;

    private void Awake()
    {
        Rules.TriggerHolderEvent.AddListener(UpdateDirections);

        Rules.CanMoveEvent.AddListener(UpdateDirections);


        UpdateDirections();

        triggersGameObjects[Direction.forward] = Tforward;
        triggersGameObjects[Direction.back] = Tback;
        triggersGameObjects[Direction.right] = Tright;
        triggersGameObjects[Direction.left] = Tleft;
        triggersGameObjects[Direction.up] = Tup;
        triggersGameObjects[Direction.down] = Tdown;
    }

    public void UpdateDirections()
    {

        transform.forward = Vector3.forward;
        transform.up = Vector3.up;

        //transform.rotation = new Quaternion(0,0,0,0);


        directions[Direction.forward] = false;
        directions[Direction.back] = false;
        directions[Direction.right] = false;
        directions[Direction.left] = false;
        directions[Direction.up] = false;
        directions[Direction.down] = false;


        r = l = u = d = b = f = false;
    }

    public void TrigerEnter(GameObject triger)
    {
        if(triger == Tforward)
        {
           f = directions[Direction.forward] = true;


        }

        if (triger == Tback)
        {
            b= directions[Direction.back] = true;

        }

        if (triger == Tright)
        {
           r= directions[Direction.right] = true;


        }

        if (triger == Tleft)
        {
           l= directions[Direction.left] = true;

        }

        if (triger == Tup)
        {
          u=  directions[Direction.up] = true;


        }

        if (triger == Tdown)
        {
           d= directions[Direction.down] = true;
        }

    }


   

   
}

public enum Direction
{
    forward,
    back,
    right,
    left,
    up,
    down
}
