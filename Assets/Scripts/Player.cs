using System;
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
    public Path currentpath;
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

    PlayerState playerState = PlayerState.Normal;
    bool ilkupdate;
    void Start()
    {
        lastpos = PlayerFollow.position;
        baseSpeedZ = speedZ;
        swerveInput = GetComponent<SwerveInput>();

    }
    
    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
        Events.complateGame += Events_complateGame;

    }

  

    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
        Events.complateGame -= Events_complateGame;

    }
   

    void Update()
    {
        if (GamaManager.Instance.gameState==GameState.None)
        {
            return;
        }
        if (gameOver == true)
        {
            return;
        }

        float swerveAmount = swerveInput.MoveFactor * Time.deltaTime * speedX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        moveonpath();
        transform.rotation = PlayerFollow.rotation;

        if (PlayerFollow.position - lastpos != Vector3.zero && ilkupdate==true)
        {
            transform.position += PlayerFollow.position - lastpos;
        }
        ilkupdate = true;
        lastpos = PlayerFollow.position;
        if (Vector3.Distance(new Vector3(PlayerFollow.transform.position.x,0f,PlayerFollow.transform.position.z), new Vector3(transform.position.x, 0f, transform.position.z)+(transform.right.normalized) * swerveAmount) <= 3 && playerState != PlayerState.Coner)
        {
            transform.Translate(swerveAmount, 0, 0);

        }
    }


   
    private void moveonpath()
    {
        if (currentpath == null)
        {
            ilkupdate = false;
            currentWayPointID = 0;
            currentpath = PathManager.instance.GetPath();

            if (currentpath == null)
            {
                if (GamaManager.Instance.gameState==GameState.winLine)
                {
                    Events.CallComplateGame();
                    gameOver = true;
                  
                }
                return;
            }

            if (currentpath.isCorner)
            {              
                currentpath.pointchange(transform.position);
                playerState = PlayerState.Coner;
              
            }
            else
            {
                playerState = PlayerState.Normal;
            }
            currentpath.CreatePath();
            PlayerFollow.transform.position = currentpath.GetPoint();
           

        }
        //move
        float distance = Vector3.Distance(currentpath.bezierObjList[currentWayPointID], PlayerFollow.position);
        direction = currentpath.bezierObjList[currentWayPointID] - PlayerFollow.position;
        PlayerFollow.position = Vector3.MoveTowards(PlayerFollow.position, currentpath.bezierObjList[currentWayPointID], speedZ * Time.deltaTime);
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
        if (currentWayPointID >= currentpath.bezierObjList.Count)
        {
            currentpath = null;

        }

    }
    public void SpeedUpdate(float speed, float time)
    {
        ParticalEffectController.Instance.SpeedEffect().Play();
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
        ParticalEffectController.Instance.SpeedEffect().Stop();
    }

    private void Events_GameOver()
    {

        gameOver = true;

    }

    private void Events_complateGame()
    {
        gameOver = true;
    }

}
