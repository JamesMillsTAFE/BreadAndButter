using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreadAndButter;

public class CoreTest : MonoSingleton<CoreTest>
{
    [SerializeField]
    private RunnableTest runnableTest;

    [SerializeField]
    private bool testEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        CreateInstance();
        FlagAsPersistant();

        RunnableUtils.Setup(ref runnableTest, gameObject, "Stephane", new Vector3(1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        runnableTest.Enabled = testEnabled;
        RunnableUtils.Run(ref runnableTest, gameObject);
    }
}
