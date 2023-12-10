namespace Utils.Extensions
{
    using MelonLoader;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class ListExtensions
    {
        public static bool AnyAndNotNull<T>(this List<T> list) => list != null && list.Any();

        public static void RegisterChildsInPath(this List<GameObject> Original, string path)
        {
            var list = Finder.ListFind(path);
            if (list != null)
            {
                if (list.Count() != 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        GameObject obj = list[i];
                        Original.AddGameObject(obj);
                    }
                }
            }
        }

        public static void AddString(this List<string> list, string text)
        {
            if (list != null && !string.IsNullOrEmpty(text))
            {
                if (!list.Contains(text))
                {
                    list.Add(text);
                }
            }
            else
            {
                if (list == null)
                {
                    MelonLogger.Msg($"Error, failed to add string {text} because {nameof(list).ToString()} is null.");
                    return;
                }
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
            }
        }

        public static void RemoveString(this List<string> list, string text)
        {
            if (list != null && !string.IsNullOrEmpty(text))
            {
                if (list.Contains(text))
                {
                    list.Remove(text);
                }
            }
            else
            {
                if (list == null)
                {
                    MelonLogger.Msg($"Error, failed to remove string {text} because {nameof(list).ToString()} is null.");
                    return;
                }
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
            }
        }

        public static void AddGameObject(this List<GameObject> list, GameObject obj)
        {
            if (list != null && obj != null)
            {
                if (!list.Contains(obj))
                {
                    list.Add(obj);
                }
            }
            else
            {
                if (list == null)
                {
                    MelonLogger.Msg($"Error, failed to add gameobject {obj.name} because {nameof(list).ToString()} is null.");
                    return;
                }
                if (obj == null)
                {
                    return;
                }
            }
        }

        public static void RemoveGameobject(this List<GameObject> list, GameObject obj)
        {
            if (list != null && obj != null)
            {
                if (list.Contains(obj))
                {
                    list.Remove(obj);
                }
            }
            else
            {
                if (list == null)
                {
                    MelonLogger.Msg($"Error, failed to remove gameobject {obj.name} because {nameof(list).ToString()} is null.");
                }
                if (obj == null)
                {
                    return;
                }
            }
        }


        public static void DestroyAndClearList(this List<GameObject> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    GameObject item = list[i];
                    if (item != null)
                    {
                        Object.Destroy(item);
                    }
                }
                list.Clear();
            }


            else
            {
                if (list == null)
                {
                    MelonLogger.Msg($"Error, failed to Destroy gameobjects  in {nameof(list).ToString()} is null.");
                    return;
                }
            }
        }

    }
}