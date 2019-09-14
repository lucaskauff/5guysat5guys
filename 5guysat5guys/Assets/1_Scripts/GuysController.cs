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

    [Header("Objects to serialize")]
    [SerializeField] Text dirFb = default; //will be replaced by an image

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

    }

    private void Update()
    {
        if (canMove)
        {
            if (canChangeDirection)
            {
                StopCoroutine(WaitBeforeChangingDirection());
                StartCoroutine(WaitBeforeChangingDirection());
                canChangeDirection = false;
            }

            DirectionFeedback();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
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
            isOnMoving = false;
        }
    }

    private void DirectionFeedback()
    {
        dirFb.text = direction.ToString();
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
    }
}