using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuysController : MonoBehaviour
{
    [Header("Public variables")]
    public int nbOfDir = 4;
    public float changingDirectionDelay = 1;
    public float stepRange = 1;
    public float moveSpeed = 1;

    [Header("Serializable variables")]
    [SerializeField] Vector2 startingPos = new Vector2(0, 0);

    [Header("Objects to serialize")]
    [SerializeField] GameObject myVCam = default;
    [SerializeField] GameObject myCanvas = default;
    [SerializeField] Image dirFb = default;
    [SerializeField] Sprite[] difDir = default;

    [Header("My components")]
    [SerializeField] SpriteRenderer myRend = default;

    //Hidden public
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool hasPlayed = false;

    //Private
    bool canChangeDirection = true;
    int direction = 0;
    bool isOnMoving = false;
    Vector2 dirVector = new Vector2(0, 0);
    Vector2 targetPos = new Vector2(0, 0);
    bool smthgInDir = false;

    private void Start()
    {
        transform.position = startingPos;

        dirVector = new Vector2(Mathf.Cos(0), Mathf.Sin(0));
        targetPos = new Vector2(transform.position.x + dirVector.x * stepRange, transform.position.y + dirVector.y * stepRange);
    }

    private void Update()
    {
        if (canMove)
        {
            ColDetection();
            DirectionFeedback();

            if (canChangeDirection)
            {
                StartCoroutine(WaitBeforeChangingDirection());
                canChangeDirection = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (smthgInDir)
                {
                    //add NOPE sound
                }
                else
                {
                    StopAllCoroutines();
                    canMove = false;
                    isOnMoving = true;
                    hasPlayed = true;
                }
            }
        }
        else
        {
            if (isOnMoving)
            {
                FaisleMouv();
            }
        }
    }

    private void FaisleMouv()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetPos)
        {
            canChangeDirection = true;
            isOnMoving = false;
        }
    }

    private void ColDetection()
    {
        RaycastHit2D detection = Physics2D.Raycast((Vector2)transform.position + dirVector * 0.5f, dirVector, stepRange);

        if (detection.collider)
        {
            smthgInDir = true;
        }
        else
        {
            smthgInDir = false;
        }
    }

    private void DirectionFeedback()
    {
        if (smthgInDir)
        {
            dirFb.sprite = difDir[4];
        }
        else
        {
            dirFb.sprite = difDir[direction];
        }
    }

    public void GuyActivation(bool upordown)
    {
        canMove = upordown;
        myCanvas.SetActive(upordown);
        myVCam.SetActive(upordown);

        if (upordown)
        {            
            canChangeDirection = true;
            targetPos = new Vector2(transform.position.x + dirVector.x * stepRange, transform.position.y + dirVector.y * stepRange);
        }
    }

    IEnumerator WaitBeforeChangingDirection()
    {
        yield return new WaitForSeconds(changingDirectionDelay);

        if (direction == nbOfDir - 1)
        {
            direction = 0;
        }
        else
        {
            direction += 1;
        }

        dirVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (360 / nbOfDir * direction)), Mathf.Sin(Mathf.Deg2Rad * (360 / nbOfDir * direction)));
        targetPos = new Vector2(transform.position.x + dirVector.x * stepRange, transform.position.y + dirVector.y * stepRange);

        canChangeDirection = true;
        StopCoroutine(WaitBeforeChangingDirection());
    }
}