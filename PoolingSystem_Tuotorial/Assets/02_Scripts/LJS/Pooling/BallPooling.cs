using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global_define;

public class BallPooling : MonoBehaviour
{
    #region SINGLETON
    public static BallPooling Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    int Init_Count = 10;
    int Make_Count = 5;

    public PlayerController player;
    public Ball ballPrefabs;

    LinkedList<Ball> linkBallActive = new LinkedList<Ball>();
    List<Ball> lBallRemove = new List<Ball>();
    LinkedList<Ball> linkBallDeactive = new LinkedList<Ball>();

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        var node = linkBallActive.GetEnumerator();

        while (node.MoveNext())
        {
            if (node.Current.gameObject.activeSelf == false)
            {
                lBallRemove.Add(node.Current);
            }
        }

        if (lBallRemove.Count > 0)
        {
            for (int i = 0; i < lBallRemove.Count; i++)
            {
                linkBallActive.Remove(lBallRemove[i]);
                linkBallDeactive.AddLast(lBallRemove[i]);
            }
        }

        lBallRemove.Clear();
    }

    public void Init()
    {
        AddPool(Init_Count);
    }

    public void AddPool(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            var obj = Instantiate(ballPrefabs, player.transform.position, Quaternion.identity);
            obj.transform.parent = gameObject.transform;
            linkBallDeactive.AddLast(obj);
            obj.gameObject.SetActive(false);
        }
    }


    public Ball Drop(Vector3 _direction)
    {
        if (linkBallDeactive.Count == 0)
        {
            AddPool(Make_Count);
        }
        var deactiveBall = linkBallDeactive.First.Value;
        linkBallActive.AddLast(deactiveBall);
        linkBallDeactive.RemoveFirst();

        IPoolObject pooledObj = deactiveBall.GetComponent<IPoolObject>();
        if (pooledObj != null)
        {
            pooledObj.OnSpawnObject(_direction);
        }
        return deactiveBall;
    }
}
