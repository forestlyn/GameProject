using System.Collections.Generic;

public class Holder : Interactive
{
    public bool isEmpty;
    public bool isMatch;
    private Ball currentBall;
    public BallName match;
    public HashSet<Holder> linkHolders = new HashSet<Holder>();

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (match == ball.ballDetails.ballName)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }
    public override void OnEmptyClickedAction()
    {
        foreach(Holder holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.parent = holder.transform;

                holder.CheckBall(currentBall);
                holder.isEmpty = false;
                currentBall = null;
                isEmpty = true;

                GameController.Instance.CheckFinished();
            }
        }
    }
}