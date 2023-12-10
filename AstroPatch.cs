using System;
using System.Reflection;
using System.Text;
using HarmonyLib;
using MelonLoader;


#region Imports

#endregion Imports

/// <summary>
/// Wrapper for Harmony Patching.
/// </summary>
internal class AstroPatch
{
    private bool isDevMode { get; } = true;
    private string PatchIdentifier { get; } = "AstroPatch";
    internal MethodInfo TargetMethod_MethodInfo { get; set; }

    internal MethodBase TargetMethod_MethodBase { get; set; }
    internal string HarmonyInstanceID { get; set; }
    internal HarmonyMethod Prefix { get; set; }
    internal HarmonyMethod PostFix { get; set; }
    internal HarmonyMethod Transpiler { get; set; }
    internal HarmonyMethod Finalizer { get; set; }

    internal HarmonyMethod IlManipulator { get; set; }
    internal HarmonyLib.Harmony Instance { get; set; }
    private bool HasThrownException { get; set; } = false;
    private bool ShowErrorOnConsole { get; set; } = true;
    private bool ShowSuccessFulPatches { get; set; } = true;

    private bool ShowFailedPatches { get; set; } = true;
    internal bool isActivePatch { get; private set; } = false;
    private bool isMethodInfoPatch { get; set; } = false;

    internal string TargetPath_MethodInfo => $"{TargetMethod_MethodInfo?.DeclaringType?.FullName}.{TargetMethod_MethodInfo?.Name}";
    internal string TargetPath_base => $"{TargetMethod_MethodInfo?.DeclaringType?.FullName}.{TargetMethod_MethodBase.Name}";

    internal string PatchType
    {
        get
        {
            StringBuilder patchtype = new StringBuilder();
            if (PostFix != null)
            {
                string patch = $"PostFix Patch : {PostFix.method?.DeclaringType?.FullName}.{PostFix.method?.Name} ";
                if (patchtype.Length != 0)
                {
                    patchtype.AppendLine(patch);
                }
                else
                {
                    patchtype.Append(patch);
                }
            }

            if (Prefix != null)
            {
                string patch = $"Prefix Patch : {Prefix.method?.DeclaringType?.FullName}.{Prefix.method?.Name} ";
                if (patchtype.Length != 0)
                {
                    patchtype.AppendLine(patch);
                }
                else
                {
                    patchtype.Append(patch);
                }
            }

            if (Transpiler != null)
            {
                string patch = $"Transpiler Patch : {Transpiler.method?.DeclaringType?.FullName}.{Transpiler.method?.Name} ";
                if (patchtype.Length != 0)
                {
                    patchtype.AppendLine(patch);
                }
                else
                {
                    patchtype.Append(patch);
                }
            }

            if (Finalizer != null)
            {
                string patch = $"Finalizer Patch : {Finalizer.method?.DeclaringType?.FullName}.{Finalizer.method?.Name} ";
                if (patchtype.Length != 0)
                {
                    patchtype.AppendLine(patch);
                }
                else
                {
                    patchtype.Append(patch);
                }
            }

            if (IlManipulator != null)
            {
                string patch = $"IlManipulator Patch : {IlManipulator.method?.DeclaringType?.FullName}.{IlManipulator.method?.Name} ";
                if (patchtype.Length != 0)
                {
                    patchtype.AppendLine(patch);
                }
                else
                {
                    patchtype.Append(patch);
                }
            }

            if (patchtype.Length == 0)
            {
                return "Failed to Read Patch.";
            }

            return patchtype.ToString();
        }
    }

