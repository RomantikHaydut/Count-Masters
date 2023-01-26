using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    //[SerializeField] float magnetForce;

    //public GameObject player;
    //public List<GameObject> playerlist;
    //public List<GameObject> activePlayerlist;
    //public List<GameObject> inactivePlayerlist;
    //// public List<Rigidbody> playerBodys;

    //public int maxCount = 250;
    //public int startCount = 9;


    //private void Update()
    //{
    //    if (GameManager.Instance.isGameStarted)
    //    {
    //        RunnerMagnet2();
    //    }
    //    else
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            CreateRunnerStart2();
    //            GameManager.Instance.isGameStarted = true;
    //        }
    //    }

    //}

    //void CreateRunner()
    //{
    //    GameObject ad = Instantiate(player, PlayerSpawnPosition(), transform.rotation);
    //    ad.transform.SetParent(transform);
    //    //playerBodys.Add(ad.GetComponent<Rigidbody>());
    //}
    //void CreateRunnerStart()
    //{
    //    for (int i = 0; i < startCount; i++)
    //    {
    //        GameObject ad = Instantiate(player, PlayerSpawnPosition(), transform.rotation);
    //        ad.transform.SetParent(transform);
    //        //playerBodys.Add(ad.GetComponent<Rigidbody>());
    //    }
    //}

    //#region Object Pooling
    //void CreateRunnerStart2()
    //{
    //    for (int i = 0; i < maxCount; i++)
    //    {
    //        GameObject ad = Instantiate(player, PlayerSpawnPosition(), transform.rotation);
    //        ad.transform.SetParent(transform);
    //        playerlist.Add(ad);
    //        MakePlayerInactive(ad);
    //        //playerBodys.Add(ad.GetComponent<Rigidbody>());
    //    }

    //    for (int i = 0; i < startCount; i++)
    //    {
    //        MakePlayerActive(playerlist[i]);
    //    }
    //}

    //public void CreateRunner2(int createCount)
    //{
    //    if (inactivePlayerlist.Count > 0)
    //    {
    //        for (int i = 0; i < createCount; i++)
    //        {
    //            if (inactivePlayerlist.Count == 0)
    //                break;

    //            GameObject runner = inactivePlayerlist[i];
    //            MakePlayerActive(runner);
    //        }
    //    }
    //}

    //public void DestroyRunners(int destroyCount)
    //{
    //    if (destroyCount >= activePlayerlist.Count)
    //        destroyCount = activePlayerlist.Count - 1;
    //    if (activePlayerlist.Count > 1)
    //    {
    //        for (int i = 0; i < destroyCount; i++)
    //        {
    //            if (activePlayerlist.Count <= 1)
    //                break;

    //            MakePlayerInactive(activePlayerlist[0]);
    //        }
    //    }

    //}

    //public void DestroyRunner(GameObject runner)
    //{
    //    if (activePlayerlist.Count > 1)
    //    {
    //        MakePlayerInactive(runner);
    //    }

    //}

    //private void MakePlayerActive(GameObject player)
    //{
    //    if (inactivePlayerlist.Contains(player))
    //        inactivePlayerlist.Remove(player);

    //    if (!activePlayerlist.Contains(player))
    //        activePlayerlist.Add(player);

    //    player.transform.position = PlayerSpawnPosition();
    //    player.SetActive(true);
    //}
    //public void MakePlayerInactive(GameObject player)
    //{
    //    if (activePlayerlist.Contains(player))
    //        activePlayerlist.Remove(player);

    //    if (!inactivePlayerlist.Contains(player))
    //        inactivePlayerlist.Add(player);

    //    player.SetActive(false);
    //}
    //#endregion
    //Vector3 PlayerSpawnPosition()
    //{
    //    Vector3 pos = Random.insideUnitSphere * 0.1f;
    //    Vector3 newPos = pos + transform.position;
    //    newPos.y = 0f;
    //    return newPos;
    //}

    ////void RunnerMagnet()
    ////{
    ////    for (int i = 0; i < playerBodys.Count; i++)
    ////    {
    ////        playerBodys[i].AddForce((transform.position - playerBodys[i].position) * Time.deltaTime * magnetForce);
    ////    }
    ////}

    //void RunnerMagnet2()
    //{
    //    for (int i = 0; i < activePlayerlist.Count; i++)
    //    {
    //        activePlayerlist[i].GetComponent<Rigidbody>().AddForce(magnetForce * Time.deltaTime * (transform.position - activePlayerlist[i].transform.position));
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Sum"))
    //    {
    //        other.gameObject.SetActive(false);
    //        other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
    //        int spawnCount = other.gameObject.GetComponent<DoorController>().number;

    //        CreateRunner2(spawnCount);
    //    }

    //    if (other.gameObject.CompareTag("Multiply"))
    //    {
    //        other.gameObject.SetActive(false);
    //        other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
    //        int spawnCount = other.gameObject.GetComponent<DoorController>().number * activePlayerlist.Count;

    //        CreateRunner2(spawnCount);

    //    }
    //}
}
