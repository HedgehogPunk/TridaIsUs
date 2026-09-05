using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlock : MonoBehaviour
{
    public string obj;

    public Code code;

    public bool spawnedOnThisTurn = false;

    public string prefab;

   

  

    private void Start()
    {
        spawnedOnThisTurn = false;

       
    }

    public Direction Opposites(Direction direction)
    {
        if(direction == Direction.forward)
        {
            return Direction.back;
        }
        if (direction == Direction.back)
        {
            return Direction.forward;
        }
        if (direction == Direction.right)
        {
            return Direction.left;
        }
        if (direction == Direction.left)
        {
            return Direction.right;
        }
        if (direction == Direction.up)
        {
            return Direction.down;
        }
        if (direction == Direction.down)
        {
            return Direction.up;
        }

        return Direction.back;
    }

    public Vector3 DirToVector(Direction direction)
    {
        if (direction == Direction.forward)
        {
            return Vector3.forward;
        }
        if (direction == Direction.back)
        {
            return Vector3.back;
        }
        if (direction == Direction.right)
        {
            return Vector3.right;
        }
        if (direction == Direction.left)
        {
            return Vector3.left;
        }
        if (direction == Direction.up)
        {
            return Vector3.up;
        }
        if (direction == Direction.down)
        {
            return Vector3.down;
        }

        return Vector3.forward;
    }

    public Direction VectorToDir(Vector3 vector)
    {
        if(vector == Vector3.forward)
        {
            return Direction.forward;
        }
        if (vector == Vector3.back)
        {
            return Direction.back;
        }
        if (vector == Vector3.right)
        {
            return Direction.right;
        }
        if (vector == Vector3.left)
        {
            return Direction.left;
        }
        if (vector == Vector3.up)
        {
            return Direction.up;
        }
        if (vector == Vector3.down)
        {
            return Direction.down;
        }

        return Direction.forward;
    }

}
