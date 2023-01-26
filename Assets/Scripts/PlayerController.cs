using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerType playerData;
    public bool rush = true;

    float minZBound;
    float maxZBound;

    [SerializeField] private bool mouseMovement;
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float horizontalSpeed = 1f;
    private float mouseClickPosX;
    private float mouseActivePosX;
    public float boundryX; // Ground boundry

    [SerializeField] LayerMask ground;

    private UIManager uiManager;
    private Vector3 currentTouchDeltaPosition;

    [HideInInspector] public List<GameObject> enemyList;
    GameObject[] enemies;

    [Header("Clone")]
    [SerializeField] float magnetForce;
    public GameObject player;
    [SerializeField] private float playerRadius;
    [SerializeField] private float playerHeight;
    public List<GameObject> playerlist;
    public List<GameObject> activePlayerlist;
    public List<GameObject> inactivePlayerlist;
    public List<GameObject> finishedPlayerlist;
    public int maxCount = 250;
    public int startCount = 9;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        uiManager = FindObjectOfType<UIManager>();
        boundryX = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>().bounds.size.x / 2;
        maxZBound = 4.53f;
        minZBound = -4.53f;
        CapsuleCollider playerCollider = player.GetComponent<CapsuleCollider>();
        //CharacterController playerCollider = player.GetComponent<CharacterController>();
        playerRadius = playerCollider.radius;
        playerHeight = playerCollider.height;
        DisplayRunnerCount();
    }

    void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            RunnerMagnet2();
            if (rush)
            {
                Movement();
            }
            else
            {
                // CheckEnemiesLive();
            }
            //CheckZBound();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateRunnerStart2();
                GameManager.Instance.isGameStarted = true;
                rush = true;
            }
        }

    }

    void MovePlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + Input.GetAxis("Mouse X") * Time.deltaTime * playerData.moveSpeed, minZBound, maxZBound));
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                currentTouchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + currentTouchDeltaPosition.x * Time.deltaTime * playerData.moveSpeed, minZBound, maxZBound));
            }
        }
    }
    private void Movement()
    {
        //Forward movement.
        transform.position += transform.forward * Time.deltaTime * forwardSpeed;

        //Horizontal Movement keyboard.
        if (!mouseMovement)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            bool goingRight = (horizontalInput > 0) ? true : false;
            if (goingRight)
            {
                bool canGoRight = (transform.position.x + DistanceFromFarRunner() < boundryX) ? true : false;
                if (!canGoRight)
                    horizontalInput = 0;
            }
            else
            {
                bool canGoLeft = (transform.position.x - DistanceFromFarRunner() > -boundryX) ? true : false;
                if (!canGoLeft)
                    horizontalInput = 0;
            }
            

            transform.position += transform.right * Time.deltaTime * horizontalInput * horizontalSpeed;
        }
        //Horizontal Movement mouse.
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseClickPosX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseActivePosX = Input.mousePosition.x;
                float horizontalInput = (mouseActivePosX - mouseClickPosX <= 0) ? -1 : 1;
                transform.position += transform.right * Time.deltaTime * horizontalInput * horizontalSpeed;
            }
        }
    }
    //void CheckEnemiesLive()
    //{
    //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    if (enemies.Length == 0)
    //    {
    //        rush = true;
    //    }
    //}
    void CheckZBound()
    {
        minZBound = transform.position.z;
        maxZBound = transform.position.z;

        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            if (transform.GetChild(i).transform.position.z < minZBound)
            {
                minZBound = transform.GetChild(i).transform.position.z;
            }
            if (transform.GetChild(i).transform.position.z > maxZBound)
            {
                maxZBound = transform.GetChild(i).transform.position.z;
            }
        }

        if (!Physics.Raycast(new Vector3(transform.position.x, 3f, minZBound - 1f), new Vector3(0f, -1f, 0f), 10f, ground))
        {
            minZBound = transform.position.z;
        }
        else
        {
            minZBound = -4.53f;
        }
        if (!Physics.Raycast(new Vector3(transform.position.x, 3f, maxZBound + 1f), new Vector3(0f, -1f, 0f), 10f, ground))
        {
            maxZBound = transform.position.z;
        }
        else
        {
            maxZBound = 4.53f;
        }
    }

    private float DistanceFromFarRunner()
    {
        float maxDistance = 0;
        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            float newDistance = Mathf.Abs(transform.position.z - activePlayerlist[i].transform.position.z);
            if (newDistance > maxDistance)
                maxDistance = newDistance;
        }

        return maxDistance;
    }


    // Clone

    void CreateRunnerStart2()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject ad = Instantiate(player, PlayerSpawnPosition(), transform.rotation);
            ad.transform.SetParent(transform);
            playerlist.Add(ad);
            MakePlayerInactive(ad);
            //playerBodys.Add(ad.GetComponent<Rigidbody>());
        }

        for (int i = 0; i < startCount; i++)
        {
            MakePlayerActive(playerlist[i]);
        }

        DisplayRunnerCount();

    }

    public void CreateRunner2(int createCount)
    {
        if (inactivePlayerlist.Count > 0)
        {
            for (int i = 0; i < createCount; i++)
            {
                if (inactivePlayerlist.Count == 0)
                    break;

                GameObject runner = inactivePlayerlist[0];
                MakePlayerActive(runner);
            }
        }

        DisplayRunnerCount();
    }

    public void DestroyRunners(int destroyCount)
    {
        if (destroyCount >= activePlayerlist.Count)
            destroyCount = activePlayerlist.Count - 1;
        if (activePlayerlist.Count > 1)
        {
            for (int i = 0; i < destroyCount; i++)
            {
                if (activePlayerlist.Count <= 1)
                    break;

                MakePlayerInactive(activePlayerlist[0]);
            }
        }
        DisplayRunnerCount();
    }

    public void DestroyRunner(GameObject runner)
    {
        if (activePlayerlist.Count > 1)
        {
            MakePlayerInactive(runner);
        }

        DisplayRunnerCount();
    }

    private void MakePlayerActive(GameObject player)
    {
        if (inactivePlayerlist.Contains(player))
            inactivePlayerlist.Remove(player);

        if (!activePlayerlist.Contains(player))
            activePlayerlist.Add(player);

        player.transform.position = PlayerSpawnPosition();
        player.SetActive(true);
    }
    public void MakePlayerInactive(GameObject player)
    {
        if (activePlayerlist.Contains(player))
            activePlayerlist.Remove(player);

        if (!inactivePlayerlist.Contains(player))
            inactivePlayerlist.Add(player);

        player.SetActive(false);
    }

    Vector3 PlayerSpawnPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = pos + transform.position;
        newPos.y = 0f;
        return newPos;
    }

    void RunnerMagnet2()
    {
        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            activePlayerlist[i].GetComponent<Rigidbody>().AddForce(magnetForce * Time.deltaTime * (transform.position - activePlayerlist[i].transform.position));
        }
    }

    private IEnumerator Finish()
    {
        FindObjectOfType<CameraController>().Rotate();
        yield return null;
        int layerCount = 0;
        int runnerCount = 0; // After for loop it is leftOverCount
        int runnerRequirementForLayer = 1;

        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            runnerCount++;
            if (runnerCount >= runnerRequirementForLayer)
            {
                runnerRequirementForLayer++;
                layerCount++;
                runnerCount = 0;
            }

            Rigidbody rb = activePlayerlist[i].GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        List<GameObject> activeRunnerCloneList = new List<GameObject>();

        for (int i = 0; i < activePlayerlist.Count; i++)
            activeRunnerCloneList.Add(activePlayerlist[i]);


        float offsetX = -layerCount * playerRadius * 2;
        for (int i = layerCount; i >= 1; i--) // Split triangle
        {
            for (int j = 0; j < i; j++)
            {
                Vector3 targetPos = new Vector3((playerRadius * 2 * (j + 1)) + offsetX, playerHeight * (layerCount - i), 0);
                StartCoroutine(FinishMovement(activeRunnerCloneList[0], targetPos));
                activeRunnerCloneList.Remove(activeRunnerCloneList[0]);
            }
            offsetX += playerRadius;
        }

        offsetX = (-layerCount * playerRadius * 2);
        for (int i = 0; i < runnerCount; i++) // Split Leftovers
        {
            Vector3 targetPos = new Vector3(offsetX, playerHeight * i, 0);
            StartCoroutine(FinishMovement(activeRunnerCloneList[0], targetPos));
            activeRunnerCloneList.Remove(activeRunnerCloneList[0]);
            offsetX += playerRadius;
        }
        yield break;
    }

    private IEnumerator FinishMovement(GameObject runner, Vector3 targetPos)
    {
        float timer = 0;
        float speed = 1f;
        Vector3 startPos = runner.transform.position;
        while (true)
        {
            if (timer >= 1)
            {
                runner.transform.position = new Vector3(targetPos.x + transform.position.x, targetPos.y, transform.position.z);
                //runner.GetComponent<RunnerController>().characterController.enabled = true;
                //runner.GetComponent<RunnerController>().comeFinish = true;
                yield break;
            }

            yield return null;
            runner.transform.position = Vector3.Lerp(startPos, new Vector3(targetPos.x + transform.position.x, targetPos.y, transform.position.z), timer);
            timer += Time.deltaTime * speed;
        }
    }

    public void DisplayRunnerCount()
    {
        uiManager.DisplayText(gameObject, activePlayerlist.Count.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Finish());
        }
    }
}
