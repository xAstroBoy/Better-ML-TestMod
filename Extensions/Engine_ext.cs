using System;
using System.Globalization;
using System.Text;


namespace Utils.Extensions
{
    #region Imports

    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using MelonLoader;
    using UnityEngine;
    using Utils.Colors;
    using Color = System.Drawing.Color;

    #endregion Imports

    internal static class Engine_ext
    {
        internal static void DestroyChildren(this Transform parent)
        {
            for (var i = parent.childCount; i > 0; i--)
                UnityEngine.Object.DestroyImmediate(parent.GetChild(i - 1).gameObject);
        }

        internal static GameObject NoUnload(this GameObject obj)
        {
            obj.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return obj;
        }

        internal static void PrintPath(this GameObject obj)
        {
            if (obj != null)
            {
                string path = obj.GetPath();
                if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
                {
                    MelonLogger.Msg($"{obj.name} Path is : {path}");
                }
            }
        }
        /// <summary>
        /// This gets the GameObject path 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static string GetPath(this GameObject obj)
        {
            return GetPath(obj.transform);
        }
        /// <summary>
        /// This gets the Transform path
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        internal static string GetPath(this Transform current)
        {
            if (current.parent == null)
                return current.name;
            return GetPath(current.parent) + "/" + current.name;
        }

        internal static string AddRichColorTag(this string text, Color color)
        {
            return $"<color=#{ColorUtils.ColorToHex(color)}>{text}</color>";

        }
        internal static string AddRichColorTag(this string text, UnityEngine.Color color)
        {
            return $"<color=#{ColorUtils.ColorToHex(color)}>{text}</color>";

        }

        // HTML Rainbow codes
        internal static string RainbowRichText(this string text)
        {
            var sb = new StringBuilder();
            var rainbow = new List<string>
            {
                "FF0000",
                "FF1F00",
                "FF3D00",
                "FF5C00",
                "FF7A00",
                "FF9900",
                "FFB800",
                "FFD600",
                "FFF500",
                "EBFF00",
                "CCFF00",
                "ADFF00",
                "8FFF00",
                "70FF00",
                "52FF00",
                "33FF00",
                "14FF00",
                "00FF0A",
                "00FF29",
                "00FF47",
                "00FF66",
                "00FF85",
                "00FFA3",
                "00FFC2",
                "00FFE0",
                "00FFFF",
                "00E0FF",
                "00C2FF",
                "00A3FF",
                "0085FF",
                "0066FF",
                "0047FF",
                "0029FF",
                "000AFF",
                "1400FF",
                "3300FF",
                "5200FF",
                "7000FF",
                "8F00FF",
                "AD00FF",
                "CC00FF",
                "EB00FF",
                "FF00F5",
                "FF00D6",
                "FF00B8",
                "FF0099",
                "FF007A",
                "FF005C",
            };
            var index = 0;
            foreach (var letter in text)
            {
                sb.Append($"<color=#{rainbow[index]}>{letter}</color>");
                index = (index + 1) % rainbow.Count();
            }

            return sb.ToString();
        }

        public static Vector3 SetZ(this Vector3 vector, float Z)
        {
            vector.Set(vector.x, vector.y, Z);
            return vector;
        }
        public static Vector3 SetY(this Vector3 vector, float Y)
        {
            vector.Set(vector.x, Y, vector.z);
            return vector;
        }
        public static Vector3 SetX(this Vector3 vector, float X)
        {
            vector.Set(X, vector.y, vector.z);
            return vector;
        }
        public static float RoundAmount(this float i, float nearestFactor) => (float)Math.Round((double)i / (double)nearestFactor) * nearestFactor;

        public static Vector3 RoundAmount(this Vector3 i, float nearestFactor) => new Vector3(i.x.RoundAmount(nearestFactor), i.y.RoundAmount(nearestFactor), i.z.RoundAmount(nearestFactor));

        public static Vector2 RoundAmount(this Vector2 i, float nearestFactor) => new Vector2(i.x.RoundAmount(nearestFactor), i.y.RoundAmount(nearestFactor));


        internal static void FlipTransformRotation(this  Transform transform)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            transform.rotation = Quaternion.Euler(rot);

        }

        internal static UnityEngine.Color Get_Transform_Active_ToColor(this Transform obj)
        {
            return obj.gameObject.Get_GameObject_Active_ToColor();
        }

        internal static UnityEngine.Color Get_GameObject_Active_ToColor(this GameObject obj)
        {
            return obj != null ? obj.active ? UnityEngine.Color.green : UnityEngine.Color.red : UnityEngine.Color.red;
        }

