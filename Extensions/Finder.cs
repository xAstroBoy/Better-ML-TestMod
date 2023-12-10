namespace Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MelonLoader;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class Finder
    {
        /// <summary>
        /// This bypasses Also the Inactive Object bug that Unity has when using .find being unable to find inactive objects.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject Find(string path)
        {
            if (path.IsNotNullOrEmptyOrWhiteSpace())
            {
                // Get all paths
                string[] paths = path.Split('/');
                // Get the root path
                string RootPath = paths[0];
                // get the rest of the child path minus the root
                string[] ChildPaths = paths.Skip(1).ToArray();
                // Get the root gameobject
                var Root = GameObject.Find(RootPath);
                // If the root gameobject is null, return null
                if (Root == null)
                {
                    Root = FindRootSceneObject(RootPath);
                    if (Root == null)
                    {
                        MelonLogger.Error($"[ERROR (Find) ]  Gameobject on path [ {path} ]  is Invalid, No Root Object Found!");
                        return null;
                    }
                }
                if (ChildPaths.Length == 0)
                {
                    return Root;
                }
                // convert the rest of the childs into a string
                string ChildPath = string.Join("/", ChildPaths);
                // Find the child gameobject
                var result = Root.FindObject(ChildPath, true);
                if (result == null)
                {
                    MelonLogger.Error($"[ERROR (Find) ]  Gameobject on path [ {path} ]  is Invalid, No Child Object Found!");
                    return null;
                }
                return result;
            }
            return null;
        }

        public static List<GameObject> RootSceneObjects => SceneManager.GetActiveScene().GetRootGameObjects().ToList();

        public static List<T> GetRootGameObjectsComponents<T>(bool IncludeInactive = true) where T : Component
        {
            try
            {
                var results = new List<T>();
                for (int i = 0; i < RootSceneObjects.Count; i++)
                {
                    GameObject obj = RootSceneObjects[i];
                    var objects = obj.GetComponentsInChildren<T>(IncludeInactive);
                    if (objects.Count() != 0)
                    {
                        for (int i1 = 0; i1 < objects.Count(); i1++)
                        {
                            T component = objects[i1];
                            if (!results.Contains(component))
                            {
                                results.Add(component);
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                MelonLogger.Error("Error parsing Components from Root Objects");
                MelonLogger.Error(e);
                return null;
            }
        }

        public static GameObject FindRootSceneObject(string name)
        {
            var list = SceneManager.GetActiveScene().GetRootGameObjects();

            for (int i = 0; i < list.Count(); i++)
            {
                var obj = list[i];
                if (obj != null && obj.name.Equals(name))
                {
                    return obj;
                }
            }
            MelonLogger.Warning($"[WARNING (FindRootSceneObject) ]  Root Gameobject name [ {name} ]  is Invalid, No Object Found!");
            return null;
        }


        public static GameObject FindObject(this GameObject gameobject, string path, bool DontWarn = false)
        {
            if (gameobject == null) return null;
            var obj = gameobject.transform.Find(path);
            if (obj == null)
            {
                if (!DontWarn)
                {
                    MelonLogger.Warning($"[WARNING (FindObject) ]  Transform {gameobject.name} Doesnt have a object in path [ {path} ] !");
                }
                return null;
            }

            return obj.gameObject;
        }


        public static Transform FindObject(this Transform transform, string path, bool DontWarn = false)
        {
            if (transform == null) return null;
            var obj = transform.Find(path);
            if (obj == null)
            {
                if (!DontWarn)
                {
                    MelonLogger.Warning($"[WARNING (FindObject) ]  Transform {transform.name} Doesnt have a object in path [ {path} ] !");
                }
                return null;
            }

            return obj;
        }

        public static List<GameObject> ListFind(string path)
        {
            List<GameObject> list = new List<GameObject>();

            var obj = GameObject.Find(path);
            if (obj != null)
            {
                var list1 = obj.GetComponentsInChildren<Transform>();
                for (int i = 0; i < list1.Count(); i++)
                {
                    Transform item = list1[i];
                    list.AddGameObject(item.gameObject);
                }
                return list;
            }
            else
            {
                MelonLogger.Warning($"[WARNING (ListFind) ] Gameobject on path [ {path} ]  is Invalid, No Object Found!");
                return list;
            }
        }
    }
}