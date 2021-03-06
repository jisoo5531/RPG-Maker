using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    private MovingObj thePlayer;

    public Transform target;            // 같은 씬 내에서 이동을 위한 변수 (씬 전환할거라면 필요 없음)
    public BoxCollider2D targetBound;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP, DOWN, LEFT, RIGHT")]
    public string direction;    // 캐릭터가 바라보고 있는 방향
    private Vector2 vector;     // getfloat("dirX")

    [Tooltip("문이 있으면 : true, 문이 없으면 : flase")]
    public bool door;           // 문이 있는지 여부

    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;

    // Start is called before the first frame update
    void Start()
    {
        
        theCamera = FindObjectOfType<CameraManager>();  // 같은 씬 내에서 이동을 위한 변수 (씬 전환할거라면 필요 없음)
        thePlayer = FindObjectOfType<MovingObj>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!door)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (door)
        {
            if(collision.gameObject.name == "Player")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    vector.Set(thePlayer.animatior.GetFloat("DirX"), thePlayer.animatior.GetFloat("DirY"));
                    switch (direction)
                    {
                        case "UP":
                            if (vector.y == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "DOWN":
                            if (vector.y == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "LEFT":
                            if (vector.x == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        default:
                            StartCoroutine(TransferCoroutine());
                            break;
                    }
                    StartCoroutine(TransferCoroutine());

                }
            }
        }
    }
    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theFade.FadeOut();

        if (door)
        {
            anim_1.SetBool("Open", true);
            if (door_count == 2)
                anim_2.SetBool("Open", true);
        }
        yield return new WaitForSeconds(0.3f);

        theOrder.SetTransparent("Player");
        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
                anim_2.SetBool("Open", false);
        }

        yield return new WaitForSeconds(0.7f);
        theOrder.SetUnTransparent("Player");

        theCamera.SetBound(targetBound);

        // 같은 씬 내 맵 이동
        thePlayer.transform.position = target.transform.position;
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move();
    }
}
