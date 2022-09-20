using System;

namespace LluviaTextureHelper
{
    public class MarshalHelper
    {
        private static void CopyInfo(Skin.TexLayout layout, Skin.FullLayout fullLayout)
        {
            layout.U = fullLayout.U;
            layout.V = fullLayout.V;
            layout.TW = fullLayout.TW;
            layout.TH = fullLayout.TH;
        }

        private static void CopyInfo(Skin.UVLayout layout, Skin.FullLayout fullLayout)
        {
            layout.U = fullLayout.U;
            layout.V = fullLayout.V;
        }

        private static void CopyInfo(Skin.UVXYLayout layout, Skin.FullLayout fullLayout)
        {
            layout.U = fullLayout.U;
            layout.V = fullLayout.V;
            layout.X = fullLayout.X;
            layout.Y = fullLayout.Y;
        }

        private static void CopyInfo(Skin.XYLayout layout, Skin.FullLayout fullLayout)
        {
            layout.X = fullLayout.X;
            layout.Y = fullLayout.Y;
        }

        private static void CopyInfo(Skin.TexLayoutWithWH layout, Skin.FullLayout fullLayout)
        {
            layout.U = fullLayout.U;
            layout.V = fullLayout.V;
            layout.TW = fullLayout.TW;
            layout.TH = fullLayout.TH;
            layout.W = fullLayout.W;
            layout.H = fullLayout.H;
        }


