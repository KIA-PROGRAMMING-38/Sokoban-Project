using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Sokoban
{
    internal class Box
    {
        public enum State
        {
            Idle,
            Move,
            GrabByPlayer
        }

        public int X;
        public int Y;
        public int PrevX;
        public int PrevY;
        public string Image;
        public ConsoleColor Color;
        public State CurState;
        public int DirX;
        public int DirY;

        public int Update( in Player player )
        {
            // 박스 이전위치 갱신..
            PrevX = X;
            PrevY = Y;

            switch ( CurState )
            {
                case Box.State.Idle:
                    if ( player.X == X && player.Y == Y )   // 플레이어와 박스가 같을 때..
                    {
                        // 박스가 이동할 위치를 계산( 현재위치 - 이전위치 = 이동할 방향 )..
                        int boxMoveDirX = player.X - player.PrevX;
                        int boxMoveDirY = player.Y - player.PrevY;

                        // 박스 현재위치 갱신.. 
                        X += boxMoveDirX;
                        Y += boxMoveDirY;

                        break;
                    }

                    break;
                case Box.State.Move:
                    X -= DirX;
                    Y -= DirY;

                    return 1;

                case Box.State.GrabByPlayer:
                {
                    PrevX = X;
                    PrevY = Y;

                    // 박스가 이동할 위치를 계산( 현재위치 - 이전위치 = 이동할 방향 )..
                    int boxMoveDirX = player.X - player.PrevX;
                    int boxMoveDirY = player.Y - player.PrevY;

                    // 박스 현재위치 갱신.. 
                    X += boxMoveDirX;
                    Y += boxMoveDirY;
                }

                break;
            }

            return 0;
        }

        public void Render()
        {
            Renderer.Render( PrevX, PrevY, " ", Game.FOREGROUND_COLOR );
            Renderer.Render( X, Y, Image, Color );
        }

        public void UndoPos()
        {
            X = PrevX;
            Y = PrevY;
        }

        public void UndoState()
        {
            CurState = State.Idle;
        }

        public void UndoPosState()
        {
            UndoPos();
            UndoState();
        }

        public void ChangeDir( int dirX, int dirY )
        {
            DirX = dirX;
            DirY = dirY;

            CurState = State.Move;
        }
    }
}
