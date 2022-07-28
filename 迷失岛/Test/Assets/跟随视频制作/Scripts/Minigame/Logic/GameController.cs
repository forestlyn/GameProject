using UnityEngine;
using UnityEngine.Events;

public class GameController : MySingleton<GameController>
{
    public UnityEvent OnFinshed;

    [Header("ÓÎÏ·Êý¾Ý")]
    public GameH2AData gameData;

    public GameH2AData[] gameDataArray;

    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;

    public Transform[] holderTransforms;

    public void ResetGame()
    {
        for (int i = 0; i < lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }
        foreach (Transform holder in holderTransforms)
        {
            if (holder.childCount > 0)
            {
                Destroy(holder.GetChild(0).gameObject);
            }
        }
        DrawLine();
        CreateBall();
    }

    public void CheckFinished()
    {
        foreach (Ball ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
            {
                return;
            }
        }
        foreach (Transform holder in holderTransforms)
        {
            holder.GetComponent<BoxCollider2D>().enabled = false;
        }
        EventHandler.CallGamePassedEvent(gameData.gameName);
        OnFinshed?.Invoke();
    }

    public void DrawLine()
    {
        foreach (Connection connection in gameData.lineConnections)
        {
            LineRenderer line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[connection.from].position);
            line.SetPosition(1, holderTransforms[connection.to].position);

            Holder from = holderTransforms[connection.from].GetComponent<Holder>();
            Holder to = holderTransforms[connection.to].GetComponent<Holder>();
            from.linkHolders.Add(to);
            to.linkHolders.Add(from);
        }
    }

    public void CreateBall()
    {
        for (int i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] != BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = false;
                Ball ball = Instantiate(ballPrefab, holderTransforms[i]);
                ball.SetUpBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
                holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
            }
            else
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
            }
        }
    }

    public void SetGameWeekData(int week)
    {
        Debug.LogWarning("now week" + week);
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }
}