        public static Skin.MarshalPlayfield7k ToMarshalable(Skin.Playfield7k scene)
        {
            Skin.MarshalPlayfield7k marshal = new();

            marshal.SkinName = scene.SkinName;
            marshal.SkinVersion = scene.SkinVersion;
            marshal.SkinCreator = scene.SkinCreator;

            marshal.ScreenResolutionWidth = scene.ScreenResolutionWidth;
            marshal.ScreenResolutionHeight = scene.ScreenResolutionHeight;

            marshal.AccompanyingArtwork_X = scene.AccompanyingArtwork_X;
            marshal.AccompanyingArtwork_Y = scene.AccompanyingArtwork_Y;
            marshal.AccompanyingArtwork_W = scene.AccompanyingArtwork_W;
            marshal.AccompanyingArtwork_H = scene.AccompanyingArtwork_H;

            marshal.WallPapers = scene.WallPapers;

            marshal.Companion = scene.Companion;
            marshal.CompanionFramesQuantity = scene.CompanionFramesQuantity;
            marshal.CompanionFrameWidth = scene.CompanionFrameWidth;
            marshal.CompanionFrameHeight = scene.CompanionFrameHeight;
            marshal.Companion_X = scene.Companion_X;
            marshal.Companion_Y = scene.Companion_Y;
            marshal.Companion_W = scene.Companion_W;
            marshal.Companion_H = scene.Companion_H;

            marshal.Playfield.StartX = scene.Playfield.StartX;
            marshal.Playfield.Height = scene.Playfield.Height;
            marshal.Playfield.Columns_1357_Width = scene.Playfield.Columns_1357_Width;
            marshal.Playfield.Columns_246_Width = scene.Playfield.Columns_246_Width;
            marshal.Playfield.ColumnSeparatorsWidth = scene.Playfield.ColumnSeparatorsWidth;
            marshal.Playfield.Columns_1357_Color = scene.Playfield.Columns_1357_Color;
            marshal.Playfield.Columns_246_Color = scene.Playfield.Columns_246_Color;
            marshal.Playfield.ColumnSeparatorsColor = scene.Playfield.ColumnSeparatorsColor;
            marshal.Playfield.VerdictLineColor = scene.Playfield.VerdictLineColor;
            marshal.Playfield.BorderColor = scene.Playfield.BorderColor;

            marshal.BombName = scene.BombName;
            marshal.BombFrameQuantity = scene.BombFrameQuantity;
            marshal.BombFrameWidth = scene.BombFrameWidth;
            marshal.BombFrameHeight = scene.BombFrameHeight;
            marshal.BombDuration = scene.BombDuration;
            marshal.Bomb_W = scene.Bomb_W;
            marshal.Bomb_H = scene.Bomb_H;


            CopyInfo(marshal.Laser_7k_1357, scene.Laser_7k_1357);
            CopyInfo(marshal.Laser_7k_26, scene.Laser_7k_26);
            CopyInfo(marshal.Laser_7k_4, scene.Laser_7k_4);
            marshal.LaserHeight = scene.LaserHeight;
            marshal.LaserDuration = scene.LaserDuration;
            marshal.LaserAnimations = scene.LaserAnimations;

            marshal.KeysBackground = scene.KeysBackground;

            CopyInfo(marshal.Key_7k_1357, scene.Key_7k_1357);
            CopyInfo(marshal.Key_7k_26, scene.Key_7k_26);
            CopyInfo(marshal.Key_7k_4, scene.Key_7k_4);
            marshal.Keys_1357_Margin = scene.Keys_1357_Margin;
            marshal.Keys_1357_Height = scene.Keys_1357_Height;
            marshal.Keys_246_Margin = scene.Keys_246_Margin;
            marshal.Keys_246_Height = scene.Keys_246_Height;

            CopyInfo(marshal.Note_7k_N_1357, scene.Note_7k_N_1357);
            CopyInfo(marshal.Note_7k_N_26, scene.Note_7k_N_26);
            CopyInfo(marshal.Note_7k_N_4, scene.Note_7k_N_4);
            CopyInfo(marshal.Note_7k_L_1357, scene.Note_7k_L_1357);
            CopyInfo(marshal.Note_7k_L_26, scene.Note_7k_L_26);
            CopyInfo(marshal.Note_7k_L_4, scene.Note_7k_L_4);
            marshal.Notes_H = scene.Notes_H;

            CopyInfo(marshal.Injection_Autoplay, scene.Injection_Autoplay);
            CopyInfo(marshal.Injection_Replay, scene.Injection_Replay);
            CopyInfo(marshal.Injection_O2Normal, scene.Injection_O2Normal);
            CopyInfo(marshal.Injection_O2Hard, scene.Injection_O2Hard);
            CopyInfo(marshal.Injection_LR2Easy, scene.Injection_LR2Easy);
            CopyInfo(marshal.Injection_LR2Normal, scene.Injection_LR2Normal);
            CopyInfo(marshal.Injection_LR2Hard, scene.Injection_LR2Hard);
            CopyInfo(marshal.Injection_Mirror, scene.Injection_Mirror);
            CopyInfo(marshal.Injection_Random, scene.Injection_Random);
            CopyInfo(marshal.Injection_HRandom, scene.Injection_HRandom);
            CopyInfo(marshal.Injection_AllLN, scene.Injection_AllLN);
            CopyInfo(marshal.Injection_LR2NMJudge, scene.Injection_LR2NMJudge);
            CopyInfo(marshal.Injection_NoSSC, scene.Injection_NoSSC);
            marshal.Injections_TW = scene.Injections_TW;
            marshal.Injections_TH = scene.Injections_TH;
            marshal.Injections_W = scene.Injections_W;
            marshal.Injections_H = scene.Injections_H;

            marshal.HealthBarFillIsHorizontal = scene.HealthBarFillIsHorizontal;
            marshal.HealthBarBackground = scene.HealthBarBackground;
            marshal.HealthBar = scene.HealthBar;

            CopyInfo(marshal.Rank_S, scene.Rank_S);
            marshal.Ranks_TW = scene.Ranks_TW;
            marshal.Ranks_TH = scene.Ranks_TH;
            marshal.Ranks_X = scene.Ranks_X;
            marshal.Ranks_Y = scene.Ranks_Y;
            marshal.Ranks_W = scene.Ranks_W;
            marshal.Ranks_H = scene.Ranks_H;

            marshal.Verdicts_ClassicPosition = scene.Verdicts_ClassicPosition;
            CopyInfo(marshal.Verdict_COOL, scene.Verdict_COOL);
            CopyInfo(marshal.Verdict_GOOD, scene.Verdict_GOOD);
            CopyInfo(marshal.Verdict_OKAY, scene.Verdict_OKAY);
            CopyInfo(marshal.Verdict_BAD, scene.Verdict_BAD);
            CopyInfo(marshal.Verdict_MISS, scene.Verdict_MISS);
            marshal.Verdicts_X = scene.Verdicts_X;
            marshal.Verdicts_Y = scene.Verdicts_Y;
            marshal.Verdicts_ScaleW = scene.Verdicts_ScaleW;
            marshal.Verdicts_H = scene.Verdicts_H;

            marshal.Placard_Health = scene.Placard_Health;
            marshal.Placard_BPM = scene.Placard_BPM;
            marshal.Placard_ScrollSpeed = scene.Placard_ScrollSpeed;
            marshal.Placard_Offset = scene.Placard_Offset;
            marshal.Placard_MaxCombo = scene.Placard_MaxCombo;
            marshal.Placard_Accuracy = scene.Placard_Accuracy;
            marshal.Placard_Time = scene.Placard_Time;
            marshal.Placard_COOL = scene.Placard_COOL;
            marshal.Placard_GOOD = scene.Placard_GOOD;
            marshal.Placard_OKAY = scene.Placard_OKAY;
            marshal.Placard_BAD = scene.Placard_BAD;
            marshal.Placard_MISS = scene.Placard_MISS;

            marshal.Numbers_Health = scene.Numbers_Health;
            marshal.Numbers_BPM = scene.Numbers_BPM;
            marshal.Numbers_ScrollSpeed = scene.Numbers_ScrollSpeed;
            marshal.Numbers_Offset = scene.Numbers_Offset;
            marshal.Numbers_MaxCombo = scene.Numbers_MaxCombo;
            marshal.Numbers_Accuracy = scene.Numbers_Accuracy;
            marshal.Numbers_Time = scene.Numbers_Time;
            marshal.Numbers_COOL = scene.Numbers_COOL;
            CopyInfo(marshal.Numbers_GOOD, scene.Numbers_GOOD);
            CopyInfo(marshal.Numbers_OKAY, scene.Numbers_OKAY);
            CopyInfo(marshal.Numbers_VerdictCounter, scene.Numbers_VerdictCounter);
            CopyInfo(marshal.CounterPosition_COOL, scene.CounterPosition_COOL);
            CopyInfo(marshal.CounterPosition_GOOD, scene.CounterPosition_GOOD);
            CopyInfo(marshal.CounterPosition_OKAY, scene.CounterPosition_OKAY);
            CopyInfo(marshal.CounterPosition_BAD, scene.CounterPosition_BAD);
            CopyInfo(marshal.CounterPosition_MISS, scene.CounterPosition_MISS);

            marshal.ArbitraryTexture = scene.ArbitraryTexture;

            return marshal;
        }
    }
}