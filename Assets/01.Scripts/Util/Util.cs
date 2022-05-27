using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Util
{
    public static string StringAppend(params string[] strArr)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        for (int i = 0; i < strArr.Length; ++i)
        {
            if (strArr[i] == "" ||
                strArr[i] == string.Empty ||
                strArr[i] == null)
            {
                continue;
            }

            builder.Append(strArr[i]);
        }

        return builder.ToString();
    }

    public static void DrawHandlesRect(Vector2 center, Vector2 size, Color color)
    {
#if UNITY_EDITOR
        Vector2 extents = size * 0.5f;

        Vector3 topLeft = new Vector3(center.x - extents.x, center.y + extents.y);
        Vector3 topRight = new Vector3(center.x + extents.x, center.y + extents.y);
        Vector3 bottomLeft = new Vector3(center.x - extents.x, center.y - extents.y);
        Vector3 bottomRight = new Vector3(center.x + extents.x, center.y - extents.y);

        Handles.color = color;

        Handles.DrawLine(topLeft, topRight);
        Handles.DrawLine(topLeft, bottomLeft);
        Handles.DrawLine(topRight, bottomRight);
        Handles.DrawLine(bottomLeft, bottomRight);
#endif
    }

    public static void DrawDebugRect(Rect rect)
    {
        Vector3 topLeft = new Vector3(rect.xMin, rect.yMin, 0f);
        Vector3 topRight = new Vector3(rect.xMax, rect.yMin, 0f);
        Vector3 bottomLeft = new Vector3(rect.xMin, rect.yMax, 0f);
        Vector3 bottomRight = new Vector3(rect.xMax, rect.yMax, 0f);

        Debug.DrawLine(topLeft, topRight);
        Debug.DrawLine(bottomLeft, bottomRight);
        Debug.DrawLine(topLeft, bottomLeft);
        Debug.DrawLine(topRight, bottomRight);
    }

    public static Renderer GetRendererComponent(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null)
            renderer = gameObject.GetComponentInChildren<Renderer>();

        return renderer;
    }

    public static List<Renderer> GetRendererComponents(GameObject gameObject)
    {
        List<Renderer> rendererList = new List<Renderer>();

        AddRendererList(gameObject, rendererList);

        return rendererList;
    }

    public static T GetComponentHierarchy<T>(GameObject gameObject) where T : UnityEngine.Component
    {
        T component = gameObject.GetComponent<T>();

        if (component != null)
            return component;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            component = GetComponentHierarchy<T>(child.gameObject);
            if (component != null)
                return component;
        }

        return null;
    }

    public static List<T> GetComponentsHierarchy<T>(GameObject gameObject) where T : UnityEngine.Component
    {
        List<T> list = new List<T>();

        T[] components = gameObject.GetComponents<T>();

        if (components != null)
            list.AddRange(components);

        components = gameObject.GetComponentsInChildren<T>();

        if (components != null)
            list.AddRange(components);

        return list;
    }

    private static void AddRendererList(GameObject gameObject, List<Renderer> rendererList)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
            rendererList.Add(renderer);

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            AddRendererList(gameObject.transform.GetChild(i).gameObject, rendererList);
        }
    }

    public static Animator GetAnimatorComponent(GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator == null)
            animator = gameObject.GetComponentInChildren<Animator>();

        return animator;
    }

    /// <summary>
    ///  자신과 자식오브젝트의 태그 변경
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tag"></param>
    public static void SetTag(GameObject gameObject, string tag)
    {
        gameObject.tag = tag;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            SetTag(child.gameObject, tag);
        }
    }

    /// <summary>
    /// 자신과 자식오브젝트의 레이어변경
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="layer"></param>
    public static void SetLayer(GameObject gameObject, string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }

    public static void SetLayer(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }

    public static void SetHideFlag(GameObject gameObject, HideFlags flags)
    {
        gameObject.hideFlags = flags;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            SetHideFlag(child.gameObject, flags);
        }
    }

    public static Vector3 GetRendererBounds(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            renderer = gameObject.GetComponentInChildren<Renderer>();
        }

        if (renderer != null)
        {
            return renderer.bounds.size;
        }

        return Vector3.zero;
    }

    public static void ChangeMeshAndMaterial(Transform oriBone, SkinnedMeshRenderer oriRenderer, SkinnedMeshRenderer newRenderer, Material material)
    {
        Transform[] newBones = new Transform[newRenderer.bones.Length];

        for (int i = 0; i < newRenderer.bones.Length; i++)
            newBones[i] = Util.FindChildByName(oriBone, newRenderer.bones[i].name);

        oriRenderer.bones = newBones;
        oriRenderer.sharedMesh = newRenderer.sharedMesh;
        oriRenderer.material = material;
    }

    public static Transform FindChildByName(Transform current, string name)
    {
        if (current.name == name)
            return current;

        for (int i = 0; i < current.childCount; ++i)
        {
            Transform found = FindChildByName(current.GetChild(i), name);
            if (found != null)
                return found;
        }

        return null;
    }

    public static bool PercentageCheck(int percentage)
    {
        if (percentage == 100)
            return true;

        percentage = 100 - percentage;

        int rand = UnityEngine.Random.Range(0, 100);

        if (percentage <= rand)
            return true;
        else
            return false;
    }

    public static T GetRandomListItem<T>(List<T> list)
    {
        int rand = UnityEngine.Random.Range(0, list.Count);
        return list[rand];
    }

    public static AnimationClip GetAnimationClip(Animator animator, string name)
    {
        if (animator == null)
            return null;

        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            if (animator.runtimeAnimatorController.animationClips[i].name == name)
                return animator.runtimeAnimatorController.animationClips[i];
        }

        return null;
    }

    public static string[] GetEnumDescriptionArray(Type type)
    {
        Array enumArray = System.Enum.GetValues(type);

        if (enumArray == null)
            return null;

        string[] descriptionArray = new string[enumArray.Length];

        int count = 0;
        foreach (var value in enumArray)
        {
            var memInfo = type.GetMember(value.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            string description = ((DescriptionAttribute)attributes[0]).Description;
            descriptionArray[count] = description;
            count++;
        }

        return descriptionArray;
    }

    public static string GetEnumDescription(Enum value)
    {
        System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

    /// <summary>
    /// 에셋경로를 Resources에서 로드가능한 경로로 바꾼다
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string AssetPathToResourcesPath(string path)
    {
        int index = path.IndexOf("Resources/");
        if (index == -1)
        {
            return null;
        }
        else
        {
            path = path.Substring(index + 10);
        }

        return RemoveFileExtension(path);

    }
    public static string RemoveFileExtension(string fileName)
    {
        int index = fileName.LastIndexOf(".");
        if (index != -1)
            return fileName.Substring(0, index);

        return fileName;
    }

    public static int FindIndex(Enum selectIdx, Type type)
    {
        Array enumArray = System.Enum.GetValues(type);

        for (int i = 0; i < enumArray.Length; i++)
        {
            if (selectIdx.ToString().CompareTo((enumArray.GetValue(i)).ToString()) == 0)
                return i;
        }

        return -1;
    }

    public static T StringToEnum<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }

    public static Enum IndexToType(int index, Type type)
    {
        Array enumArray = System.Enum.GetValues(type);

        if (index < 0 || index >= enumArray.Length)
            return (Enum)enumArray.GetValue(0);

        return (Enum)enumArray.GetValue(index);
    }

    public static TKey GetKeyByValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue value)
    {
        foreach (KeyValuePair<TKey, TValue> item in dictionary)
        {
            if (item.Value.Equals(value))
                return item.Key;
        }
        return default(TKey);
    }

    public static System.Collections.Hashtable MakeHash(params object[] args)
    {
        System.Collections.Hashtable hashTable = new System.Collections.Hashtable(args.Length / 2);
        if (args.Length % 2 != 0)
        {
            Debug.LogError("Hash requires an even number of arguments");
            return null;
        }
        else
        {
            int i = 0;
            while (i < args.Length - 1)
            {
                hashTable.Add(args[i], args[i + 1]);
                i += 2;
            }
            return hashTable;
        }
    }

    /// <summary>
    /// float 자료형을 ushort로
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="minTarget"></param>
    /// <param name="maxTarget"></param>
    /// <returns></returns>
    public static ushort ScaleFloatToUShort(float value, float minValue, float maxValue, ushort minTarget, ushort maxTarget)
    {
        // ScaleFloatToUShort( -1f, -1f, 1f, ushort.MinValue, ushort.MaxValue) => 0
        // ScaleFloatToUShort(  0f, -1f, 1f, ushort.MinValue, ushort.MaxValue) => 32767
        // ScaleFloatToUShort(0.5f, -1f, 1f, ushort.MinValue, ushort.MaxValue) => 49151
        // ScaleFloatToUShort(  1f, -1f, 1f, ushort.MinValue, ushort.MaxValue) => 65535

        // note: C# ushort - ushort => int, hence so many casts
        int targetRange = maxTarget - minTarget; // max ushort - min ushort > max ushort. needs bigger type.
        float valueRange = maxValue - minValue;
        float valueRelative = value - minValue;
        return (ushort)(minTarget + (ushort)(valueRelative / valueRange * (float)targetRange));
    }

    public static byte ScaleFloatToByte(float value, float minValue, float maxValue, byte minTarget, byte maxTarget)
    {
        // ScaleFloatToByte( -1f, -1f, 1f, byte.MinValue, byte.MaxValue) => 0
        // ScaleFloatToByte(  0f, -1f, 1f, byte.MinValue, byte.MaxValue) => 127
        // ScaleFloatToByte(0.5f, -1f, 1f, byte.MinValue, byte.MaxValue) => 191
        // ScaleFloatToByte(  1f, -1f, 1f, byte.MinValue, byte.MaxValue) => 255

        // note: C# byte - byte => int, hence so many casts
        int targetRange = maxTarget - minTarget; // max byte - min byte only fits into something bigger
        float valueRange = maxValue - minValue;
        float valueRelative = value - minValue;
        return (byte)(minTarget + (byte)(valueRelative / valueRange * (float)targetRange));
    }

    public static float ScaleUShortToFloat(ushort value, ushort minValue, ushort maxValue, float minTarget, float maxTarget)
    {
        // ScaleUShortToFloat(    0, ushort.MinValue, ushort.MaxValue, -1, 1) => -1
        // ScaleUShortToFloat(32767, ushort.MinValue, ushort.MaxValue, -1, 1) => 0
        // ScaleUShortToFloat(49151, ushort.MinValue, ushort.MaxValue, -1, 1) => 0.4999924
        // ScaleUShortToFloat(65535, ushort.MinValue, ushort.MaxValue, -1, 1) => 1

        // note: C# ushort - ushort => int, hence so many casts
        float targetRange = maxTarget - minTarget;
        ushort valueRange = (ushort)(maxValue - minValue);
        ushort valueRelative = (ushort)(value - minValue);
        return minTarget + (float)((float)valueRelative / (float)valueRange * targetRange);
    }

    public static float ScaleByteToFloat(byte value, byte minValue, byte maxValue, float minTarget, float maxTarget)
    {
        // ScaleByteToFloat(  0, byte.MinValue, byte.MaxValue, -1, 1) => -1
        // ScaleByteToFloat(127, byte.MinValue, byte.MaxValue, -1, 1) => -0.003921569
        // ScaleByteToFloat(191, byte.MinValue, byte.MaxValue, -1, 1) => 0.4980392
        // ScaleByteToFloat(255, byte.MinValue, byte.MaxValue, -1, 1) => 1

        // note: C# byte - byte => int, hence so many casts
        float targetRange = maxTarget - minTarget;
        byte valueRange = (byte)(maxValue - minValue);
        byte valueRelative = (byte)(value - minValue);
        return minTarget + (float)((float)valueRelative / (float)valueRange * targetRange);
    }

    public static ushort PackThreeFloatsIntoUShort(float u, float v, float w, float minValue, float maxValue)
    {
        // eulerAngles have 3 floats, putting them into 2 bytes of [x,y],[z,0]
        // would be a waste. instead we compress into 5 bits each => 15 bits.
        // so a ushort.

        // 5 bits max value = 1+2+4+8+16 = 31 = 0x1F
        byte lower = ScaleFloatToByte(u, minValue, maxValue, 0x00, 0x1F);
        byte middle = ScaleFloatToByte(v, minValue, maxValue, 0x00, 0x1F);
        byte upper = ScaleFloatToByte(w, minValue, maxValue, 0x00, 0x1F);
        ushort combined = (ushort)(upper << 10 | middle << 5 | lower);
        return combined;
    }

    public static float[] UnpackUShortIntoThreeFloats(ushort combined, float minTarget, float maxTarget)
    {
        // see PackThreeFloatsIntoUShort for explanation

        byte lower = (byte)(combined & 0x1F);
        byte middle = (byte)((combined >> 5) & 0x1F);
        byte upper = (byte)(combined >> 10); // nothing on the left, no & needed

        // note: we have to use 4 bits per float, so between 0x00 and 0x0F
        float u = ScaleByteToFloat(lower, 0x00, 0x1F, minTarget, maxTarget);
        float v = ScaleByteToFloat(middle, 0x00, 0x1F, minTarget, maxTarget);
        float w = ScaleByteToFloat(upper, 0x00, 0x1F, minTarget, maxTarget);
        return new float[] { u, v, w };
    }

    public static T CopyComponent<T>(T original, GameObject destination) where T : UnityEngine.Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }

    public static void SafeInvoke(Action action)
    {
        if (action != null)
            action();
    }

    public static void SafeInvoke<T>(Action<T> action, T param)
    {
        if (action != null)
            action(param);
    }

    public static void SafeInvoke<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
    {
        if (action != null)
            action(param1, param2);
    }

    public static void SafeInvoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
    {
        if (action != null)
            action(param1, param2, param3);
    }

    public static string SecondToMinuteString(int value)
    {
        int minute = value / 60;
        int second = value % 60;

        if (minute > 0)
        {
            return string.Format("{0} : {1:D2}", minute, second);
        }
        else
        {
            return second.ToString();
        }
    }

    public static void ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

    public static T CreateGameObject<T>() where T : MonoBehaviour
    {
        return CreateGameObject<T>(null, Vector3.zero, Quaternion.identity);
    }

    public static T CreateGameObject<T>(Transform parent) where T : MonoBehaviour
    {
        return CreateGameObject<T>(parent, Vector3.zero, Quaternion.identity);
    }

    public static T CreateGameObject<T>(Transform parent, Vector3 pos) where T : MonoBehaviour
    {
        return CreateGameObject<T>(parent, pos, Quaternion.identity);
    }

    public static T CreateGameObject<T>(Transform parent, Vector3 pos, Quaternion rot) where T : MonoBehaviour
    {
        GameObject obj = new GameObject();

        if (parent != null)
        {
            obj.transform.SetParent(parent);
            obj.transform.localPosition = pos;
            obj.transform.localRotation = rot;
        }

        obj.name = typeof(T).Name;
        return obj.AddComponent<T>();
    }

}