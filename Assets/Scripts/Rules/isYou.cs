using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isYou : Rule
{
  

    public bool you;

    bool moved;

    public bool moving;

    public GameObject directionArrows;


    private void Awake()
    {
        Rules.RuleDiactivateEvent.AddListener(DisActivate);

        Rules.CanMoveEvent.AddListener(DirArrows);

        Settings.DirArrowsEvent.AddListener(DirArrows);
    }


    public void DirArrows()
    {
        if (you)
        {

            if (Settings.DirectionArrows)
            {
                directionArrows.SetActive(true);
            }
            else
            {
                directionArrows.SetActive(false);
            }
        }
        else
        {
            directionArrows.SetActive(false);
        }
    }

    public override void Activate()
    {
        base.Activate();

        moved = false;

        you = true;


        if(Settings.DirectionArrows)
        {
            directionArrows.SetActive(true);
        }
    }

    public override void DisActivate()
    {
        base.DisActivate();

        moved = false;


        you = false;

        moving = false;
    }


    public bool IsMove(Direction direction, Vector3 vector)
    {

        if (GetComponent<Code>().th.directions[direction] == false)
        {

            return true;
        }
        else
        {

            Collider[] colliders = GetComponent<Code>().th.triggersGameObjects[direction].GetComponent<TrigerScript>().colliders;

            

            foreach (Collider collider in colliders)
            {
                isPush push;

                if(collider.TryGetComponent(out push))
                {

                    if (!collider.GetComponent<isYou>().you) //  не ты
                    {

                        if (!push.push) // не толкается
                        {

                            if (collider.GetComponent<isShut>().shut && GetComponent<isOpen>().open 
                                || collider.GetComponent<isOpen>().open && GetComponent<isShut>().shut)
                            {

                            }
                            else
                            {
                                return false;
                            }

                        }

                        if ((direction == Direction.up || direction == Direction.down) && collider.GetComponent<isFly>().fly == false) // не может двигаться вверх или низ
                        {
                            return false;
                        }
                    }

                }
                else
                {
                    return false;
                }

            }

            List<Collider> pushableColliders = GetPushableCollider(direction);

            foreach(Collider col in pushableColliders)
            {
                if(col.GetComponent<isYou>().IsMove(direction, vector) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public List<Collider> GetPushableCollider(Direction direction)
    {
        Collider[] colliders = GetComponent<Code>().th.triggersGameObjects[direction].GetComponent<TrigerScript>().colliders;

        List<Collider> pushColliders = new List<Collider>();

        foreach (Collider collider in colliders)
        {
            isPush push;

            if (collider.TryGetComponent(out push))
            {
                if (push.push)
                {
                    if ( ! (push.not && you == false))
                    {
                        pushColliders.Add(collider);
                    }
                }
            }

        }

        return pushColliders;

    }


    public void Move(Direction direction, Vector3 vector)
    {
        

        if (moved)
        {
            return;
        }

        transform.parent.rotation = Quaternion.LookRotation(vector);

        moved = true;



        if (IsMove(direction,vector))
        {
            List<Collider> colliders = GetPushableCollider(direction);

            foreach (Collider collider in colliders)
            {

                collider.GetComponent<isYou>().Move(direction, vector);
            }



            transform.parent.position += vector;


        }
        else if(moving)
        {
            moved = false;

            moving = false;

            GameBlock gb = transform.parent.GetComponent<GameBlock>();

            Move(gb.Opposites(direction), gb.DirToVector(gb.Opposites(direction)));
        }

    }
}
