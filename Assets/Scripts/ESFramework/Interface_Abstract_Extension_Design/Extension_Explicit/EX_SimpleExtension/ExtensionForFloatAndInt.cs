using ES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace ES
{

    public static class ExtensionForFloatAndInt
    {
        public static float _SafeDivide(this float f, float b)
        {
            if (b == 0) b = 1;
            return f / b;
        }
        // 1. 检查float是否在范围内
        public static bool _InRange(this float f, Vector2 range)
        {
            return f >= range.x && f <= range.y;
        }

        // 2. 检查int是否在范围内
        public static bool _InRange(this int i, Vector2Int range)
        {
            return i >= range.x && i <= range.y;
        }

        // 3. 限制数值在最小最大值之间
        public static float _Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        // 4. 检查float是否接近0
        public static bool _IsApproximatelyZero(this float f, float threshold = 0.001f)
        {
            return Mathf.Abs(f) < threshold;
        }

        // 5. 将角度限制在0-360度
        public static float _AsNormalizeAngle(this float angle)
        {
            angle %= 360f;
            if (angle < 0) angle += 360f;
            return angle;
        }

        // 6. 将角度限制在-180到180度
        public static float _AsNormalizeAngle180(this float angle)
        {
            angle %= 360f;
            if (angle > 180f) angle -= 360f;
            if (angle < -180f) angle += 360f;
            return angle;
        }

        // 7. 检查两个float是否近似相等
        public static bool _IsApproximately(this float a, float b, float threshold = 0.001f)
        {
            return Mathf.Abs(a - b) < threshold;
        }

        // 8. 将值映射到新范围
        public static float _Remap(this float value, float fromMin, float fromMax, float toMin=0, float toMax=1)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
        public static string _FormatToDecimalPlaces(this float num, int digits)
        {
            return num.ToString($"F{digits}");
        }
        public static string _ToPercentage(this float num)
        {
            return $"{(num * 100):0}%";
        }
        // 9. 检查int是否为偶数
        public static bool _IsEven(this int i)
        {
            return i % 2 == 0;
        }

        // 10. 检查int是否为奇数
        public static bool _IsOdd(this int i)
        {
            return i % 2 != 0;
        }
        public static string _ToDateOrdinal(this int num)
        {
            if (num % 100 / 10 == 1) return $"{num}th";
            switch (num % 10)
            {
                case 1: return $"{num}st";
                case 2: return $"{num}nd";
                case 3: return $"{num}rd";
                default: return $"{num}th";
            }
        }
        public static string _ToFormattedString(this float num)
        {
            return num.ToString("#,0.##");
        }
        public static string _ToRoman(this int num)
        {
            var romanNumerals = new (int, string)[]
            {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
            };

            var result = new StringBuilder();
            foreach (var (value, numeral) in romanNumerals)
            {
                while (num >= value)
                {
                    result.Append(numeral);
                    num -= value;
                }
            }
            return result.ToString();
        }
        public static IEnumerable<int> _RangeInts(this int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }
        public static string _ToMoneyFormat(this int num)
        {
            return num.ToString("C", CultureInfo.CurrentCulture);
        }

        public static bool _IsCloseToZero(this float num, float tolerance = 0.0001f)
        {
            return Math.Abs(num) < tolerance;
        }
        public static float _LerpTo(this float start, float end, float t)
        {
            return start + (end - start) * t;
        }
    }
}

