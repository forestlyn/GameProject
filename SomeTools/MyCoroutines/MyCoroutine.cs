using System.Collections;

namespace MyTools.MyCoroutines
{
    public sealed class MyCoroutine
    {
        private IEnumerator enumerator;

        private string m_key;
        /// <summary>
        /// 用于标识协程
        /// </summary>
        public string Key { get => m_key; }
        public ICoroutineYield Current => GetCoroutineYield(enumerator.Current);
        public MyCoroutine(IEnumerator enumerator)
        {
            m_key = enumerator.GetHashCode().ToString();
            this.enumerator = enumerator;
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        private ICoroutineYield GetCoroutineYield(object current)
        {
            if (current == null)
            {
                return new YieldReturnNULL();
            }
            else if (current is YieldWaitForSeconds)
            {
                return current as YieldWaitForSeconds;
            }
            return new YieldReturnNULL();
        }
    }
    /// <summary>
    /// 返回值需要实现的接口
    /// </summary>
    public interface ICoroutineYield
    {
        /// <summary>
        /// 当前yield return 是否完成
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool IsDone(float deltaTime);
    }

    public class YieldReturnNULL : ICoroutineYield
    {
        public YieldReturnNULL() { }
        public bool IsDone(float deltaTime)
        {
            return true;
        }
    }

    public class YieldWaitForSeconds : ICoroutineYield
    {
        private float m_time;
        public YieldWaitForSeconds(float delayTime) { m_time = delayTime; }
        public bool IsDone(float deltaTime)
        {
            m_time -= deltaTime;
            return m_time <= 0;
        }
    }
}

