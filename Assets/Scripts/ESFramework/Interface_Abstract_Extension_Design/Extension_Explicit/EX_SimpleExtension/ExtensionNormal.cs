using ES.EvPointer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace ES
{
    public static partial  class ExtensionNormal
    {
        public static B BePicked<T, P, B>(this T t, P p, object yarn, object on) where P : IPointer<B, T, object, object>
        {
            return p.Pick(t, yarn, on);
        }
        public static B BePicked<T, P, B, Yarn>(this T t, P p, Yarn yarn, object on) where P : IPointer<B, T, Yarn, object>
        {
            return p.Pick(t, yarn, on);
        }
        public static B BePicked<T, P, B, Yarn, On>(this T t, P p, Yarn yarn, On on) where P : IPointer<B, T, Yarn, On>
        {
            return p.Pick(t, yarn, on);
        }
         
       
       
        // 91. 执行Action并返回结果(链式调用)
        public static T Do<T>(this T obj, Action<T> action)
        {
            action?.Invoke(obj);
            return obj;
        }

        // 92. 执行Func并返回结果(链式调用)
        public static TResult Do<T, TResult>(this T obj, Func<T, TResult> func)
        {
            return func != null ? func(obj) : default;
        }

        // 93. 检查对象是否为null
        public static bool _IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        // 94. 检查对象是否不为null
        public static bool _IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }
        public static T _IsNotNullAndUseIt<T>(this T ob) where T:UnityEngine.Object
        {
            if (ob == null) return null;
            return ob;
            /*可以配合?.使用
              someObject.NotNull()?.XXXX();
              确保Miss判定
             */
        }
        public static T _IsNotNullAndUseItWithNullAction<T>(this T ob,Action<T> IfNull) where T : class
        {
            if (ob == null) { IfNull.Invoke(ob); return null; }
            return ob;
            /*可以配合?.使用
              someObject.NotNull()?.XXXX();
              确保Miss判定
             */
        }

        // 95. 如果对象为null则返回默认值
        public static T _IfNullOrDefault<T>(this T obj, T defaultValue) where T : class
        {
            return obj ?? defaultValue;
        }

        // 96. 将对象转换为列表
        public static List<T> _AsListOnlySelf<T>(this T obj)
        {
            return new List<T> { obj };
        }

        // 97. 将对象转换为数组
        public static T[] _AsArrayOnlySelf<T>(this T obj)
        {
            return new T[] { obj };
        }

        // 98. 交换两个变量的值
        public static void _Swap<T>(ref this T a, ref T b) where T: struct
        {
            (a, b) = (b, a);
        }

        // 99. 检查对象是否在数组中
        public static bool _IsIn<T>(this T obj, params T[] array)
        {
            return array.Contains(obj);
        }

        // 100. 检查对象是否在列表中
        public static bool _IsIn<T>(this T obj, IEnumerable<T> collection)
        {
            return collection.Contains(obj);
        }
    }
}