        internal static UnityEngine.Color Get_AudioSource_Active_ToColor(this AudioSource obj)
        {
            return obj != null ? obj.enabled ? UnityEngine.Color.green : UnityEngine.Color.red : UnityEngine.Color.red;
        }

        internal static UnityEngine.Color Get_MonoBehaviour_Enabled_ToColor(this MonoBehaviour obj)
        {
            return obj != null ? obj.enabled ? UnityEngine.Color.green : UnityEngine.Color.red : UnityEngine.Color.red;
        }

        internal static bool? Is_DontDestroyOnLoad(this GameObject obj)
        {
            return obj != null ? obj.scene.name.Equals("DontDestroyOnLoad") : (bool?)null;
        }

        internal static bool? Is_DontDestroyOnLoad(this Transform obj)
        {
            return obj?.gameObject.Is_DontDestroyOnLoad();
        }

        internal static void Set_DontDestroyOnLoad(this Object obj)
        {
            UnityEngine.Object.DontDestroyOnLoad(obj);
        }

        internal static bool? Is_HideAndDontSave(this GameObject obj)
        {
            try
            {
                return obj != null ? obj.scene.name.Equals("HideAndDontSave") : (bool?)null;
            }
            catch { return false; }
        }

        internal static bool? Is_HideAndDontSave(this Transform obj)
        {
            return obj?.gameObject.Is_HideAndDontSave();
        }

        internal static bool? Is_CurrentWorld(this GameObject obj)
        {
            return obj != null ? !obj.Is_HideAndDontSave().Value && !obj.Is_DontDestroyOnLoad().Value : (bool?)null;
        }

        internal static bool? Is_CurrentWorld(this Transform obj)
        {
            return obj?.gameObject.Is_CurrentWorld();
        }

        internal static void CopyPath(this GameObject obj)
        {
            if (obj != null)
            {
                string path = obj.GetPath();
                if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
                {
                    MelonLogger.Msg($"{obj.name} Path is : {path}");
                    MelonLogger.Msg($"The Path has been copied on the clipboard.");
                    Clipboard.SetText(path);
                }
            }
        }

        internal static void CopyRotation(this GameObject obj)
        {
            if (obj != null)
            {
                MelonLogger.Msg($"{obj.name} rotation is : new Quaternion({obj.transform.rotation.x}f, {obj.transform.rotation.y}f, {obj.transform.rotation.z}f, {obj.transform.rotation.w}f)");
                MelonLogger.Msg($"The rotation has been copied on the clipboard.");
                Clipboard.SetText($"new Quaternion({obj.transform.rotation.x}f, {obj.transform.rotation.y}f, {obj.transform.rotation.z}f, {obj.transform.rotation.w}f)");
            }
        }

        internal static void CopyPosition(this GameObject obj)
        {
            if (obj != null)
            {
                MelonLogger.Msg($"{obj.name} position is : new Vector3({obj.transform.position.x}f, {obj.transform.position.y}f, {obj.transform.position.z}f)");
                MelonLogger.Msg($"The Position has been copied on the clipboard.");
                Clipboard.SetText($"new Vector3({obj.transform.position.x}f, {obj.transform.position.y}f, {obj.transform.position.z}f)");
            }
        }

        internal static void CopyLocalPosition(this GameObject obj)
        {
            if (obj != null)
            {
                MelonLogger.Msg($"{obj.name} Local position is : new Vector3({obj.transform.localPosition.x}f, {obj.transform.localPosition.y}f, {obj.transform.localPosition.z}f)");
                MelonLogger.Msg($"The Local Position has been copied on the clipboard.");
                Clipboard.SetText($"new Vector3({obj.transform.localPosition.x}f, {obj.transform.localPosition.y}f, {obj.transform.localPosition.z}f)");
            }
        }

        internal static void CopyLocalRotation(this GameObject obj)
        {
            if (obj != null)
            {
                MelonLogger.Msg($"{obj.name} localRotation is : new Quaternion({obj.transform.localRotation.x}f, {obj.transform.localRotation.y}f, {obj.transform.localRotation.z}f, {obj.transform.localRotation.w}f)");
                MelonLogger.Msg($"The localRotation has been copied on the clipboard.");
                Clipboard.SetText($"new Quaternion({obj.transform.localRotation.x}f, {obj.transform.localRotation.y}f, {obj.transform.localRotation.z}f, {obj.transform.localRotation.w}f)");
            }
        }

