using ES.EvPointer;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ES
{
    [Serializable]
    public class Link_AttackHappen : LinkDirect<int, int, int>
    {

    }
    [Serializable]
    public struct Link_SelfDefine : ILink
    {
        public string name_;
    }

    [Serializable, TypeRegistryItem]
    public struct Link_DestroyWhy : ILink
    {
        [LabelText("销毁原因")]
        public EnumCollect.SetFlyingDestroyWhyOption options;
    }

    [Serializable]
    public struct Link_aaa : ILink
    {

    }
    [Serializable]
    public struct Link_bbb: ILink
    {

    }
    public class LinkTable : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
