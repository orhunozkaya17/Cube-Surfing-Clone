using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwerveInput))]
public class Player : MonoBehaviour
{
    [Header("Base ")]
    [SerializeField] float speedX = 0.5f;
    [SerializeField] float speedZ = 0.5f;
    [SerializeField] float maxSwerveAmount = 0.5f;

    [Header("Path")]
    [SerializeField] Transform PlayerFollow;
    [SerializeField] Path path;
    [SerializeField] float reachDistance = 0.1f;
    [SerializeField] float rotationSpeed = 10f;
    private int currentWayPointID = 0;
    private Vector3 direction;
    Vector3 lastpos;
    private float baseSpeedZ;
    private SwerveInput swerveInput;
    private bool gameOver = false;
    private float speedTime = 0f;
    Coroutine SpeedRoutine;
    void Start()
    {
        lastpos = PlayerFollow.position; 
        baseSpeedZ = speedZ;
        swerveInput = GetComponent<SwerveInput>();
    }

    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
    }
    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
    }
    private void Events_GameOver()
    {
        gameOver = true;
    }

    void Update()
    {
        if (gameOver == true)
        {
            return;
        }

        float swerveAmount = swerveInput.MoveFactor * Time.deltaTime * speedX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        moveonpath();
        transform.rotation = PlayerFollow.rotation;
        //transform.Translate(direction.normalized * Time.deltaTime * speedZ); 
        if (PlayerFollow.position- lastpos  != Vector3.zero)
        {
            transform.position += PlayerFollow.position-lastpos;
        }
        lastpos = PlayerFollow.position;
        //distance between playerfollow   and player 


        float distance = Vector3.Distance(PlayerFollow.position, transform.position + ((transform.right.normalized) * swerveAmount));

        if (Vector3.Distance(PlayerFollow.position, transform.position + (transform.right.normalized) * swerveAmount) <= 3)
        {
            transform.Translate(swerveAmount, 0, 0);

        }




    }


    public void SpeedUpdate(float speed, float time)
    {
        speedZ = speed;
        speedTime = time;
        if (SpeedRoutine != null)
        {
            StopCoroutine(SpeedRoutine);
        }
        SpeedRoutine = StartCoroutine(SpeedBaseDown());
    }

    IEnumerator SpeedBaseDown()
    {


        while (speedTime > 0)
        {
            speedTime -= Time.deltaTime;
            yield return null;
        }
        speedZ = baseSpeedZ;
    }
    private void moveonpath()
    {

        //move
        float distance = Vector3.Distance(path.bezierObjList[currentWayPointID], PlayerFollow.position);
        direction = path.bezierObjList[currentWayPointID] - PlayerFollow.position;
        PlayerFollow.position = Vector3.MoveTowards(PlayerFollow.position, path.bezierObjList[currentWayPointID], speedZ * Time.deltaTime);
        //rotation

        if (direction != Vector3.zero)
        {
            direction.y = 0;
            direction.Normalize();
            var quartinion = Quaternion.LookRotation(direction);
            PlayerFollow.rotation = Quaternion.Slerp(transform.rotation, quartinion, rotationSpeed * Time.deltaTime);
        }
        if (distance < reachDistance)
        {
            currentWayPointID++;
        }
        if (currentWayPointID >= path.bezierObjList.Count)
        {
            gameOver = true;

        }
        
    }
}
