namespace Utils.Extensions
{
    using System.Linq;
    using UnityEngine;

    internal static class ComponentUtils
    {
        #region GameObject

        internal static T GetGetInChildrens_OrAddComponent<T>(this GameObject obj, bool IncludeInactive = false) where T : Component
        {
            if (obj == null) return null;
            var result = obj.GetComponent<T>();
            if (result == null)
                result = obj.GetComponentInChildren<T>(IncludeInactive);
            if (result == null)
                result = obj.AddComponent<T>();
            return result;
        }

        internal static T GetGetInChildrens<T>(this GameObject obj, bool IncludeInactive = false) where T : Component
        {
            if (obj == null) return null;
            var result = obj.GetComponent<T>();
            if (result == null)
                result = obj.GetComponentInChildren<T>(IncludeInactive);
            return result;
        }

        internal static T GetGetInChildrens_OrParent<T>(this GameObject obj, bool IncludeInactive = false) where T : Component
        {
            if (obj == null) return null;
            var result = obj.GetComponent<T>();
            if (result == null)
                result = obj.GetComponentInChildren<T>(IncludeInactive);
            if (result == null)
                result = obj.GetComponentInParent<T>();
            return result;
        }

        internal static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            if (obj == null) return null;
            var result = obj.GetComponent<T>();
            if (result == null)
                result = obj.AddComponent<T>();
            return result;
        }

        internal static void RemoveComponent<T>(this GameObject obj) where T : Component
        {
            if (obj != null)
            {
                var existing = obj.GetComponent<T>();
                if (existing)
                {
                    existing.DestroyMeLocal();
                }
            }
        }

        internal static void RemoveComponents<T>(this GameObject Parent) where T : Component
        {
            if (Parent == null) return;
            var ParentComp = Parent.GetComponents<T>();
            if (ParentComp != null)
            {
                if (ParentComp.Count() != 0)
                {
                    foreach (var comp in ParentComp)
                    {
                        if (comp != null)
                        {
                            comp.DestroyMeLocal(true);
                        }
                    }
                }
            }
            var childs1 = Parent.GetComponentsInChildren<T>(true);
            if (childs1 != null)
            {
                if (childs1.Count() != 0)
                {
                    foreach (var comp in childs1)
                    {
                        if (comp != null)
                        {
                            comp.DestroyMeLocal(true);
                        }
                    }
                }
            }
        }

        public static void SetComponentState<T>(this GameObject Parent, bool enabled) where T : Behaviour
        {
            if (Parent == null) return;
            var ParentComp = Parent.GetComponents<T>();
            if (ParentComp != null)
            {
                if (ParentComp.Count() != 0)
                {
                    foreach (var comp in ParentComp)
                    {
                        if (comp != null)
                        {
                            comp.enabled = enabled;
                        }
                    }
                }
            }
        }

        public static void ActivateComponents<T>(this GameObject Parent) where T : Behaviour
        {
            if (Parent == null) return;
            var ParentComp = Parent.GetComponents<T>();
            if (ParentComp != null)
            {
                if (ParentComp.Count() != 0)
                {
                    foreach (var comp in ParentComp)
                    {
                        if (comp != null)
                        {
                            comp.enabled = true;
                        }
                    }
                }
            }

            foreach (var child in Parent.transform.Get_All_Childs())
            {
                var comps = child.GetComponents<T>();
                if (comps == null) continue;
                if (comps.Length != 0) continue;
                foreach (var comp in comps)
                {
                    if (comp != null)
                    {
                        comp.enabled = true;
                    }
                }
            }
        }

        internal static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            if (obj != null)
            {
                var existing = obj.GetComponent<T>();
                if (existing)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion GameObject

        #region Transform

        internal static T GetGetInChildrens_OrAddComponent<T>(this Transform obj, bool IncludeInactive = false) where T : Component
        {
            return obj.gameObject.GetGetInChildrens_OrAddComponent<T>(IncludeInactive);
        }

        internal static T GetGetInChildrens<T>(this Transform obj, bool IncludeInactive = false) where T : Component
        {
            return obj.gameObject.GetGetInChildrens<T>(IncludeInactive);
        }

        internal static T GetGetInChildrens_OrParent<T>(this Transform obj, bool IncludeInactive = false) where T : Component
        {
            return obj.gameObject.GetGetInChildrens_OrParent<T>(IncludeInactive);
        }

        internal static T GetOrAddComponent<T>(this Transform obj) where T : Component
        {
            return obj.gameObject.GetOrAddComponent<T>();
        }

        internal static void RemoveComponent<T>(this Transform obj) where T : Component
        {
            obj.gameObject.RemoveComponent<T>();
        }

        internal static void RemoveComponents<T>(this Transform obj) where T : Component
        {
            obj.gameObject.RemoveComponents<T>();
        }

        internal static bool HasComponent<T>(this Transform obj) where T : Component
        {
            return obj.gameObject.HasComponent<T>();
        }

        #endregion Transform

        #region component

        internal static T AddComponent<T>(this Component c) where T : Component
        {
            return c.gameObject.AddComponent<T>();
        }

        internal static T GetGetInChildrens_OrAddComponent<T>(this Component obj, bool IncludeInactive = false) where T : Component
        {
            return obj.transform.GetGetInChildrens_OrAddComponent<T>(IncludeInactive);
        }

        internal static T GetGetInChildrens<T>(this Component obj, bool IncludeInactive = false) where T : Component
        {
            return obj.transform.GetGetInChildrens<T>(IncludeInactive);
        }

        internal static T GetGetInChildrens_OrParent<T>(this Component obj, bool IncludeInactive = false) where T : Component
        {
            return obj.transform.GetGetInChildrens_OrParent<T>(IncludeInactive);
        }

        internal static T GetOrAddComponent<T>(this Component obj) where T : Component
        {
            return obj.transform.GetOrAddComponent<T>();
        }

        internal static void RemoveComponent<T>(this Component obj) where T : Component
        {
            obj.transform.RemoveComponent<T>();
        }

        internal static void RemoveComponents<T>(this Component obj) where T : Component
        {
            obj.transform.RemoveComponents<T>();
        }

        internal static bool HasComponent<T>(this Component obj) where T : Component
        {
            return obj.transform.HasComponent<T>();
        }

        #endregion component

    }
}