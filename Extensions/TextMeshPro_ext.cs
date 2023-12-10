using TMPro;

namespace Utils.Extensions
{
    internal static class TextMeshPro_ext
    {
        internal static void CopyFrom(this TextMeshPro instance, TextMeshProUGUI Template)
        {
            if (instance != null && Template != null)
            {
                instance.font = Template.font;
                instance.fontSharedMaterial = Template.fontSharedMaterial;
                instance.fontSharedMaterials = Template.fontSharedMaterials;
                instance.fontMaterial = Template.fontMaterial;
                instance.fontMaterials = Template.fontMaterials;
                instance.fontSize = Template.fontSize;
                instance.fontWeight = Template.fontWeight;
                instance.fontSizeMin = Template.fontSizeMin;
                instance.fontSizeMax = Template.fontSizeMax;
                instance.fontStyle = Template.fontStyle;
                instance.material = Template.material;
                instance.isRightToLeftText = Template.isRightToLeftText;
                instance.richText = Template.richText;
                instance.alpha = Template.alpha;
                instance.enableVertexGradient = Template.enableVertexGradient;
                instance.colorGradient = Template.colorGradient;
                instance.colorGradientPreset = Template.colorGradientPreset;
                instance.spriteAsset = Template.spriteAsset;
                instance.tintAllSprites = Template.tintAllSprites;
                instance.styleSheet = Template.styleSheet;
                instance.textStyle = Template.textStyle;
                instance.overrideColorTags = Template.overrideColorTags;
                instance.faceColor = Template.faceColor;
                instance.outlineColor = Template.outlineColor;
                instance.outlineWidth = Template.outlineWidth;
                instance.enableAutoSizing = Template.enableAutoSizing;
                instance.horizontalAlignment = Template.horizontalAlignment;
                instance.verticalAlignment = Template.verticalAlignment;
                instance.alignment = Template.alignment;
                instance.characterSpacing = Template.characterSpacing;
                instance.wordSpacing = Template.wordSpacing;
                instance.lineSpacing = Template.lineSpacing;
                instance.lineSpacingAdjustment = Template.lineSpacingAdjustment;
                instance.paragraphSpacing = Template.paragraphSpacing;
                instance.characterWidthAdjustment = Template.characterWidthAdjustment;
                instance.enableWordWrapping = Template.enableWordWrapping;
                instance.wordWrappingRatios = Template.wordWrappingRatios;
                instance.overflowMode = Template.overflowMode;
                instance.enableKerning = Template.enableKerning;
                instance.extraPadding = Template.extraPadding;
                instance.parseCtrlCharacters = Template.parseCtrlCharacters;
                instance.isOverlay = Template.isOverlay;
                instance.isOrthographic = Template.isOrthographic;
                instance.enableCulling = Template.enableCulling;
                instance.ignoreVisibility = Template.ignoreVisibility;
                instance.horizontalMapping = Template.horizontalMapping;
                instance.verticalMapping = Template.verticalMapping;
                instance.mappingUvLineOffset = Template.mappingUvLineOffset;
                instance.renderMode = Template.renderMode;
                instance.geometrySortingOrder = Template.geometrySortingOrder;
                instance.isTextObjectScaleStatic = Template.isTextObjectScaleStatic;
                instance.vertexBufferAutoSizeReduction = Template.vertexBufferAutoSizeReduction;
                instance.firstVisibleCharacter = Template.firstVisibleCharacter;
                instance.maxVisibleCharacters = Template.maxVisibleCharacters;
                instance.maxVisibleWords = Template.maxVisibleWords;
                instance.maxVisibleLines = Template.maxVisibleLines;
                instance.useMaxVisibleDescender = Template.useMaxVisibleDescender;
                instance.pageToDisplay = Template.pageToDisplay;
                instance.margin = Template.margin;
                instance.havePropertiesChanged = Template.havePropertiesChanged;
                instance.isUsingLegacyAnimationComponent = Template.isUsingLegacyAnimationComponent;
                instance.autoSizeTextContainer = Template.autoSizeTextContainer;
                instance.isVolumetricText = Template.isVolumetricText;
                instance.checkPaddingRequired = Template.checkPaddingRequired;
                instance.isMaskUpdateRequired = Template.isMaskUpdateRequired;
                instance.tag_LineIndent = Template.tag_LineIndent;
                instance.tag_Indent = Template.tag_Indent;
                instance.tag_NoParsing = Template.tag_NoParsing;
                instance.maskable = Template.maskable;
                instance.isMaskingGraphic = Template.isMaskingGraphic;
                instance.color = Template.color;
                instance.raycastTarget = Template.raycastTarget;
                instance.useLegacyMeshGeneration = Template.useLegacyMeshGeneration;
            }
        }
    }
}