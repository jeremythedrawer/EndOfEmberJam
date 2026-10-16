using System;
using System.Collections.Generic;
using UnityEngine;
using static AtlasRendering;
public static class Atlas
{
    public const int PIXELS_PER_UNIT = 180;
    public const int FRAMES_PER_SEC = 30;
    public readonly static float UNITS_PER_PIXEL = 1 / (float)PIXELS_PER_UNIT;
    public static int FLOAT_SIZE = sizeof(float);
    public static int FLOAT2_SIZE = FLOAT_SIZE * 2;
    public static int FLOAT3_SIZE = FLOAT_SIZE * 3;
    public static int FLOAT4_SIZE = FLOAT_SIZE * 4;
    public static int INT_SIZE = sizeof(uint);

    [Flags] public enum ColorBits
    {
        None = 0,
        RedChannel = 1 << 8,
        GreenChannel = 1 << 9,
        BlueChannel = 1 << 10,
    }
    public enum ClipType
    {
        Loop,
        PingPong,
        OneShot,
        Manual,
    }
    public enum SpriteMode
    {
        Simple,
        Motion,
        Slice,
        UISimple,
        UIMotion,
        UISlice,
        UIText,
    }
    public enum EntityMotionType
    {
        None,
    }


    [Serializable] public struct SimpleSprite
    {
        public Vector2[] customPositions;
        
        public Vector4 uvSizeAndPos;
        
        public Vector3 worldSize;
        
        public Vector2 uvPivot;
        
        public int index;
    }
    [Serializable] public struct MotionSprite
    {
        public SimpleSprite sprite;

        public int audioIndex;
        public int holdFrames;
    }
    [Serializable] public struct SliceSprite
    {
        public SimpleSprite sprite;
        public Vector4[] uvSizeAndPos;

        public Vector4 slice;
        public Vector4 worldSlices;
    }
    [Serializable] public struct AtlasClip
    {
        public string clipName;
        public ClipType clipType;
        public int motionIndex;
        public int keyframeStartIndex;
        public int keyframeEndIndex;

        public AudioClip[] audioClips;
        public Action[] actions;
    }
    public static Dictionary<int, AtlasClip> BuildClipKeys(AtlasClip[] clips)
    {
        Dictionary<int, AtlasClip> clipDict = new Dictionary<int, AtlasClip>();

        for (int i = 0; i < clips.Length; i++)
        {
            AtlasClip clip = clips[i];
            clipDict[clip.motionIndex] = clip;
        }

        return clipDict;
    }

    public static readonly Dictionary<EntityMotionType, Type> MotionEnumDictionary =
    new Dictionary<EntityMotionType, Type>
    {
        //{ EntityMotionType.Passenger, typeof(PassengerMotion) },
        //{ EntityMotionType.Train, typeof(TrainMotion) },
    };

}
