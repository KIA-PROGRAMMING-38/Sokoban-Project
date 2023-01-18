using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Sokoban
{
    internal class Player
    {
        public enum ActionKind
        {
            None,
            Move,
            Grab,
            Kick,
        }

        struct MoveInfo
        {
            public int X;
            public int Y;
        }

        public static int[] ACTION_USE_MP = new int[4]
        {
            0, 1, 3, 5
        };

        public int X;
        public int Y;
        public int PrevX;
        public int PrevY;
        public string Image;
        public ConsoleColor Color;
        public ActionKind actionKind;

        public int MaxHp;
        public int CurHp;
        public int MaxMp;
        public int CurMp;

        public void UndoPos()
        {
            X = PrevX;
            Y = PrevY;
        }

        public void Update( ConsoleKey inputKey, Box[] boxes )
        {
            // 플레이어 이전위치 갱신..
            PrevX = X;
            PrevY = Y;

            MoveInfo moveInfo = new MoveInfo();

            ProcessInputKey( inputKey, ref moveInfo );

            // 현재 MP가 행동 시 필요한 MP 보다 부족하다면..
            if ( CurMp < ACTION_USE_MP[(int)actionKind] )
                return;

            // 행동에 필요한 MP 만큼 빼주기..
            CurMp -= ACTION_USE_MP[(int)actionKind];

            int boxCount = boxes.Length;

            // 행동 종류에 따라 해야할 행동 진행..
            switch ( actionKind )
            {
                case ActionKind.Move:
                    X += moveInfo.X;
                    Y += moveInfo.Y;

                    break;
                case ActionKind.Grab:
                    for ( int boxIndex = 0; boxIndex < boxCount; ++boxIndex )
                    {
                        int boxX = boxes[boxIndex].X;
                        int boxY = boxes[boxIndex].Y;
                        Box.State boxState = boxes[boxIndex].CurState;

                        int xDist = Math.Abs( X - boxX );
                        int yDist = Math.Abs( Y - boxY );

                        if ( 1 == xDist + yDist )
                        {
                            if ( Box.State.GrabByPlayer == boxState )
                                boxState = Box.State.Idle;
                            else
                                boxState = Box.State.GrabByPlayer;

                            boxes[boxIndex].CurState = boxState;
                        }
                    }

                    break;
                case ActionKind.Kick:
                    for ( int boxIndex = 0; boxIndex < boxCount; ++boxIndex )
                    {
                        int boxX = boxes[boxIndex].X;
                        int boxY = boxes[boxIndex].Y;

                        int xDist = Math.Abs( X - boxX );
                        int yDist = Math.Abs( Y - boxY );

                        if ( 1 == xDist + yDist )
                        {
                            // 박스 방향 설정 및 박스한테 방향 값 넘겨줘서 바꾸게 하기..
                            int dirX = X - boxX;
                            int dirY = Y - boxY;

                            boxes[boxIndex].ChangeDir( dirX, dirY );
                        }
                    }

                    break;
            }
        }

        private void ProcessInputKey( ConsoleKey inputKey, ref MoveInfo outMoveInfo )
        {
            actionKind = ActionKind.None;

            switch ( inputKey )
            {
                case ConsoleKey.RightArrow:
                case ConsoleKey.LeftArrow:
                    outMoveInfo.X += (int)inputKey - 38;
                    actionKind = ActionKind.Move;

                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    outMoveInfo.Y += (int)inputKey - 39;
                    actionKind = ActionKind.Move;

                    break;
                case ConsoleKey.Spacebar:
                    actionKind = ActionKind.Grab;

                    break;
                case ConsoleKey.A:
                    actionKind = ActionKind.Kick;

                    break;

                default:
                    return;
            }
        }

        public void Render( Map.SpaceType prevPosSpaceType )
        {
            if ( Map.SpaceType.Pass == prevPosSpaceType )
            {
                Renderer.Render( PrevX, PrevY, " ", Game.FOREGROUND_COLOR );
            }
            Renderer.Render( X, Y, Image, Color );
        }
    }
}
