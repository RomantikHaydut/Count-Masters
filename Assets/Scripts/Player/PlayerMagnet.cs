using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    private UIManager uiManager;

    [Header("Clone")]
    [SerializeField] private GameObject _player;

    private bool isGameStarted=false;
    public bool canMagnet = true;
    [SerializeField] private float _magnetForce;
    private float _playerRadius;
    private float _playerHeight;

    public List<GameObject> activePlayerlist;
    public List<GameObject> inactivePlayerlist;
    public List<GameObject> finishedPlayerlist;

    [SerializeField] private int _maxCount = 250;
    private int _startCount;
    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        CapsuleCollider playerCollider = _player.GetComponent<CapsuleCollider>();
        _playerRadius = playerCollider.radius;
        _playerHeight = playerCollider.height;


    }
    private void Start()
    {
        _startCount = GameManager.Instance.startCount;
    }


    void Update()
    {
        if (isGameStarted)
        {
            RunnerMagnet();
        }
    }


    public void CreateRunnerStart()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject ad = Instantiate(_player, PlayerSpawnPosition(), transform.rotation);
            ad.transform.SetParent(transform);
            MakePlayerInactive(ad);
        }

        for (int i = 0; i < _startCount; i++)
        {
            MakePlayerActive(inactivePlayerlist[i]);
        }

        isGameStarted = true;
        DisplayRunnerCount();

    }

    public void CreateRunner(int createCount)
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

        if (activePlayerlist.Count < 1)
            return;

        for (int i = 0; i < destroyCount; i++)
        {
            MakePlayerInactive(activePlayerlist[0]);
        }

        DisplayRunnerCount();
    }

    public void DestroyRunner(GameObject runner)
    {
        MakePlayerInactive(runner);
        if (activePlayerlist.Count == 0)
            GameManager.Instance.LoseGame();

        DisplayRunnerCount();
    }

    private void MakePlayerActive(GameObject player)
    {
        inactivePlayerlist.Remove(player);

        activePlayerlist.Add(player);

        player.transform.position = PlayerSpawnPosition();
        player.SetActive(true);
    }
    public void MakePlayerInactive(GameObject player)
    {
        activePlayerlist.Remove(player);

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

    private void RunnerMagnet()
    {
        if (canMagnet)
        {
            for (int i = 0; i < activePlayerlist.Count; i++)
            {
                activePlayerlist[i].GetComponent<Rigidbody>().AddForce(_magnetForce * Time.deltaTime * (transform.position - activePlayerlist[i].transform.position));
            }
        }
    }

    public void StopMagnet() => canMagnet = false;
    public void OpenMagnet() => canMagnet = true;

    private void MovePlayerToMid() => transform.position = new Vector3(0, transform.position.y, transform.position.z);

    private IEnumerator Finish()
    {

        yield return null;
        MovePlayerToMid();
        StopHorizontalMovement();
        RotateCamera();

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

        float offsetXForPlayerParent = layerCount * _playerRadius;
        transform.position += new Vector3(offsetXForPlayerParent, 0, 0);

        List<GameObject> activeRunnerCloneList = new List<GameObject>();

        for (int i = 0; i < activePlayerlist.Count; i++)
            activeRunnerCloneList.Add(activePlayerlist[i]);


        float offsetX = -layerCount * _playerRadius * 2;
        float offsetY = 0;
        for (int i = layerCount; i >= 1; i--) // Split triangle
        {
            for (int j = 0; j < i; j++)
            {
                Vector3 targetPos = new Vector3((_playerRadius * 2 * (j + 1)) + offsetX, offsetY, 0);
                StartCoroutine(FinishMovement(activeRunnerCloneList[0], targetPos));
                activeRunnerCloneList.Remove(activeRunnerCloneList[0]);
            }
            offsetY += _playerHeight;
            offsetX += _playerRadius;
        }

        offsetX = (-layerCount * _playerRadius * 2);
        for (int i = 0; i < runnerCount; i++) // Split Leftovers
        {
            Vector3 targetPos = new Vector3(offsetX, _playerHeight * i, 0);
            StartCoroutine(FinishMovement(activeRunnerCloneList[0], targetPos));
            activeRunnerCloneList.Remove(activeRunnerCloneList[0]);
            offsetX += _playerRadius;
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
                yield break;
            }

            yield return null;
            runner.transform.position = Vector3.Lerp(startPos, new Vector3(targetPos.x + transform.position.x, targetPos.y, transform.position.z), timer);
            timer += Time.deltaTime * speed;
        }
    }

    private void StopHorizontalMovement() => GetComponent<PlayerMovement>().canHorizontalMove = false;
    private void RotateCamera() => FindObjectOfType<CameraController>().Rotate();

    public void DisplayRunnerCount()
    {
        uiManager.DisplayText(gameObject, activePlayerlist.Count.ToString());
    }

    private void MoveCameraTarget(Vector3 movePosition)
    {
        GameObject.FindGameObjectWithTag("FocalPoint").transform.position = movePosition;
    }
    public void StartBossFight(Transform boss)
    {
        canMagnet = false;
        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            activePlayerlist[i].GetComponent<RunnerController>().StartAttack(boss);
        }
        MoveCameraTarget(transform.position + transform.forward * 5);
    }

    public void FinishBossFight()
    {
        for (int i = 0; i < activePlayerlist.Count; i++)
        {
            activePlayerlist[i].GetComponent<RunnerController>().StopAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Finish());
            FindObjectOfType<PlayerMovement>().IncreaseSpeed(1.5f);
        }
    }
}
