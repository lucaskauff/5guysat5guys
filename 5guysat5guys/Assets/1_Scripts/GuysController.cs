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
    [SerializeField] Rigidbody2D myRb = default;

    //Hidden public
    [HideInInspector] public bool canMove = false;

    //Private
    bool canChangeDirection = true;
    int direction = 0;
    bool isOnMoving = false;
    Vector2 dirVector = new Vector2(0, 0);
    Vector2 targetVector = new Vector2(0, 0);

    private void Start()
    {
        transform.position = startingPos;

        dirVector = new Vector2(Mathf.Cos(0), Mathf.Sin(0));
        targetVector = new Vector2(transform.position.x + dirVector.x * stepRange, transform.position.y + dirVector.y * stepRange);
    }

    private void Update()
    {
        if (canMove)
        {
            DirectionFeedback();

            if (canChangeDirection)
            {
                StartCoroutine(WaitBeforeChangingDirection());
                canChangeDirection = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopAllCoroutines();
                canMove = false;
                isOnMoving = true;
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
        transform.position = Vector2.MoveTowards(transform.position, targetVector, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetVector)
        {
            canChangeDirection = true;
            isOnMoving = false;
        }
    }

    private void DirectionFeedback()
    {
        dirFb.sprite = difDir[direction];
    }

    public void GuyActivation(bool upordown)
    {
        canMove = upordown;
        myCanvas.SetActive(upordown);
        myVCam.SetActive(upordown);

        if (upordown)
        {            
            canChangeDirection = true;
        }
    }

    public void ReactivateGuy()
    {
        canMove = true;
        myCanvas.SetActive(true);
    }

    public void ShutdownGuy()
    {
        canMove = false;
        myCanvas.SetActive(false);
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
        targetVector = new Vector2(transform.position.x + dirVector.x * stepRange, transform.position.y + dirVector.y * stepRange);

        canChangeDirection = true;
        StopCoroutine(WaitBeforeChangingDirection());
    }
}