using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreadAndButter;
using BreadAndButter.Utils.Comparers;
using BreadAndButter.Utils.Extensions;

public class CoreTest : MonoSingleton<CoreTest>
{
    [SerializeField]
    private RunnableTest runnableTest;

    [SerializeField]
    private bool testEnabled = true;


    [SerializeField]
    private int goCount = 50;

    private List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateInstance();
        FlagAsPersistant();

        int goIndex = gameObjects.BinarySearch(gameObjects[5], new GameObjectNameComparer());

        RunnableUtils.Setup(ref runnableTest, gameObject, "Stephane", new Vector3(1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        runnableTest.Enabled = testEnabled;
        RunnableUtils.Run(ref runnableTest, gameObject);
    }

    private void GenerateObjects()
    {
        for (int i = 0; i < goCount; i++)
        {
            gameObjects.Add(new GameObject($"GameObject({Random.Range(-100, 100)})"));
            gameObjects[gameObjects.Count - 1].transform.parent = transform;
            gameObjects[gameObjects.Count - 1].tag = Random.Range(0, 2) == 1 ? "Player" : "Untagged";
        }
    }
}
