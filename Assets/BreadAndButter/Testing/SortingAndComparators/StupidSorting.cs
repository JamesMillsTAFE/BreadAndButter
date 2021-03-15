using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidSorting : MonoBehaviour
{
    [SerializeField]
    private int goCount = 50;

    private List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateObjects();
         
        Debug.LogError(gameObjects
            .Where(x => x.tag == "Player")
            .Select(x => x.name)
            .OrderBy(x => int.Parse(x.Substring(x.IndexOf('(') + 1, x.LastIndexOf(')') - x.IndexOf('(') - 1)))
            .Select(x => x.Substring(x.IndexOf('(') + 1, x.LastIndexOf(')') - x.IndexOf('(') - 1))
            .Where(x => int.Parse(x) > 0)
            .ToList()
            .Aggregate((current, next) => $"{current}, {next}"));

        string test = "";
        foreach (GameObject go in gameObjects)
        {
            test += $"{go.name},";
        }
        Debug.LogError(test);

        test = "";
        foreach (GameObject go in gameObjects.Randomise())
        {
            test += $"{go.name},";
        }
        Debug.LogError(test);
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
