using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 i_movement;
    [SerializeField]
    float moveSpeed = 10.0f;

    [SerializeField]
    GameObject[] hands;

    [SerializeField]
    GameObject[] bears;

    public int playerId;

    public bool iswhite = false;
    public bool isblack = false;
    public bool isdouble = false;

	public float pressTh = 0.01f;

    string pressedText="Pad";


    Vector2 blackhand;
    Vector2 whitehand;

    public float minX0,maxX0, minY0, maxY0;
    public float minX1, maxX1, minY1, maxY1;

    public float BellyRange=3f;

    Animator whitebearAnim;
    Animator brownbearAnim;

     enum ShitType
    {
        Vanilla,
        Choco,
        Both
    };

    ShitType shitType;

    float ph1, ph2, pv1, pv2;
    float d1, d2;
    private void Awake()
    {
        blackhand = hands[0].transform.position;
        whitehand = hands[1].transform.position;
    }
    void Start()
    {
        ph1 = ph2 = pv1 = pv2 = d1 = d2 = 0f;
        brownbearAnim = bears[0].GetComponent<Animator>();
        whitebearAnim = bears[1].GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {

        HandMove();
       

        hands[0].transform.position = new Vector2(Mathf.Clamp(hands[0].transform.position.x, minX0, maxX0), 
            Mathf.Clamp(hands[0].transform.position.y, minY0, maxY0));

        hands[1].transform.position = new Vector2(Mathf.Clamp(hands[1].transform.position.x, minX1, maxX1),
         Mathf.Clamp(hands[1].transform.position.y, minY1, maxY1));


        float h1 = Input.GetAxis("RightJoystickH1");
        float v1 = Input.GetAxis("RightJoystickV1");
        float h2 = Input.GetAxis("RightJoystickH2");
        float v2 = Input.GetAxis("RightJoystickV2");

        Vector2 moveDir1 =  new Vector2(h1 - ph1, v1 - pv1);
        d1 = moveDir1.magnitude;
        Vector2 moveDir2 = new Vector2(h2 - ph2, v2 - pv2);
        d2 = moveDir2.magnitude;

		ph1 = h1;
		ph2 = h2;
		pv1 = v1;
		pv2 = v2;

        if(Input.GetButtonDown("A1"))
        {
            Debug.Log("A1 pressed");
        }
        if(Input.GetButtonDown("A2"))
        {
            Debug.Log("A2 pressed");
        }


        if(d2>pressTh)
        {
            WhiteBearPooping();
        }
        else
        {
            WhiteStopPooping();
        }

        if (d1>pressTh)
        {
            BrownBearPooping();
        }
        else
        {
            BrownStopPooping();
        }

        //下面的if还需要加蛋筒ready to be filled的validation
        if ((d1 > pressTh && Input.GetButtonDown("LeftBumper2") || d2 > pressTh && Input.GetButtonDown("LeftBumper1")))
		{
			if (!SoundManager.instance.sfxSource.isPlaying)
			{
				SoundManager.instance.PlaySFX("icecreamSquirt");
			}
		} else
		{
            if (SoundManager.instance.SFXinPlay() == "icecreamSquirt" && SoundManager.instance.sfxSource.isPlaying)
			{
				SoundManager.instance.sfxSource.Pause();
			}
		}

    }

    void HandMove()
    {
       
        Vector2 movement0 = new Vector2(Input.GetAxis("RightJoystickH1"), Input.GetAxis("RightJoystickV1")) * moveSpeed * Time.deltaTime;
        hands[0].transform.Translate(movement0);

        Vector2 movement1 = new Vector2(Input.GetAxis("RightJoystickH2"), Input.GetAxis("RightJoystickV2")) * moveSpeed * Time.deltaTime;
        hands[1].transform.Translate(movement1);

    }

    public bool Getkeydown(OrderType type)
    {
        if (type == OrderType.White)
        {
            return Input.GetButtonDown("LeftBumper1");
        }

        else if (type == OrderType.Black)
        {
            return Input.GetButtonDown("LeftBumper2");
        }

        else if (type == OrderType.Double)
        {
            return Input.GetButtonDown("LeftBumper1") &&Input.GetButtonDown("LeftBumper2");
        }
        else return (false); 

       // return Input.GetKeyDown(map[type]);
    }

    public bool GetKeyUp(OrderType type)
    {
        if (type == OrderType.White)
        {
            return Input.GetButtonUp("LeftBumper1");
        }

        else if (type == OrderType.Black)
        {
            return Input.GetButtonUp("LeftBumper2");
        }

        else if (type == OrderType.Double)
        {
            return Input.GetButtonUp("LeftBumper1") && Input.GetButtonUp("LeftBumper2");
        }
        else return (false);

    }

    public float GetRotateValue(int playerIdx)
    {
        if (playerIdx == 0)
        {
            return d1;
        }
        else {
            return d2;
        }
    }


	public void WhiteBearPooping()
    {
        whitebearAnim.SetBool("IsWhitePoop", true);
    }
    public void WhiteStopPooping()
    {
        whitebearAnim.SetBool("IsWhitePoop", false);
    }

    public void BrownBearPooping()
    {
         brownbearAnim.SetBool("IsBrownPoop", true);
    }
    public void BrownStopPooping()
    {
        brownbearAnim.SetBool("IsBrownPoop", false);
    }
}
