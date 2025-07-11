using ES.EvPointer;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ES
{
    public class Architecture_ : MonoBehaviour
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
    [Serializable,TypeRegistryItem("标准原型-拥有事件收发和配置")]
    public class BaseArchitectureWithLinkAndConfiguration :BaseESHosting, IArchitecture
    {
        
        
        [LabelText("Link池")]public LinkReceivePool LinkIOC = new LinkReceivePool();
        public string Name_ => description;
        


        [LabelText("<<↑原型>>", SdfIconType.CpuFill),GUIColor("descriptionGUIColor_"), ShowInInspector,PropertyOrder(-1)]
        public string description="标准原型";
        [HideInInspector]public Color descriptionGUIColor_ =new Color(1f,0.588f,1f,1);
        public void AddReceiveLink<Link>(IReceiveLink<Link> link) where Link:ILink
        {
            LinkIOC.AddReceive<Link>(link);
        }
        public void RemoveReceiveLink<Link>(IReceiveLink<Link> link) where Link : ILink
        {
            LinkIOC.RemoveReceive<Link>(link);
        }
        public void SendLink<Link>(Link link)where Link:ILink
        {
            LinkIOC.SendLink(link);
        }

        public override void TryAddToIEnumableOnly(IESModule module)
        {
            throw new NotImplementedException();
        }

        public override void TryRemoveFromIEnumableOnly(IESModule module)
        {
            throw new NotImplementedException();
        }

        public override void TryDestroySelf()
        {
            throw new NotImplementedException();
        }
    }
    //一个架构内的Message，具有一组Link，一组针池
    //原型具有一个 Link的IOC 
    [Serializable, TypeRegistryItem("标准原型-键池")]
    public class BaseArchitectureWithKeyValuePool :BaseESHosting, IArchitecture,IInittable
    {
        public string Name_ => description;
        

        

        [LabelText("<<↑原型>>", SdfIconType.CpuFill), GUIColor("descriptionGUIColor_"), ShowInInspector, PropertyOrder(-1)]
        public string description = "标准键针池原型";
        [HideInInspector] public Color descriptionGUIColor_ = new Color(1f, 0.588f, 1f, 1);
       

        [LabelText("键值参数池"),SerializeReference]
        public ArchitectureKeyValuePoolTypeListIOC KeyValueParamIOC = new ArchitectureKeyValuePoolTypeListIOC();
        [LabelText("参数初始化数据包")]
        public ArchitectureDataPack applyPack;
        
        [DetailedInfoBox("为何是预览??","IOC不支持持久序列化，仅支持运行时，所以已经准备好一个数据包，在Start时载入数据来初始化,这样做的话也有利于一套数据多次使用，并且有望实现减少运行时数据量(只存动态标签即可)",InfoMessageType.Warning)]
        [Button("初始化预览")]
        public void Init(params object[] ps)
        {
            KeyValueParamIOC ??= new ArchitectureKeyValuePoolTypeListIOC();
            //
            if (!KeyValueParamIOC.Groups.ContainsKey(EnumCollect.ArchitectureKeyValuePoolType.DynamicTag))
            {
                KeyValueParamIOC.Groups.Add( EnumCollect.ArchitectureKeyValuePoolType.DynamicTag,new List<IArchitectureKeyValuePoolTypeValue>());
            }
            if (!KeyValueParamIOC.Groups.ContainsKey(EnumCollect.ArchitectureKeyValuePoolType.FloatValue))
            {
                KeyValueParamIOC.Groups.Add(EnumCollect.ArchitectureKeyValuePoolType.FloatValue, new List<IArchitectureKeyValuePoolTypeValue>());
            }
            if (!KeyValueParamIOC.Groups.ContainsKey(EnumCollect.ArchitectureKeyValuePoolType.IntValue))
            {
                KeyValueParamIOC.Groups.Add(EnumCollect.ArchitectureKeyValuePoolType.IntValue, new List<IArchitectureKeyValuePoolTypeValue>());
            }
            if (!KeyValueParamIOC.Groups.ContainsKey(EnumCollect.ArchitectureKeyValuePoolType.BoolValue))
            {
                KeyValueParamIOC.Groups.Add(EnumCollect.ArchitectureKeyValuePoolType.BoolValue, new List<IArchitectureKeyValuePoolTypeValue>());
            }
            if (!KeyValueParamIOC.Groups.ContainsKey(EnumCollect.ArchitectureKeyValuePoolType.StringValue))
            {
                KeyValueParamIOC.Groups.Add(EnumCollect.ArchitectureKeyValuePoolType.StringValue, new List<IArchitectureKeyValuePoolTypeValue>());
            }
            if(applyPack != null)
            {
                foreach(var i in applyPack.allInfos)
                {
                    var arch = i.Value.getArch;
                    if(arch is ArchitectureTypeValue_DynamicTag tag)
                    {
                        KeyValueParamIOC.TryAdd(EnumCollect.ArchitectureKeyValuePoolType.DynamicTag, tag);
                    }else if (arch is ArchitectureTypeValue_Float f)
                    {
                        KeyValueParamIOC.TryAdd(EnumCollect.ArchitectureKeyValuePoolType.FloatValue, f);
                    }
                    else if (arch is ArchitectureTypeValue_Int int_)
                    {
                        KeyValueParamIOC.TryAdd(EnumCollect.ArchitectureKeyValuePoolType.IntValue, int_);
                    }
                    else if (arch is ArchitectureTypeValue_String str)
                    {
                        KeyValueParamIOC.TryAdd(EnumCollect.ArchitectureKeyValuePoolType.StringValue, str);
                    }
                    else if (arch is ArchitectureTypeValue_Bool bo)
                    {
                        KeyValueParamIOC.TryAdd(EnumCollect.ArchitectureKeyValuePoolType.BoolValue, bo);
                    }
                  /*  else if (arch is ArchitectureTypeValue_DynamicTag g)
                    {
                        KeyValueParamIOC.AddElement(EnumCollect.ArchitectureKeyValuePoolType.DynamicTag, arch);
                    }*/
                }
            }
        }

        public override void TryAddToIEnumableOnly(IESModule module)
        {
            throw new NotImplementedException();
        }

        public override void TryRemoveFromIEnumableOnly(IESModule module)
        {
            throw new NotImplementedException();
        }

        public override void TryDestroySelf()
        {
            throw new NotImplementedException();
        }
    }
    public interface IArchitecture : IESHosting
    {
        public abstract string Name_ { get; }
    }
    
    public interface IWithArchitecture 
    {
        public abstract IArchitecture GetArchitecture { get; }
    }
}

#region 测试圆形


#endregion