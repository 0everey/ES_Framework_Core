using ES;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test501 : SerializedMonoBehaviour
{

    public Stack<Attackable> aaa = new Stack<Attackable>();

    public Stack<Attackable> aaa2 = new Stack<Attackable>();

    /* public List<int> source;
     public string[] source1;
     public Dictionary<int,Link_AttackHappen> source2;
     public HashSet<Link_EntityAttackEntityTruely> source3;
     public Queue<GameObject> source4;
     public LinkedList<_deepcloneT> source5;
     public Stack<float> source6;
     [Space(25)]
     public List<int> target;
     public string[] target1;
     public Dictionary<int,Link_AttackHappen> target2;
     public HashSet<Link_EntityAttackEntityTruely> target3;
     public Queue<GameObject> target4;
     public Stack<float> target6;
     public LinkedList<_deepcloneT> target5;*/
    [Button("TEST深拷贝")]
    public void Test()
    {
        aaa2 = KeyValueMatchingUtility.Creator.DeepCloneGenericStack(aaa) as Stack<Attackable>;
       /* target = KeyValueMatchingUtility.Creator.DeepClone(source);
        target1 = KeyValueMatchingUtility.Creator.DeepClone(source1);
        target2 = KeyValueMatchingUtility.Creator.DeepClone(source2);
        target3 = KeyValueMatchingUtility.Creator.DeepClone(source3);
     
        target4 = KeyValueMatchingUtility.Creator.DeepClone(source4);
        target6 = KeyValueMatchingUtility.Creator.DeepClone(source6);
        target5 = KeyValueMatchingUtility.Creator.DeepClone(source5);*/
    }
    private void Update()
    {
       
        for (int i = 0; i < 100_000; i++)
        {
            //隐式调用
          /*  _deepcloneT _DeepcloneT_ = KeyValueMatchingUtility.Creator.DeepClone(source5);
*/
            //显示调用IDeepClone
            //_deepcloneT _DeepcloneT = new _deepcloneT();
        }
    }
    [Serializable]
    public class _deepcloneT : IDeepClone<_deepcloneT>
    {
        public float ff;
        public float bb;
        public float not;
        public GameObject prefab;
        public void DeepCloneFrom(_deepcloneT t)
        {
            ff = t.ff;
            bb = t.bb;
            prefab = t.prefab;
        }
    }
}
