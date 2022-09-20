using System;
using System.Collections.Generic;

namespace LluviaTextureHelper.Skin
{
    public class MarshalPlayfield7k
    {
        public string SkinName { get; set; } = string.Empty;
        public string SkinVersion { get; set; } = string.Empty;
        public string SkinCreator { get; set; } = string.Empty;

        public int ScreenResolutionWidth { get; set; }
        public int ScreenResolutionHeight { get; set; }

        public int AccompanyingArtwork_X { get; set; }
        public int AccompanyingArtwork_Y { get; set; }
        public int AccompanyingArtwork_W { get; set; }
        public int AccompanyingArtwork_H { get; set; }

        public List<string> WallPapers { get; set; } = new();

        public string Companion { get; set; } = string.Empty;
        public int CompanionFramesQuantity { get; set; }
        public int CompanionFrameWidth { get; set; }
        public int CompanionFrameHeight { get; set; }
        public int Companion_X { get; set; }
        public int Companion_Y { get; set; }
        public int Companion_W { get; set; }
        public int Companion_H { get; set; }

        public Playfield1 Playfield { get; set; } = new();

        public string BombName { get; set; } = string.Empty;
        public int BombFrameQuantity { get; set; }
        public int BombFrameWidth { get; set; }
        public int BombFrameHeight { get; set; }
        public int BombDuration { get; set; }
        public int Bomb_W { get; set; }
        public int Bomb_H { get; set; }

        public TexLayout Laser_7k_1357 { get; set; } = new();
        public TexLayout Laser_7k_26 { get; set; } = new();
        public TexLayout Laser_7k_4 { get; set; } = new();
        public int LaserHeight { get; set; }
        public int LaserDuration { get; set; }
        public List<LaserAnimation> LaserAnimations { get; set; } = new();

        public FullLayout KeysBackground { get; set; } = new();
        public TexLayout Key_7k_1357 { get; set; } = new();
        public TexLayout Key_7k_26 { get; set; } = new();
        public TexLayout Key_7k_4 { get; set; } = new();
        public int Keys_1357_Margin { get; set; }
        public int Keys_1357_Height { get; set; }
        public int Keys_246_Margin { get; set; }
        public int Keys_246_Height { get; set; }

        public TexLayout Note_7k_N_1357 { get; set; } = new();
        public TexLayout Note_7k_N_26 { get; set; } = new();
        public TexLayout Note_7k_N_4 { get; set; } = new();
        public TexLayout Note_7k_L_1357 { get; set; } = new();
        public TexLayout Note_7k_L_26 { get; set; } = new();
        public TexLayout Note_7k_L_4 { get; set; } = new();
        public int Notes_H { get; set; }

        public UVLayout Injection_Autoplay { get; set; } = new();
        public UVXYLayout Injection_Replay { get; set; } = new();
        public UVLayout Injection_O2Normal { get; set; } = new();
        public UVXYLayout Injection_O2Hard { get; set; } = new();
        public UVLayout Injection_LR2Easy { get; set; } = new();
        public UVLayout Injection_LR2Normal { get; set; } = new();
        public UVLayout Injection_LR2Hard { get; set; } = new();
        public UVXYLayout Injection_Mirror { get; set; } = new();
        public UVLayout Injection_Random { get; set; } = new();
        public UVLayout Injection_HRandom { get; set; } = new();
        public UVXYLayout Injection_AllLN { get; set; } = new();
        public UVXYLayout Injection_LR2NMJudge { get; set; } = new();
        public UVXYLayout Injection_NoSSC { get; set; } = new();
        public int Injections_TW { get; set; }
        public int Injections_TH { get; set; }
        public int Injections_W { get; set; }
        public int Injections_H { get; set; }

        public bool HealthBarFillIsHorizontal { get; set; }
        public FullLayout HealthBarBackground { get; set; } = new();
        public FullLayout HealthBar { get; set; } = new();

        public UVLayout Rank_S { get; set; } = new();
        public int Ranks_TW { get; set; }
        public int Ranks_TH { get; set; }
        public int Ranks_X { get; set; }
        public int Ranks_Y { get; set; }
        public int Ranks_W { get; set; }
        public int Ranks_H { get; set; }

        public bool Verdicts_ClassicPosition { get; set; }
        public TexLayout Verdict_COOL { get; set; } = new();
        public TexLayout Verdict_GOOD { get; set; } = new();
        public TexLayout Verdict_OKAY { get; set; } = new();
        public TexLayout Verdict_BAD { get; set; } = new();
        public TexLayout Verdict_MISS { get; set; } = new();
        public int Verdicts_X { get; set; }
        public int Verdicts_Y { get; set; }
        public float Verdicts_ScaleW { get; set; }
        public int Verdicts_H { get; set; }

        public FullLayout Placard_Health { get; set; } = new();
        public FullLayout Placard_BPM { get; set; } = new();
        public FullLayout Placard_ScrollSpeed { get; set; } = new();
        public FullLayout Placard_Offset { get; set; } = new();
        public FullLayout Placard_MaxCombo { get; set; } = new();
        public FullLayout Placard_Accuracy { get; set; } = new();
        public FullLayout Placard_Time { get; set; } = new();
        public FullLayout Placard_COOL { get; set; } = new();
        public FullLayout Placard_GOOD { get; set; } = new();
        public FullLayout Placard_OKAY { get; set; } = new();
        public FullLayout Placard_BAD { get; set; } = new();
        public FullLayout Placard_MISS { get; set; } = new();

        public FullLayout Numbers_Health { get; set; } = new();
        public FullLayout Numbers_BPM { get; set; } = new();
        public FullLayout Numbers_ScrollSpeed { get; set; } = new();
        public FullLayout Numbers_Offset { get; set; } = new();
        public FullLayout Numbers_MaxCombo { get; set; } = new();
        public FullLayout Numbers_Accuracy { get; set; } = new();
        public FullLayout Numbers_Time { get; set; } = new();

        public FullLayout Numbers_COOL { get; set; } = new();
        public TexLayout Numbers_GOOD { get; set; } = new();
        public TexLayout Numbers_OKAY { get; set; } = new();

        public TexLayoutWithWH Numbers_VerdictCounter { get; set; } = new();
        public XYLayout CounterPosition_COOL { get; set; } = new();
        public XYLayout CounterPosition_GOOD { get; set; } = new();
        public XYLayout CounterPosition_OKAY { get; set; } = new();
        public XYLayout CounterPosition_BAD { get; set; } = new();
        public XYLayout CounterPosition_MISS { get; set; } = new();

        public FullLayout ArbitraryTexture { get; set; } = new();
    }

    public class TexLayout
    {
        public int U { get; set; }
        public int V { get; set; }
        public int TW { get; set; }
        public int TH { get; set; }
    }

    public class TexLayoutWithWH
    {
        public int U { get; set; }
        public int V { get; set; }
        public int TW { get; set; }
        public int TH { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

    public class Layout
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

    public class UVXYLayout
    {
        public int U { get; set; }
        public int V { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class UVLayout
    {
        public int U { get; set; }
        public int V { get; set; }
    }

    public class XYLayout
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}