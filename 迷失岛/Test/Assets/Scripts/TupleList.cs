using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new TupleList", menuName = "Tuple/TupleList")]
public class TupleList : ScriptableObject
{
    [Serializable]
    public struct pair
    {
        [SerializeField]
        public int x, y;
    }

    [SerializeField]
    private List<pair> tuples;

    public pair GetPair(int index)
    {
        return tuples[index];
    }

    public IEnumerator<pair> GetEnumerator()
    {
        return tuples.GetEnumerator();
    }
}