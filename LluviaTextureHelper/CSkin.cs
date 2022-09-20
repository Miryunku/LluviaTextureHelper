using System;
using System.Collections.Generic;

namespace LluviaTextureHelper.Skin
{
    public class Playfield7k
    {
        public string SkinName { get; set; }
        public string SkinVersion { get; set; }
        public string SkinCreator { get; set; }

        public int ScreenResolutionWidth { get; set; }
        public int ScreenResolutionHeight { get; set; }

        public int AccompanyingArtwork_X { get; set; }
        public int AccompanyingArtwork_Y { get; set; }
        public int AccompanyingArtwork_W { get; set; }
        public int AccompanyingArtwork_H { get; set; }

        public List<string> WallPapers { get; set; }

        public string Companion { get; set; }
        public int CompanionFramesQuantity { get; set; }
        public int CompanionFrameWidth { get; set; }
        public int CompanionFrameHeight { get; set; }
        public int Companion_X { get; set; }
        public int Companion_Y { get; set; }
        public int Companion_W { get; set; }
        public int Companion_H { get; set; }

        public Playfield1 Playfield { get; set; }

        public string BombName { get; set; }
        public int BombFrameQuantity { get; set; }
        public int BombFrameWidth { get; set; }
        public int BombFrameHeight { get; set; }
        public int BombDuration { get; set; }
        public int Bomb_W { get; set; }
        public int Bomb_H { get; set; }

        public FullLayout Laser_7k_1357 { get; set; }
        public FullLayout Laser_7k_26 { get; set; }
        public FullLayout Laser_7k_4 { get; set; }
        public int LaserHeight { get; set; }
        public int LaserDuration { get; set; }
        public List<LaserAnimation> LaserAnimations { get; set; }

        public FullLayout KeysBackground { get; set; }
        public FullLayout Key_7k_1357 { get; set; }
        public FullLayout Key_7k_26 { get; set; }
        public FullLayout Key_7k_4 { get; set; }
        public int Keys_1357_Margin { get; set; }
        public int Keys_1357_Height { get; set; }
        public int Keys_246_Margin { get; set; }
        public int Keys_246_Height { get; set; }

        public FullLayout Note_7k_N_1357 { get; set; }
        public FullLayout Note_7k_N_26 { get; set; }
        public FullLayout Note_7k_N_4 { get; set; }
        public FullLayout Note_7k_L_1357 { get; set; }
        public FullLayout Note_7k_L_26 { get; set; }
        public FullLayout Note_7k_L_4 { get; set; }
        public int Notes_H { get; set; }

        public FullLayout Injection_Autoplay { get; set; }
        public FullLayout Injection_Replay { get; set; }
        public FullLayout Injection_O2Normal { get; set; }
        public FullLayout Injection_O2Hard { get; set; }
        public FullLayout Injection_LR2Easy { get; set; }
        public FullLayout Injection_LR2Normal { get; set; }
        public FullLayout Injection_LR2Hard { get; set; }
        public FullLayout Injection_Mirror { get; set; }
        public FullLayout Injection_Random { get; set; }
        public FullLayout Injection_HRandom { get; set; }
        public FullLayout Injection_AllLN { get; set; }
        public FullLayout Injection_LR2NMJudge { get; set; }
        public FullLayout Injection_NoSSC { get; set; }
        public int Injections_TW { get; set; }
        public int Injections_TH { get; set; }
        public int Injections_W { get; set; }
        public int Injections_H { get; set; }

        public bool HealthBarFillIsHorizontal { get; set; }
        public FullLayout HealthBarBackground { get; set; }
        public FullLayout HealthBar { get; set; }

        public FullLayout Rank_S { get; set; }
        public int Ranks_TW { get; set; }
        public int Ranks_TH { get; set; }
        public int Ranks_X { get; set; }
        public int Ranks_Y { get; set; }
        public int Ranks_W { get; set; }
        public int Ranks_H { get; set; }

        public bool Verdicts_ClassicPosition { get; set; }
        public FullLayout Verdict_COOL { get; set; }
        public FullLayout Verdict_GOOD { get; set; }
        public FullLayout Verdict_OKAY { get; set; }
        public FullLayout Verdict_BAD { get; set; }
        public FullLayout Verdict_MISS { get; set; }
        public int Verdicts_X { get; set; }
        public int Verdicts_Y { get; set; }
        public float Verdicts_ScaleW { get; set; }
        public int Verdicts_H { get; set; }

        public FullLayout Placard_Health { get; set; }
        public FullLayout Placard_BPM { get; set; }
        public FullLayout Placard_ScrollSpeed { get; set; }
        public FullLayout Placard_Offset { get; set; }
        public FullLayout Placard_MaxCombo { get; set; }
        public FullLayout Placard_Accuracy { get; set; }
        public FullLayout Placard_Time { get; set; }
        public FullLayout Placard_COOL { get; set; }
        public FullLayout Placard_GOOD { get; set; }
        public FullLayout Placard_OKAY { get; set; }
        public FullLayout Placard_BAD { get; set; }
        public FullLayout Placard_MISS { get; set; }

        public FullLayout Numbers_Health { get; set; }
        public FullLayout Numbers_BPM { get; set; }
        public FullLayout Numbers_ScrollSpeed { get; set; }
        public FullLayout Numbers_Offset { get; set; }
        public FullLayout Numbers_MaxCombo { get; set; }
        public FullLayout Numbers_Accuracy { get; set; }
        public FullLayout Numbers_Time { get; set; }

        public FullLayout Numbers_COOL { get; set; }
        public FullLayout Numbers_GOOD { get; set; }
        public FullLayout Numbers_OKAY { get; set; }

        public FullLayout Numbers_VerdictCounter { get; set; }
        public FullLayout CounterPosition_COOL { get; set; }
        public FullLayout CounterPosition_GOOD { get; set; }
        public FullLayout CounterPosition_OKAY { get; set; }
        public FullLayout CounterPosition_BAD { get; set; }
        public FullLayout CounterPosition_MISS { get; set; }

        public FullLayout ArbitraryTexture { get; set; }
    }

    public class FullLayout
    {
        public int U { get; set; }
        public int V { get; set; }
        public int TW { get; set; }
        public int TH { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

    public class Color1
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }
    }

    public class Playfield1
    {
        public int StartX { get; set; }
        public int Height { get; set; }
        public int Columns_1357_Width { get; set; }
        public int Columns_246_Width { get; set; }
        public int ColumnSeparatorsWidth { get; set; }
        public Color1 Columns_1357_Color { get; set; } = new();
        public Color1 Columns_246_Color { get; set; } = new();
        public Color1 ColumnSeparatorsColor { get; set; } = new();
        public Color1 VerdictLineColor { get; set; } = new();
        public Color1 BorderColor { get; set; } = new();
    }

    public class LaserAnimation
    {
        public int Time { get; set; }
        public float WidthFactor { get; set; }
        public float HeightFactor { get; set; }
        public float OpacityFactor { get; set; }
    }
}
