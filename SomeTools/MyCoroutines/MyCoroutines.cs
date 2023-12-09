using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MyTools.MyCoroutines
{
    public sealed class MyCoroutines
    {
        private static MyCoroutines instance;
        private MyCoroutines() { }

        private Dictionary<string, List<MyCoroutine>> coroutineDict = new Dictionary<string, List<MyCoroutine>>();

        private static void CreateInstanceIfNotExist()
        {
            if (instance == null)
                instance = new MyCoroutines();
        }
        #region update
        public static void Update(float deltaTime)
        {
            CreateInstanceIfNotExist();
            instance.onUpdate(deltaTime);
        }

        private void onUpdate(float deltaTime)
        {
            if (coroutineDict.Count == 0) return;

            //不要在foreach循环中修改迭代器容器的值
            var keys = new List<string>(coroutineDict.Keys);
            foreach (var key in keys)
            {
                var list = coroutineDict[key];
                for (int i = 0; i < list.Count; i++)
                {
                    var coroutine = list[i];
                    if (!coroutine.Current.IsDone(deltaTime))
                    {
                        continue;
                    }
                    if (!coroutine.MoveNext())
                    {
                        list.RemoveAt(i);
                    }
                }
                if (coroutineDict[key].Count == 0)
                    coroutineDict.Remove(key);
            }
        }
        #endregion

        #region startcoroutine
        public static MyCoroutine StartCoroutine(IEnumerator enumerator)
        {
            return Start(new MyCoroutine(enumerator));
        }
        public static MyCoroutine StartCoroutine(string methodName, object obj)
        {
            return StartCoroutine(methodName, null, obj);
        }
        public static MyCoroutine StartCoroutine(string methodName, object value, object obj)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                return null;
            }
            else
            {
                MethodInfo methodInfo = obj.GetType().GetMethod(methodName);
                if (methodInfo != null)
                {
                    object returnObj;
                    if (value == null)
                        returnObj = methodInfo.Invoke(obj, null);
                    else
                        returnObj = methodInfo.Invoke(obj, new object[] { value });

                    if (returnObj is IEnumerator)
                    {
                        return StartCoroutine(returnObj as IEnumerator);
                    }
                    else
                    {
                        throw new Exception($"{methodName}函数返回值类型不是IEnumerator");
                    }
                }
                else
                {
                    throw new Exception($"在{obj.GetType()}中未找到名为{methodName}函数");
                }
            }
        }

        private static MyCoroutine Start(MyCoroutine coroutine)
        {
            CreateInstanceIfNotExist();
            return instance.startCoroutine(coroutine);
        }
        private MyCoroutine startCoroutine(MyCoroutine coroutine)
        {
            coroutine.MoveNext();
            if (!coroutineDict.ContainsKey(coroutine.Key))
            {
                coroutineDict.Add(coroutine.Key, new List<MyCoroutine>());
            }
            coroutineDict[coroutine.Key].Add(coroutine);
            return coroutine;
        }

        #endregion

        #region stopcoroutine
        public static void StopCoroutine(MyCoroutine coroutine)
        {
            if (instance == null) return;
            instance.stopCoroutine(coroutine);
        }

        public static void StopAllCoroutines()
        {
            if (instance == null) return;
            instance.stopAllCoroutines();
        }
        private void stopCoroutine(MyCoroutine coroutine)
        {
            if (coroutine != null)
            {
                if (coroutineDict.ContainsKey(coroutine.Key))
                {
                    coroutineDict.Remove(coroutine.Key);
                }
            }
        }
        private void stopAllCoroutines()
        {
            coroutineDict.Clear();
        }
        #endregion
    }

}