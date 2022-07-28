using System.Collections.Generic;
using UnityEngine;

public class InitializationH2A : MonoBehaviour
{
    [SerializeField]
    public Transform[] transforms;

    [SerializeField]
    public GameObject gb;

    [SerializeField]
    private TupleList tuples;
    [SerializeField]
    private float scaleMagnification;
    private void Start()
    {
        InitailizeLine();
    }
    
    public void InitailizeLine()
    {
        IEnumerator<TupleList.pair> e = tuples.GetEnumerator();
        while (e.MoveNext())
        {
            Create(e.Current.x, e.Current.y);
        }
    }

    private void Create(int t1, int t2)
    {
        CreateGameObject(gb, transforms[t1], transform,
                    CreateGBTool.CalculateQuaternion(transforms[t1].position, transforms[t2].position),
                    new Vector3(CreateGBTool.Distance(transforms[t1].position, transforms[t2].position) * scaleMagnification, 1, 1));
    }

    private void CreateGameObject(GameObject gb, Transform t, Transform parent, Quaternion q, Vector3 scale)
    {
        GameObject gameObject = Instantiate(gb, t.position, q, parent);
        gameObject.transform.localScale = scale;
    }
}