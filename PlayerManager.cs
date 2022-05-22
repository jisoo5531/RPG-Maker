using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObj
{
    static public PlayerManager instance;

    public string currentMapName;       // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장
    public string currentSceneName;
    
    
    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    private bool canMove = true;
    public bool notMove = false;

    private bool attacking = false;
    public float attackDelay;
    private float currentAttackDelay;


    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        boxCollider = GetComponent<BoxCollider2D>();
        theAudio = FindObjectOfType<AudioManager>();
        animatior = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove && !attacking)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // 상,하 입력 중 좌,우가 동시에 입력되지 않게끔
            if (vector.x != 0)
                vector.y = 0;

            animatior.SetFloat("DirX", vector.x);
            animatior.SetFloat("DirY", vector.y);

            bool checkColisionFlag = base.CheckColision();
            if (checkColisionFlag)
                break;

            animatior.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            boxCollider.offset = new Vector2(vector.x * 0.01f * speed * walkCount, vector.y * 0.01f * speed * walkCount);
            

            while (currentWalkCount < walkCount)
            {
                // 좌, 우
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                // 상, 하
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                // shift키가 눌렸을 경우, 한 타일씩 움직이게 하기 위해
                // 이 코드가 없을 시, 두 타일씩 움직임
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                if (currentWalkCount == 12)
                    boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);

            }
            currentWalkCount = 0;

        }
        animatior.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && !notMove && !attacking)
        {
            // 상하좌우 방향키가 눌렸을 경우
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        if(!notMove && !attacking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentAttackDelay = attackDelay;
                attacking = true;
                animatior.SetBool("Attacking", true);
            }
        }
        if (attacking)
        {
            currentAttackDelay -= Time.deltaTime;
            if(currentAttackDelay <= 0)
            {
                animatior.SetBool("Attacking", false);
                attacking = false;
            }
        }
    }
}