        internal static void DestroyObject(this GameObject obj)
        {
            obj.DestroyMeLocal();
        }

        internal static void CopyExistingComponents<T>(GameObject Original, GameObject Clone) where T : Component
        {
            var comp1base = Original.GetComponents<T>();
            for (var CompResult = 0; CompResult < comp1base.Count(); CompResult++)
            {
                var result = comp1base[CompResult];
                MelonLogger.Msg($"Deep Cloning {typeof(T).FullName} Component..");
                Clone.AddComponent<T>().GetCopyOf(result);
            }
        }

        internal static void DestroyExistingComponents<T>(GameObject Clone) where T : Component
        {
            var comp1base = Clone.GetComponents<T>();
            for (var CompResult = 0; CompResult < comp1base.Count(); CompResult++)
            {
                var result = comp1base[CompResult];
                if(result != null)
                {
                    Object.DestroyImmediate(result);
                }
            }

        }

        internal static GameObject InstantiateObject(this GameObject original)
        {
            if(original != null)
            {
                var clone = Object.Instantiate(original);
                return clone;
            }
            return null;
        }

        internal static GameObject InstantiateObject(this Transform obj)
        {
            var result = InstantiateObject(obj.gameObject); ;
            return result;
        }

        internal static void DestroyMeLocal<T>(this List<T> items, bool Silent = false) where T : Object
        {
            if(items != null && items.Count != 0)
            {
                foreach(var item in items)
                {
                    if(item != null)
                    {
                        item.DestroyMeLocal(Silent);
                    }
                }
            }
        }


        

        internal static void DestroyMeLocal(this Object obj, bool Silent = false)
        {
            if (obj != null)
            {
                string objname = obj.name;
                string typename = obj.GetType().ToString();

                if (obj is GameObject o)
                {
                    Object.Destroy(o);
                    MiscUtils.DelayFunction(0.5f, () =>
                    {
                        if (o != null)
                        {
                            MelonLogger.Msg($"Failed To Destroy Object {typename} Contained in {objname}", Color.Red);
                        }
                        else
                        {
                            if (!Silent)
                            {
                                MelonLogger.Msg($"Destroyed Client-side Object {typename} Contained in {objname}", Color.Green);
                            }
                        }
                    });
                }
                else if (obj is Transform item)
                {
                    if (item != null)
                    {
                        Object.Destroy(item.gameObject);
                    }
                    MiscUtils.DelayFunction(0.5f, () =>
                    {
                        if (item != null)
                        {
                            MelonLogger.Msg($"Failed To Destroy Object {typename} Contained in {objname}", Color.Red);
                        }
                        else
                        {
                            if (!Silent)
                            {
                                MelonLogger.Msg($"Destroyed Client-side Object {typename} Contained in {objname}", Color.Green);
                            }
                        }
                    });
                }
                else
                {
                    if (obj != null)
                    {
                        Object.Destroy(obj);
                    }
                    MiscUtils.DelayFunction(0.5f, () =>
                    {
                        if (obj != null)
                        {
                            MelonLogger.Msg($"Failed To Destroy Object {typename} Contained in {objname}", Color.Red);
                            MelonLogger.Msg("Try To Destroy His GameObject in case you are trying to destroy the transform.", Color.Yellow);
                        }
                        else
                        {
                            if (!Silent)
                            {
                                MelonLogger.Msg($"Destroyed Client-side Object {typename} Contained in {objname}", Color.Green);
                            }
                        }
                    });
                }
            }
        }

        internal static void RenameObject(this GameObject obj, string newname)
        {
            if (obj != null)
            {
                var oldname = obj.name;
                MelonLogger.Msg("Renamed object : " + oldname + " to " + newname);
                obj.name = newname;
            }
        }


        internal static List<Transform> Get_Childs(this GameObject obj)
        {
            return obj.transform.Get_Childs();
        }

