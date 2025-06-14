using ES;
using ES.EvPointer;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

namespace ES
{

    public class ESPreviewWindow : ESWindowBase_Abstract<ESPreviewWindow>
    {
        #region 数据滞留
        public Page_ShowSystemICON systemICON;
        public Page_CacheObjects cacheObjects;
        #endregion
        [MenuItem("Tools/ES工具/ES预览窗口")]
        public static void TryOpenWindow()
        {
            if (ESEditorRuntimePartMaster.Instance != null)
                OpenWindow();
            else Debug.LogError("确保场景中有ESEditorRuntimePartMaster");
        }
        
        protected override void ES_BuildMenuTree(OdinMenuTree tree)
        {
            base.ES_BuildMenuTree(tree);
            tree.Add("系统图标预览", systemICON ??= new Page_ShowSystemICON());
            MakePage_CacheObjects(tree, "缓存物体信息");
        }

        private void MakePage_CacheObjects(OdinMenuTree tree,string Menu)
        {
            tree.Add(Menu, cacheObjects ??= new Page_CacheObjects());
            Menu += "/";
            HashSet<GameObject> hasGS = new HashSet<GameObject>(Objects.Count);
            HashSet<UnityEngine.Object> hasOS = new HashSet<UnityEngine.Object>(Objects.Count);

            for (int i=0;i<  Objects.Count;i++)
            {
                Debug.Log(i);
                var ii = Objects[i];
                if (ii == null || hasOS.Contains(ii)) return;
                string ss = ii.name;
                if(ii is MonoBehaviour mo)
                {
                    GameObject gg = mo.gameObject;
                    if (hasGS.Contains(gg))
                    {

                    }
                    else
                    {
                        hasGS.Add(gg);
                        tree.Add(Menu+"游戏物体:" +mo.gameObject.name, new Page_Index_Object() { Object = gg });

                    }
                    ss = Menu + "游戏物体:" + mo.gameObject.name + "/" + "*脚本"+(ii.GetType());
                }
                hasOS.Add(ii);
                tree.Add(ss,new Page_Index_Object() { Object=ii });
            }
        }

        public override void ES_LoadData()
        {
            base.ES_LoadData();
            icons = new List<SystemICON>(100);
            foreach(var i in UnityEditorIcons.UnityEditorIconNames.AllChinese.Keys)
            {
                try
                {
                    string en = UnityEditorIcons.UnityEditorIconNames.AllChinese[i];
                    Texture tt = EditorGUIUtility.IconContent(en)?.image;
                    if (tt != null)
                    {

                        icons.Add(new SystemICON() { chi = i, eng = en, texture = tt });
                    }
                }
                catch (Exception e)
                {

                }
                
            }
            if (PlayerPrefs.HasKey(keyForCache))
            {
                Debug.Log("开始加载GUID");
                pathForCache = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString(keyForCache));
                if (pathForCache != null&&pathForCache.Count>0)
                {
                    Debug.Log("GUIDS"+pathForCache+pathForCache.Count);
                    Objects = new List<UnityEngine.Object>(pathForCache.Count);
                    foreach(var i in pathForCache)
                    {

                        Debug.Log("加载" + i);
                        if(int.TryParse(i,out var id))
                        {
                            Objects.Add(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(i)));
                        }
                        
                    }

                }
                else
                {
                    Debug.Log("空的");
                }
            }
        }
        public override void ES_SaveData()
        {
            base.ES_SaveData();
            pathForCache = new List<string>(Objects.Count);
            Debug.Log("开始保存GUID");
            foreach (var i in Objects)
            {
               
                string path =i.GetInstanceID().ToString();
                pathForCache.Add(path);
                Debug.Log("保存" + i+path);
            }
            PlayerPrefs.SetString(keyForCache,JsonConvert.SerializeObject(pathForCache,formatting: Formatting.Indented));
            PlayerPrefs.Save();
        }
        #region 存储
        
        public static List<SystemICON> icons = new List<SystemICON>();
        private static List<string> pathForCache = new List<string>();
        public static List<UnityEngine.Object> Objects = new List<UnityEngine.Object>();
        public static string keyForCache = "window-preview-cache-pathForCache";
        #endregion
    }
    [Serializable, TypeRegistryItem("系统ICON")]
    public class SystemICON
    {
        [LabelText("中文"),ReadOnly,VerticalGroup("a")]
        [TableColumnWidth(50)]
        public string chi;
        [LabelText("英文"), ReadOnly, VerticalGroup("a")]
        public string eng;
        [LabelText("图标"), ReadOnly, VerticalGroup("a")]
        public Texture texture;
    }
    #region 预览图表集
    [Serializable]
    public class Page_ShowSystemICON
    {
        [ShowInInspector, HideLabel, InlineProperty,TableList(ShowIndexLabels = true, HideToolbar = false, AlwaysExpanded = true)]        
        public List<SystemICON> show {get=>ESPreviewWindow.icons;set { } }
        public Page_ShowSystemICON()
        {
           
            
        }
    }

    [Serializable,TypeRegistryItem("缓存物体表")]
    public class Page_CacheObjects
    {
        [ShowInInspector]
        public List<UnityEngine.Object> objects => ESPreviewWindow.Objects;
    }
    [Serializable]
    public class Page_Index_Object
    {
        [HideLabel, DisplayAsString(fontSize: 24), ShowInInspector,PropertyOrder(-1)]
        public string ss => "操作物体"+Object.name;
        [InlineEditor,HideLabel()]
        public UnityEngine.Object Object;
    }
    #endregion
}

