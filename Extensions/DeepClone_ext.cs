namespace Utils.Extensions
{
    using MelonLoader;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    internal static class DeepClone_ext
    {
        private static bool GetCopyOfDebugMode = false;

        private static void DebugGetCopyOf(string msg)
        {
            if (GetCopyOfDebugMode)
            {
                MelonLogger.Warning("GetCopyOf Debug :" + msg);
            }
        }

        private const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

        internal static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            
            if (comp == null) return null;
            if (other.GetType() == null) return null;
            Type type = comp.GetType();

            if (type != other.GetType())
            {
                MelonLogger.Error("Type Mis-match");
                return null; // type mis-match
            }

            DebugGetCopyOf("GetCopyOf 1");
            List<Type> derivedTypes = new List<Type>();
            Type derived = type.BaseType;
            DebugGetCopyOf("GetCopyOf 2");

            while (derived != null)
            {
                if (derived != null)
                {
                    if (derived == typeof(MonoBehaviour))
                    {
                        break;
                    }
                    derivedTypes.Add(derived);
                    derived = derived.BaseType;
                }
            }
            DebugGetCopyOf("GetCopyOf 3");
            IEnumerable<PropertyInfo> pinfos = type.GetProperties(bindingFlags);
            foreach (Type derivedType in derivedTypes.Where(derivedType => derivedType != null))
            {
                pinfos = pinfos.Concat(derivedType.GetProperties(bindingFlags));
            }

            DebugGetCopyOf("GetCopyOf 4");
            pinfos = from property in pinfos
                     where !(type == typeof(Rigidbody) && property.Name == "inertiaTensor") // Special case for Rigidbodies inertiaTensor which isn't catched for some reason.
                     where !property.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ObsoleteAttribute))
                     select property;
            DebugGetCopyOf("GetCopyOf 5");
            foreach (PropertyInfo pinfo in pinfos.Where(pinfo => pinfo != null).Where(pinfo => pinfo.CanWrite))
            {
                if (pinfos.Any(e => e.Name == $"shared{char.ToUpper(pinfo.Name[0])}{pinfo.Name.Substring(1)}"))
                {
                    continue;
                }

                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { }// In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }

            DebugGetCopyOf("GetCopyOf 6");

            IEnumerable<FieldInfo> finfos = type.GetFields(bindingFlags);
            DebugGetCopyOf("GetCopyOf 7");

            foreach (var finfo in finfos)
            {
                if (finfo != null)
                {
                    foreach (Type derivedType in derivedTypes)
                    {
                        if (derivedType != null)
                        {
                            if (finfos.Any(e => e.Name == $"shared{char.ToUpper(finfo.Name[0])}{finfo.Name.Substring(1)}"))
                            {
                                continue;
                            }
                            finfos = finfos.Concat(derivedType.GetFields(bindingFlags));
                        }
                    }
                }
            }
            DebugGetCopyOf("GetCopyOf 8");
            foreach (var finfo in finfos.Where(finfo => finfo != null))
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }

            DebugGetCopyOf("GetCopyOf 9");

            finfos = from field in finfos
                     where field.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ObsoleteAttribute))
                     select field;
            DebugGetCopyOf("GetCopyOf 10");
            foreach (var finfo in finfos.Where(finfo => finfo != null))
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }

            DebugGetCopyOf("GetCopyOf End");
            return comp as T;
        }

        internal static T CopyComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            if (go == null)
            {
                MelonLogger.Msg("CopyComponent GameObject Go is null");
            }
            if (toAdd == null)
            {
                MelonLogger.Msg("CopyComponent toAdd is null");
            }
            return go.AddComponent<T>().GetCopyOf(toAdd);
        }
    }
}