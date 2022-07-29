# 迷失岛

[《迷失岛2》游戏框架开发01:实现场景转换｜Unity教程_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV19a411i7Tw/?spm_id_from=333.788&vd_source=c92f5017d5fd5404445b257519e8126e)

先尝试自己制作，做到一半发现没有框架随手乱做做不下去了 跟随视频做了一遍 自己加上了还没放出来的存档功能

## 场景

利用unity的多场景管理[多场景编辑 - Unity 手册](https://docs.unity.cn/cn/2018.4/Manual/MultiSceneEditing.html)，将需要一直存在的脚本挂在**管理器场景**上。

### 场景加载卸载

可利用协程实现异步加载 实现加载过渡动画

## 背包系统

MVC模式

分为 UI、逻辑、数据 三个部分 数据用ScriptableObject保存

利用Event，在数据改变时调用Call，需要改变的部分订阅事件，实现UI的改变。

个人感觉也可以利用GameManager保存各个部分统一调用更新UpdateUI函数，Event无法查看订阅事件的方法，阅读起来比较困难

## 鼠标

整个游戏均为鼠标点击互动，利用CursorManager来管理调用其它脚本事件，需要互动的物体加上碰撞体和对应的tag，点击物体时判断身上的tag进而获得其脚本调用对应方法。

## 小游戏

MVC模式

小游戏同样为MVC模式，分为UI，逻辑，数据三个部分 由于球和线使用动态生成，其数据均用ScriptableObject保存

利用Holder保存其当前的Ball和相邻的Holder，继承Interactive，判断其相邻的Holder有无空位 存在交换球

## 存档

### 游戏内切换场景存档

ObjectManager 每次加载场景前将存储的之前场景内可交互物体状态更新 

加载之后将场景内的物体状态更新为存储的状态

代码如下所示 利用event调用方法

```c#
private void OnBeforeSceneUnloadEvent()
{
    foreach (Prop prop in FindObjectsOfType<Prop>())
    {
        if (!propAvailableDic.ContainsKey(prop.propName))
        {
            propAvailableDic.Add(prop.propName, true);
        }
        else
        {
            prop.gameObject.SetActive(propAvailableDic[prop.propName]);
        }
    }

    foreach (Interactive interactive in FindObjectsOfType<Interactive>())
    {
        if (!interactiveStateDic.ContainsKey(interactive.name))
        {
            interactiveStateDic.Add(interactive.name, interactive.isDone);
        }
        else
        {
            interactiveStateDic[interactive.name] = interactive.isDone;
        }
    }
} 
private void OnAfterSceneLoadedEvent()
{
    foreach (Prop prop in FindObjectsOfType<Prop>())
    {
        if (!propAvailableDic.ContainsKey(prop.propName))
        {
            propAvailableDic.Add(prop.propName, true);
        }
        else
        {
            prop.gameObject.SetActive(propAvailableDic[prop.propName]);
        }
    }
    foreach (Interactive interactive in FindObjectsOfType<Interactive>())
    {
        if (!interactiveStateDic.ContainsKey(interactive.name))
        {
            interactiveStateDic.Add(interactive.name, interactive.isDone);
        }
        else
        {
            interactive.isDone = interactiveStateDic[interactive.name];
        }
    }
}
```

查看代码时发现可交互物体mailbox也订阅了AfterSceneLoadedEvent事件，加载场景时存在数据依赖，可能出现问题

于是进行测试 代码如下

测试结果发现各个方法的调用存在先后关系 先订阅的方法先通知 后订阅的后通知 由于ObjectManager始终存在，订阅时间一定早于进入场景后再订阅的mailbox，因此一定是先进行mailbox状态的赋值再进行更新状态，不会出现问题。

```c#
using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    private event Action testevent;

    private int x = 1;

    private void Start()
    {
        testevent += Log;
        testevent += Change;//输出1 如果交换语句顺序 则输出2
    }

    public void Call()
    {
        x = 1;
        testevent.Invoke();
    }

    public void Change()
    {
        x = 2;
    }

    public void Log()
    {
        Debug.LogWarning(x);
    }
}
```

**总结：event先通知先订阅的方法，后通知后订阅的方法**

### 游戏外存档

利用PlayerPrefabs和JsonUtility存档

发现List和dictionnary无法存档，上网查找需要写序列化脚本实现序列化

list无法直接序列化，利用脚本将其包装即可

而dictionary将其拆为两个list，利用ISerializationCallbackReceiver将其从list转换为dictionary或者dictionary转为list进行序列化

存档 读档利用两个event实现

SaveManager  **实现list dictionary的load时忘记加上out导致未传递回值，读档失败**

```c#
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MySingleton<SaveManager>
{
    public void Save(Object data, string key)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
    }

    public void Save<T>(List<T> data, string key)
    {
        //Debug.LogWarning("save list");
        string jsonData = JsonUtility.ToJson(new Serialization<T>(data));
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load<T>(out List<T> data, string key)//注意需要加上out
    {
        //Debug.LogWarning("load list");
        if (PlayerPrefs.HasKey(key))
            data = JsonUtility.FromJson<Serialization<T>>(PlayerPrefs.GetString(key)).ToList();
        else
            data = new List<T>();
    }

    public void Save<TKey, TValue>(Dictionary<TKey, TValue> data, string key)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<TKey,TValue>(data));
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
        Debug.LogWarning("prop save" + key + " " + data.Count);
    }

    public void Load<TKey, TValue>(out Dictionary<TKey, TValue> data, string key)
    {
        if (PlayerPrefs.HasKey(key))
            data = JsonUtility.FromJson<Serialization<TKey, TValue>>(PlayerPrefs.GetString(key)).ToDictionary();
        else
            data = new Dictionary<TKey, TValue>();
        Debug.LogWarning(PlayerPrefs.HasKey(key) + "prop load" + key + " " + data.Count);
    }
}
// List<T>
[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    private List<T> target;

    public List<T> ToList()
    { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

// Dictionary<TKey, TValue>
[System.Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys;

    [SerializeField]
    private List<TValue> values;

    private Dictionary<TKey, TValue> target;

    public Dictionary<TKey, TValue> ToDictionary()
    { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Mathf.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}
```

参考：

[(31条消息) Unity中JsonUtility对List和Dictionary的序列化_拿起键盘就是干的博客-CSDN博客](https://blog.csdn.net/Truck_Truck/article/details/78292390)

[UnityEngine.ISerializationCallbackReceiver - Unity 脚本参考 (unity3d.com)](https://docs.unity3d.com/ja/current/ScriptReference/ISerializationCallbackReceiver.html)