        internal static List<Transform> Get_Childs(this Transform obj)
        {
            List<Transform> childs = new List<Transform>();
            for (var i = 0; i < obj.childCount; i++)
            {
                var item = obj.GetChild(i);
                if (item != null)
                {
                    childs.Add(item);
                }
            }
            return childs;
        }
        /// <summary>
        /// Replaces the prefab content.
        /// </summary>
        /// <param name="Prefab"></param>
        /// <param name="template"></param>
        internal static void ReplacePrefabContent(this GameObject Prefab, GameObject template)
        {
            if (Prefab == null) return;
            if (template == null) return;
            var instantiated = UnityEngine.Object.Instantiate(template, Prefab.transform.position, Prefab.transform.rotation);
            if (instantiated != null)
            {
                foreach (var item in instantiated.Get_Childs())
                {
                    item.parent = Prefab.transform;
                    var name = item.name;
                    item.name += "_UNPACKED";
                    // Then after that, let's take every info inside it
                    var Original = Prefab.transform.FindObject(name);
                    if (Original != null)
                    {
                        // Copy everything off it
                        item.position = Original.position;
                        item.rotation = Original.rotation;
                        item.localScale = Original.localScale;

                        // TODO: Copy the components maybe if needed.
                    }
                }

                // After this remove the empty instantiated transform
                instantiated.DestroyMeLocal(true);
                // Remove the unpacked name or destroy the original childs in the original prefab.

                foreach (var item in Prefab.Get_Childs())
                {
                    if (item.name.EndsWith("_UNPACKED"))
                    {
                        item.name = item.name.Replace("_UNPACKED", string.Empty);
                    }
                    else
                    {
                        item.DestroyMeLocal(true);
                    }
                }


            }

        }

        internal static List<Transform> Get_All_Childs(this Transform item)
        {
            CheckTransform(item);
            return _Transforms;
        }

        private static List<Transform> _Transforms;

        //Recursive
        private static void CheckTransform(Transform transform)
        {
            _Transforms = new List<Transform>();

            //MelonLoader.MelonLogger.MelonLogger.Msg("Debug: Start CheckTransform Recursive Checker");
            if (transform == null)
            {
                MelonLogger.Msg("Debug: CheckTransform transform is null");
                return;
            }

            GetChildren(transform);
        }

        private static void GetChildren(Transform transform)
        {
            //MelonLogger.MelonLogger.Msg("Debug: GetChildren current transform: " + transform.gameObject.name);

            if (!_Transforms.Contains(transform))
            {
                _Transforms.Add(transform);
            }

            for (var i = 0; i < transform.childCount; i++)
            {
                GetChildren(transform.GetChild(i));
            }
        }

        internal static UnityEngine.Color ToUnityEngineColor(this System.Drawing.Color Color)
        {
            return ColorUtils.ToUnityEngineColor(Color);
        }


        internal static string ToNewVector3_String(this Vector3 item)
        {
            return $"new Vector3({item.x.ToString(CultureInfo.InvariantCulture).Replace(",", ".")}f, {item.y.ToString(CultureInfo.InvariantCulture).Replace(",", ".")}f, {item.z.ToString(CultureInfo.InvariantCulture).Replace(",", ".")}f)";
        }

        internal static string ToNewVector2_String(this Vector2 item)
        {
            return $"new Vector2({item.x.ToString(CultureInfo.InvariantCulture).Replace(",", ".")}f, {item.y.ToString(CultureInfo.InvariantCulture).Replace(",", ".")}f)";
        }

        // Convert RaycastHit to string using StringBuilder
        
        internal static string RaycastHitToString(this RaycastHit item)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"Collider collider :{item.collider.name}");
            sb.AppendLine($"Vector3 point :{item.point.ToNewVector3_String()}");
            sb.AppendLine($"Vector3 normal :{item.normal.ToNewVector3_String()}");
            sb.AppendLine($"Vector3 barycentricCoordinate :{item.barycentricCoordinate.ToNewVector3_String()}");
            sb.AppendLine($"float distance :{item.distance}");
            sb.AppendLine($"int triangleIndex :{item.triangleIndex}");
            sb.AppendLine($"Vector2 textureCoord:{item.textureCoord.ToNewVector2_String()}");
            sb.AppendLine($"Vector2 textureCoord2 :{item.textureCoord2.ToNewVector2_String()}");
            sb.AppendLine($"Transform transform :{item.transform.name}");
            sb.AppendLine($"Rigidbody rigidbody:{item.rigidbody.name}");
            sb.AppendLine($"Vector2 lightmapCoord :{item.collider.name}");
            return sb.ToString();
        }

        internal static void SetActiveFull(this GameObject obj, bool Active)
        {
            while (true)
            {
                if (obj != null)
                {
                    obj.gameObject.SetActive(Active);
                    obj = obj.transform.parent.gameObject;
                    continue;
                }

                break;
            }
        }

        internal static void SetActiveFull(this Transform obj, bool Active)
        {
            if (obj != null)
            {
                obj.gameObject.SetActiveFull(Active);
            }
        }


    }
}