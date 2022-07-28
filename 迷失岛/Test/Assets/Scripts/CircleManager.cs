using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleManager : GBManager
{
    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private List<Button> circleBtns;

    [SerializeField]
    private TupleList pairs;

    [SerializeField]
    private List<CircleGB> circleGBs;

    [SerializeField]
    private List<Transform> transforms;

    protected override void Start()
    {
        base.Start();
        restartBtn.onClick.AddListener(Initialize);
        for (int i = 0; i < circleBtns.Count; i++)
        {
            int t = i;
            circleBtns[i].onClick.AddListener(delegate { OnClickCircleButton(t); });
        }
    }

    private void OnClickCircleButton(int index)
    {
        Debug.LogWarningFormat("{0}", index);
        StartCoroutine(MoveToPosition(circleGBs[index].gameObject.transform.Find("SS").gameObject,
            transforms[index + 1].position - transforms[index].position));
    }

    private IEnumerator MoveToPosition(GameObject gb, Vector3 v)
    {
        if (gb)
        {
            while (gb.transform.localPosition != v)
            {
                gb.transform.localPosition = Vector3.MoveTowards(gb.transform.localPosition, v, 2 * Time.deltaTime);
                yield return 0;
            }
            
        }
    }
}