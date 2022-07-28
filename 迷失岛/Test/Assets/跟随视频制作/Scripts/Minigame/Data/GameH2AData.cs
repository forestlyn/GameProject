using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameH2AData", menuName = ("minigame/New GameH2AData "))]

public class GameH2AData : ScriptableObject
{
     public string gameName;

    [Header("������ּ����ӦͼƬ")]
    public List<BallDetails> ballDataList;

    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDataList.Find(x => x.ballName == ballName);
    }

    [Header("��Ϸ�߼�����")]
    public List<Connection> lineConnections;
    public List<BallName> startBallOrder;

}
[System.Serializable]
public class BallDetails
{
    public BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;

}
[System.Serializable]
public class Connection
{
   public int from, to;
}