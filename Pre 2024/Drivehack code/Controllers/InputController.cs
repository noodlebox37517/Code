using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    //TODO CLEANUP AND REMOVAL UNUSED VARIABLES
    private bool acceptInput = true;

    public bool useMouse = true;
    public bool padStyle = false;
    public bool swipemove = true;
    public float maxPadDist = 1f;
    public float minPadDist = .1f;
    public GameObject Head;

    public Bounds inputBounds;
    public GameObject dragonZone;
    private Bounds movementBounds;

    public bool moveY = true;
    public Vector2 targetpos = new Vector2();
    public float moveSpeed = 1f;

    public Vector2 botLeftInputBound;
    public Vector2 topRightInputBound;
    private Touch trackedTouch;
    private Vector2 touchStart;
    /// <summary>
    /// 0 is left most max is right most
    /// </summary>
    /// 

    public Vector3[] movePositions;
    public bool useQue = true;
    public enum Inp { Left = 0, Right = 1 }
    public Stack<Inp> inpQue = new Stack<Inp>();

    public int column = 3;
    public float[] heightPositions;
    public int centralPosID =1;
    public int currentVertPosID =1;
    public int currentPosID = 1;
    public float colomnGap =1f;
    [Header("Jump variables")]
    public float jumpHieght = 1f;
    public float jumpLength = 1f;
    private bool jumping = false;
    // go off leveltime not unity time
    private float jumpstart = 0f;
    private bool endJump;
    private bool sliding = false;
    public float gravity = -1f;
    public float jumpVel = 1f;
    public float endJumpAccelMod = 1.5f;
    [Header("Block variables")]
    public float blockTime = 1f;

    public float speedmod = 1f;
    public float _translateSpeed = 15f;
    public float TranslateSpeed
    {
        get
        {
            return _translateSpeed * speedmod;
        }
        set
        {
            _translateSpeed = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        movePositions = new Vector3[column];
        Vector3 centrepos = GameMaster.instance.dragon.transform.position;
        for (int i = 0; i<column ;i++)
        {
            Vector3 tempos = centrepos;
            tempos.x = centrepos.x + (i - (column - column % 2) / 2) * colomnGap;
            movePositions[i].x = tempos.x;
            movePositions[i].y = GameMaster.instance.dragon.transform.position.y;
        }
        heightPositions[1] = movePositions[1].y;
        movementBounds = dragonZone.GetComponent<Collider>().bounds;
        //set camera offset
        GameMaster.instance.CamC.offset = (movePositions[0].x - movePositions[1].x) *.75f ;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameMaster.instance.LC.LevelPlay)
        {
            if (swipemove)
            { 
                    if (useQue) 
                        {
                            GetInput();
                            PopInput();
                        }
                    else 
                        SwipeMove();
            }
            else
                Move();
        }

    }

    public void MoveHead(Vector2 newpos)
    {
        Vector2 dir = newpos - (Vector2)Head.transform.position;

        Head.transform.position += (Vector3)dir * moveSpeed;

    }
   

    public void MoveTowards(Vector2 targetpos)
    {
        float speedvar = (((Vector2)movementBounds.center - targetpos ).magnitude /maxPadDist);
        if (((Vector2)movementBounds.center - targetpos).magnitude<= minPadDist)
        {
            speedvar = 0f;
        }
        else if (speedvar > 1f)
        {
            speedvar = 1f;
        }
        Vector3 newpos = Head.transform.position - (Vector3)((Vector2)movementBounds.center - targetpos).normalized * moveSpeed * speedvar * Time.deltaTime;
        newpos.y = Mathf.Min(newpos.y, movementBounds.max.y);
        if (movementBounds.Contains(newpos))
        {
            Head.transform.position = newpos;
        }
       
        // check if target pos within bounds of movement, else give closest point in bounds
    }
    public void PopInput()
    {
        if (inpQue.Count > 0)
        {
            Inp tarinp = inpQue.Pop();

            switch (tarinp)
            {
                case Inp.Left:
                    Horizontal(-1);
                    break;
                case Inp.Right:
                    Horizontal(1);
                    break;
                default:
                    break;
            }
        } 
    }
    public void QueInput(Inp inp)
    {
        // check stack not full
        if (inpQue.Count < 2)
        {
            inpQue.Push(inp);
        }
        else
        {
            Debug.Log("Que full");
        }

        // ad input to stack
    }
    public void GetInput()
    {
        //TOUCH
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            GameMaster.instance.UI.notifactionText.text = Input.touchCount.ToString();

            if (touch.phase == TouchPhase.Began)
            {

                trackedTouch = touch;
                touchStart = touch.position;
            }
            else
            {
                // continue track
                if (touch.phase == TouchPhase.Ended)
                {
                    Vector2 swipedir = (touch.position - touchStart).normalized;
                    //take start and end get direction
                    GameMaster.instance.UI.notifactionText.text = swipedir.ToString();
                    Swiped(swipedir);
                }
            }
        }
        //KEYBOARD
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //todo add better exit/save/unload//back to menu
            //if level live exit level to menu, else exit application
            if (GameMaster.instance.LC.LevelPlay)
            {
                GameMaster.instance.LC.LevelPlay = false;
                GameMaster.instance.LevelExit();
            }
            else
            {
                Application.Quit();
            }

        }
        if (Input.GetKeyDown("a"))
        {
            if (acceptInput)
                QueInput(Inp.Left);
        }
        if (Input.GetKeyDown("d"))
        {
            if (acceptInput)
                QueInput(Inp.Right);
        }
        if (Input.GetKeyDown("space"))
        {
            if (acceptInput)
                QueInput(Inp.Right);
        }
        if (Input.GetKeyDown("s"))
        {
            if (jumping)
            {
                //forces jump to end
                //endJump = true;
            }
            else
                if (acceptInput)
                QueInput(Inp.Right);
        }
    }
    public void SwipeMove()
    {
        if (jumping)
        {
            float curtime = GameMaster.instance.LC.levelTime - jumpstart;
            //float x = (GameMaster.instance.LC.levelTime - jumpstart)* GameMaster.instance.LC.level.levelSpeed;
            //float y =-(.5f*x*x)  + jumpHieght;
            //float a = -(jumpHieght * 2) / jumpLength;
            //float d = (.5f * -a * jumpLength) * curtime + .5f * a * curtime * curtime;
            float a = gravity;
            if (endJump)
                a *= 2;
            float d = jumpVel * curtime + .5f * a * curtime * curtime;


            Vector3 temppos = GameMaster.instance.dragon.gameObject.transform.position;
            temppos.y = movePositions[currentPosID].y + d;
            GameMaster.instance.dragon.gameObject.transform.position = temppos;
            if(d <= 0)
            {
                endJump = false;
                jumping = false;
                acceptInput = true;
                GameMaster.instance.dragon.gameObject.transform.position = movePositions[currentPosID];
            }

        }

            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);
                GameMaster.instance.UI.notifactionText.text = Input.touchCount.ToString();

                if (touch.phase == TouchPhase.Began)
                {

                    trackedTouch = touch;
                    touchStart = touch.position;
                }
                else
                {
                    // continue track
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Vector2 swipedir = (touch.position - touchStart).normalized;
                        //take start and end get direction
                        GameMaster.instance.UI.notifactionText.text = swipedir.ToString();
                        Swiped(swipedir);
                    }
                }
            }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //todo add better exit/save/unload//back to menu
            //if level live exit level to menu, else exit application
            if (GameMaster.instance.LC.LevelPlay)
            {
                GameMaster.instance.LC.LevelPlay = false;
                GameMaster.instance.LevelExit();
            }
            else
            {
            Application.Quit();
            }
            
        }
        if (Input.GetKeyDown("a"))
            {
            if (acceptInput)     
                Horizontal(-1);
            }
            if (Input.GetKeyDown("d"))
            {
            if (acceptInput)
                Horizontal(1);
            }
            if (Input.GetKeyDown("space"))
            {
            if (acceptInput)
                Jump();
            }
            if (Input.GetKeyDown("s"))
            {
            if (jumping)
            {
                //forces jump to end
                //endJump = true;
            }
            else
                if (acceptInput)
                Ability();
            }

            //change to que
     
        
    }
    public void Horizontal(int value)
    {
        if (!sliding)
        {
            sliding = true;
            // left or right swipe
          int   newcurrentPosID = Mathf.Clamp(currentPosID + value, 0, movePositions.Length - 1);
            if(currentPosID!= newcurrentPosID)
                GameMaster.instance.dragon.Slide(value);
            currentPosID = newcurrentPosID;
            //GameMaster.instance.UI.notifactionText.text = currentPosID.ToString();
            Vector3 tempos = movePositions[currentPosID];
            tempos.y = heightPositions[currentVertPosID];
            //start couroutine

            //TODO calce speed , refuse extra input
            float Speed = TranslateSpeed;

            StartCoroutine(TranslateMove(movePositions[currentPosID], TranslateSpeed, GameMaster.instance.dragon.gameObject.transform));
            //move camera
            GameMaster.instance.CamC.MoveCam(currentPosID);
        }
        else
        {
            Debug.Log("already sliding");
        }
    }

    IEnumerator TranslateMove(Vector3 end, float speed, Transform form)
    {
        while (Vector3.Distance(form.position, end) > speed * Time.deltaTime)
        {
            form.position = Vector3.MoveTowards(form.position, end, speed * Time.deltaTime);
            yield return 0;
        }
        form.position = end;
        sliding = false;

    }
    public void Swiped(Vector2 dir)
    {
        if (acceptInput)
        {
            if (dir.x > .5f || dir.x < -.5f)
            {
                if (useQue)
                {
                    if (dir.x > .5f)
                    {
                        QueInput(Inp.Right);
                    }
                    else
                    {
                        QueInput(Inp.Left);
                    }
                }
                else
                Horizontal((int)Mathf.Sign(dir.x));
                //swipe horrizontal
                //NOTE this seams overly complex for function already checking in if above

            }
            else if (dir.y > .8f || dir.y < -.8f)
            {
                //move vertical
                int swipevalue = Mathf.Clamp(1 + (int)Mathf.Sign(dir.y), 0, 2);
                //GameMaster.instance.UI.notifactionText.text = swipevalue.ToString();
                if (swipevalue == 2)
                {
                    //insert vertical que
                    if (useQue)
                    { }
                    else
                        Jump();
                }
                else
                {

                    //use ability TODO
                    if (useQue)
                    { }
                    else
                        Ability();
                }
            }
        }
        else if (dir.y < -.8f)
        {
            if (jumping)
            {
                //endJump = true;
            }
        }
    }
    public void Ability()
    {
        //move to scriptable object.
        if (!GameMaster.instance.dragon.invulnerable)
            if (GameMaster.instance.DragonAbility.AbilityUsable())
            {
                GameMaster.instance.DragonAbility.UseAbility();
            }
            //if (GameMaster.instance.dragon.UseEnergy(50))
            //{
            //    //check what active ability is
            //    //call use from ability
            //    StartCoroutine(Invul());
            //}
            else
            {
                GameMaster.instance.UI.notifactionText.text = "no energy";
            }
        
        

        
    }

    public void Jump()
    {
        jumping = true;
        acceptInput = false;
        jumpstart = GameMaster.instance.LC.levelTime;
    }
    public void  Move()
    {
        Vector2 mousepos = new Vector2(Input.mousePosition.x / GameMaster.instance.cam.scaledPixelWidth, Input.mousePosition.y / GameMaster.instance.cam.scaledPixelHeight);
        // Debug.Log(mousepos);
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            print(Input.touchCount);
            //check actions
            //foreach touch
            // if touch is within move bounds move head
            Vector2 temppos = new Vector2(touch.position.x / GameMaster.instance.cam.scaledPixelWidth, touch.position.y / GameMaster.instance.cam.scaledPixelHeight);

            GameMaster.instance.UI.notifactionText.text = temppos.ToString();
            if (inputBounds.Contains(temppos))
            {
                GameMaster.instance.UI.notifactionText.text = "is contained";
                float posy = temppos.y * (1 / topRightInputBound.y);

                    //Debug.Log(posy);
                    posy = Mathf.Min(posy, 1);
                float newx = (movementBounds.center.x - movementBounds.extents.x) + temppos.x * movementBounds.size.x;
                float newy = (movementBounds.center.y - movementBounds.extents.y) + posy * movementBounds.size.y;
                if (!moveY)
                newy = targetpos.y;
                Vector2 newpos = new Vector2(newx, newy);
                targetpos = newpos;
            
                GameMaster.instance.UI.notifactionText.text = targetpos.ToString();
                if (!padStyle)
                {
                    MoveHead(newpos);
                }
                else
                {
                    MoveTowards(newpos);
                }
            }
        }
        //else if (Input.touchCount > 1)
        //{
        //    //for (var i = 0; i < Input.touchCount; ++i)
        //    //{
        //    //    if (Input.GetTouch(i).phase == TouchPhase.Began)
        //    //    {
        //    //        if (Input.GetTouch(i).tapCount == 2)
        //    //        {
        //    //            // double tap :)
        //    //        }
        //    //    }
        //    //}
        //}
        else if (useMouse)
        {
            Vector2 temppos = mousepos;
            //check actions
            //foreach touch 
            //if touch is within move bounds move head
            GameMaster.instance.UI.notifactionText.text = temppos.ToString();
            if (inputBounds.Contains(temppos))
            {
                //Debug.Log(temppos);
                //apply max input


                float posy = temppos.y * (1 / topRightInputBound.y);
                //Debug.Log(posy);
                posy = Mathf.Min(posy, 1);
                float newx = (movementBounds.center.x - movementBounds.extents.x) + temppos.x * movementBounds.size.x;
                float newy = (movementBounds.center.y - movementBounds.extents.y) + posy * movementBounds.size.y;
                if (!moveY)
                    newy = targetpos.y;
                Vector2 newpos = new Vector2(newx, newy);

                targetpos = newpos;
               // GameMaster.instance.UI.notifactionText.text = targetpos.ToString();
                if (!padStyle)
                {
                    MoveHead(targetpos);
                }
                else
                {
                    MoveTowards(targetpos);
                }
            }

            //if (DoubleClick())
            //{
            //    Debug.Log("double click");
            //    ability.Use();
            //}

        }

        
    }
}