    internal AstroPatch(MethodInfo TargetMethod, HarmonyMethod Prefix = null, HarmonyMethod PostFix = null, HarmonyMethod Transpiler = null, HarmonyMethod Finalizer = null, HarmonyMethod ILmanipulator = null, bool showErrorOnConsole = true, bool ShowFailedPatches = true, bool ShowSuccessFulPatches = true)
    {
        if (TargetMethod == null || (Prefix == null && PostFix == null && Transpiler == null && Finalizer == null && ILmanipulator == null))
        {
            StringBuilder FailureReason = new StringBuilder();
            if (Prefix == null)
            {
                string reason = "Prefix Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (PostFix == null)
            {
                string reason = "PostFix Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (Transpiler == null)
            {
                string reason = "Transpiler Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (Finalizer == null)
            {
                string reason = "Finalizer Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (ILmanipulator == null)
            {
                string reason = "ILmanipulator Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (TargetMethod != null)
            {
                MelonLogger.Error($"[{PatchIdentifier}] TargetMethod is NULL");
            }
            else
            {
                if (isDevMode)
                {
                    MelonLogger.Error($"[{PatchIdentifier}] Failed to Patch {TargetMethod.DeclaringType?.FullName}.{TargetMethod?.Name} because {FailureReason}.");
                }
                else
                {
                    MelonLogger.Error($"[{PatchIdentifier}] Failed to Patch {TargetMethod.Name}");
                }
            }

            return;
        }

        this.TargetMethod_MethodInfo = TargetMethod;
        this.Prefix = Prefix;
        this.PostFix = PostFix;
        this.Transpiler = Transpiler;
        this.Finalizer = Finalizer;
        this.IlManipulator = ILmanipulator;
        this.ShowErrorOnConsole = showErrorOnConsole;
        this.ShowFailedPatches = ShowFailedPatches;
        this.ShowSuccessFulPatches = ShowSuccessFulPatches;
        this.HarmonyInstanceID = $"{PatchIdentifier}: {TargetPath_MethodInfo}, {PatchType}";
        this.isMethodInfoPatch = true;
        this.Instance = new HarmonyLib.Harmony(HarmonyInstanceID);
        this.DoPatch_info(this);
    }

    internal AstroPatch(MethodBase TargetMethod, HarmonyMethod Prefix = null, HarmonyMethod PostFix = null, HarmonyMethod Transpiler = null, HarmonyMethod Finalizer = null, HarmonyMethod ILmanipulator = null, bool showErrorOnConsole = true, bool ShowFailedPatches = true, bool ShowSuccessFulPatches = true)
    {
        if (TargetMethod == null || (Prefix == null && PostFix == null && Transpiler == null && Finalizer == null && ILmanipulator == null))
        {
            StringBuilder FailureReason = new StringBuilder();
            if (Prefix == null)
            {
                string reason = "Prefix Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (PostFix == null)
            {
                string reason = "PostFix Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (Transpiler == null)
            {
                string reason = "Transpiler Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (Finalizer == null)
            {
                string reason = "Finalizer Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (ILmanipulator == null)
            {
                string reason = "ILmanipulator Method is null";
                if (FailureReason.Length != 0)
                {
                    FailureReason.AppendLine(reason);
                }
                else
                {
                    FailureReason.Append(reason);
                }
            }

            if (TargetMethod != null)
            {
                MelonLogger.Error($"[{PatchIdentifier}] TargetMethod is NULL");
            }
            else
            {
                if (isDevMode)
                {
                    MelonLogger.Error($"[{PatchIdentifier}] Failed to Patch {TargetMethod.DeclaringType?.FullName}.{TargetMethod?.Name} because {FailureReason}.");
                }
                else
                {
                    MelonLogger.Error($"[{PatchIdentifier}] Failed to Patch {TargetMethod.Name}");
                }
            }

            return;
        }

        this.TargetMethod_MethodBase = TargetMethod;
        this.Prefix = Prefix;
        this.PostFix = PostFix;
        this.Transpiler = Transpiler;
        this.Finalizer = Finalizer;
        this.IlManipulator = ILmanipulator;
        this.ShowErrorOnConsole = showErrorOnConsole;
        this.ShowSuccessFulPatches = ShowSuccessFulPatches;
        this.ShowFailedPatches = ShowFailedPatches;
        this.HarmonyInstanceID = $"{PatchIdentifier}: {TargetPath_MethodInfo}, {PatchType}";
        this.isMethodInfoPatch = false;
        this.Instance = new HarmonyLib.Harmony(HarmonyInstanceID);
        DoPatch_base(this);
    }

    private void DoPatch_info(AstroPatch patch)
    {
        try
        {
            patch.Instance.Patch(patch.TargetMethod_MethodInfo, patch.Prefix, patch.PostFix, patch.Transpiler, patch.Finalizer, patch.IlManipulator);
        }
        catch (Exception e)
        {
            HasThrownException = true;
            if (ShowErrorOnConsole)
            {
                MelonLogger.Error(e);
            }
        }
        finally
        {
            if (!HasThrownException)
            {
                isActivePatch = true;
                if (ShowSuccessFulPatches)
                {
                    if (isDevMode)
                    {
                        MelonLogger.Msg($"[{patch.PatchIdentifier}] Patched {patch.TargetPath_MethodInfo} | with {patch.PatchType}");
                    }
                    else
                    {
                        MelonLogger.Msg($"[{patch.PatchIdentifier}] Patched {patch.TargetMethod_MethodInfo?.Name}");
                    }
                }
            }
            else
            {
                isActivePatch = false;
                if (ShowFailedPatches)
                {
                    if (isDevMode)
                    {
                        MelonLogger.Error($"[{patch.PatchIdentifier}] Failed At {patch.TargetPath_MethodInfo} | with {patch.PatchType}");
                    }
                    else
                    {
                        MelonLogger.Error($"[{patch.PatchIdentifier}] Failed At {patch.TargetMethod_MethodInfo?.Name}");
                    }
                }
            }
        }
    }

    private void DoPatch_base(AstroPatch patch)
    {
        try
        {
            patch.Instance.Patch(patch.TargetMethod_MethodBase, patch.Prefix, patch.PostFix, patch.Transpiler, patch.Finalizer, patch.IlManipulator);
        }
        catch (Exception e)
        {
            HasThrownException = true;
            if (ShowErrorOnConsole)
            {
                MelonLogger.Error(e);
            }
        }
        finally
        {
            if (!HasThrownException)
            {
                isActivePatch = true;
                if (ShowSuccessFulPatches)
                {
                    if (isDevMode)
                    {
                        MelonLogger.Msg($"[{patch.PatchIdentifier}] Patched {patch.TargetPath_base} | with {patch.PatchType}");
                    }
                    else
                    {
                        MelonLogger.Msg($"[{patch.PatchIdentifier}] Patched {patch.TargetMethod_MethodBase?.Name}");
                    }
                }
            }
            else
            {
                isActivePatch = false;
                if (ShowFailedPatches)
                {
                    if (isDevMode)
                    {
                        MelonLogger.Error($"[{patch.PatchIdentifier}] Failed At {patch.TargetPath_base} | with {patch.PatchType}");
                    }
                    else
                    {
                        MelonLogger.Error($"[{patch.PatchIdentifier}] Failed At {patch.TargetMethod_MethodBase?.Name}");
                    }
                }
            }
        }
    }

    internal void Unpatch()
    {
        if (isActivePatch)
        {
            this.Instance.UnpatchSelf();
            if (!this.isMethodInfoPatch)
            {
                if (isDevMode)
                {
                    MelonLogger.Msg($"[{this.PatchIdentifier}] Removed Patch from {this.TargetPath_base} , Unlinked Method : {this.PatchType}");
                }
                else
                {
                    MelonLogger.Msg($"[{this.PatchIdentifier}] Removed Patch from {this.TargetMethod_MethodBase?.Name}");
                }
            }
            else
            {
                if (isDevMode)
                {
                    MelonLogger.Msg($"[{this.PatchIdentifier}] Removed Patch from {this.TargetPath_MethodInfo} , Unlinked Method : {this.PatchType}");
                }
                else
                {
                    MelonLogger.Msg($"[{this.PatchIdentifier}] Removed Patch from {this.TargetMethod_MethodInfo?.Name}");
                }
            }
            isActivePatch = false;
        }
    }

    internal void Patch()
    {
        if (!isActivePatch)
        {
            if (isMethodInfoPatch)
            {
                this.DoPatch_info(this);
            }
            else
            {
                this.DoPatch_base(this);
            }
        }
    }
}