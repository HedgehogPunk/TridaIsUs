using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YouManager : MonoBehaviour
{

    public static UnityEvent StepEvent = new UnityEvent();


    public List<Code> yous = new List<Code>();

    public float waitforSecondsFloat = 0.17f; // чем меньше тем быстрее

    InputMaster inputs;

    public bool canMove;

    public bool waiting;




    private void Awake()
    {
         inputs = new InputMaster();
         inputs.Enable();

        Rules.RuleDiactivateEvent.AddListener(ClearYous);
        Rules.CanMoveEvent.AddListener(CanMove);

        canMove = true;

    }

    void ClearYous()
    {
        

        yous.Clear();
    }
    void CanMove()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!waiting && canMove)
        {









            if (inputs.Movement.ForwardMove.inProgress || inputs.Movement.BackMove.inProgress ||
                inputs.Movement.RightMove.inProgress || inputs.Movement.LeftMove.inProgress ||
                inputs.Movement.UpMove.inProgress || inputs.Movement.DownMove.inProgress)

            {

               

                Rules.AddTurnEvent.Invoke();

                waiting = true;

                List<Code> oldYous = yous;


                foreach (Code you in oldYous)
                {
                    if (you != null)
                    {

                        isYou y = you.GetComponent<isYou>();

                        if (inputs.Movement.ForwardMove.inProgress)
                        {
                            y.Move(Direction.forward, new Vector3(0, 0, 1));


                        }
                        if (inputs.Movement.BackMove.inProgress)
                        {
                            y.Move(Direction.back, new Vector3(0, 0, -1));


                        }
                        if (inputs.Movement.RightMove.inProgress)
                        {
                            y.Move(Direction.right, new Vector3(1, 0, 0));


                        }
                        if (inputs.Movement.LeftMove.inProgress)
                        {
                            y.Move(Direction.left, new Vector3(-1, 0, 0));
                        }

                        if (inputs.Movement.UpMove.inProgress && y.GetComponent<isFly>().fly)
                        {
                            y.Move(Direction.up, new Vector3(0, 1, 0));
                        }

                        if (inputs.Movement.DownMove.inProgress && y.GetComponent<isFly>().fly)
                        {
                            y.Move(Direction.down, new Vector3(0, -1, 0));
                        }
                    }

                }



                NextTurn();




            }
        }
    }

    public void NextTurn()
    {
        StepEvent.Invoke();

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        canMove = false;


        
        Rules.TriggerHolderEvent.Invoke();
        Rules.TriggerScriptEvent.Invoke();


        //Rules.Wait();       // если здесь то       не читаются тексты

        yield return new WaitForSeconds(waitforSecondsFloat);

        Rules.DeathEvent.Invoke();

        Rules.RuleDiactivateEvent.Invoke();

        Rules.TextBlockEvent.Invoke();


        Rules.nextTurnEvent.Invoke();

        Rules.CanMoveEvent.Invoke();

        Rules.TriggerScriptEvent.Invoke();


        //Rules.Wait();       //  если здесь то       персонаж проходит через всё   Если оба то этот

        waiting = false;



        /* Solution
        Rules.DefeatEvent.Invoke();


        Rules.RuleDiactivateEvent.Invoke();
        Rules.TriggerHolderEvent.Invoke();
        Rules.TriggerScriptEvent.Invoke();


        WAITFORSECONDS

        Rules.TextBlockEvent.Invoke();


        Rules.nextTurnEvent.Invoke();

        Rules.CanMoveEvent.Invoke();

        Rules.TriggerScriptEvent.Invoke();



         */
    }



}
