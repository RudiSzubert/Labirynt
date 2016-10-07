using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayBoard test = new PlayBoard(30, 60);
            Player Marcin = new Player("Marcin");
            Player.Game(Marcin, test);
        }
    }
    class PlayBoard
    {
        internal int wide, altitude;
        private static bool WallLeft(ref int[,] table, ref int x, ref int y) //sciana lewo
        {
            int testx = table.GetUpperBound(0);
            int testy = table.GetUpperBound(1);
            if (y - 2 > 0 && x - 1 > 0 && x+1 < testx)
            {
                if (table[x, y - 1] == 0 && table[x - 1, y - 1] == 0 && table[x + 1, y - 1] == 0 &&
                   table[x, y - 2] == 0 && table[x - 1, y - 2] == 0 && table[x + 1, y - 2] == 0)
                {
                    table[x, y - 1] = 1;
                    y = y - 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        private static bool WallRight(ref int[,] table, ref int x, ref int y)// sciana prawo
        {
            int testx = table.GetUpperBound(0);
            int testy = table.GetUpperBound(1);
            if (y + 2 < testy && x-1 >0 && x+1 < testx)
            {
                if (table[x, y + 1] == 0 && table[x - 1, y + 1] == 0 && table[x + 1, y + 1] == 0 &&
                   table[x, y + 2] == 0 && table[x - 1, y + 2] == 0 && table[x + 1, y + 2] == 0)
                {
                    table[x, y + 1] = 1;
                    y = y + 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        private static bool WallUp(ref int[,] table, ref int x, ref int y)  //sciana gora
        {
            int testx = table.GetUpperBound(0);
            int testy = table.GetUpperBound(1);
            if (x - 2 > 0)
            {
                if (table[x - 1, y] == 0 && table[x - 1, y - 1] == 0 &&
                    table[x - 1, y + 1] == 0 && table[x - 2, y - 1] == 0 &&
                   table[x - 2, y + 1] == 0 && table[x - 2, y] == 0)
                {
                    table[x - 1, y] = 1;
                    x = x - 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        private static bool WallDown(ref int[,] table, ref int x, ref int y) //sciana dol
        {
            int testx = table.GetUpperBound(0);
            int testy = table.GetUpperBound(1);
            if (x + 2 < testx)
            {
                if (table[x + 1, y] == 0 && table[x + 1, y - 1] == 0 &&
                    table[x + 1, y + 1] == 0 && table[x + 2, y - 1] == 0 &&
                   table[x + 2, y + 1] == 0 && table[x + 2, y] == 0)
                {
                    table[x + 1, y] = 1;
                    x = x + 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public int[,] Field;  // dekl tablicy
        public void Rysuj()  // do jej rysowania
        {
            for (int i = 0; i < this.altitude; i++)
            {
                for (int j = 0; j < this.wide ; j++)
                {
                    if(j==this.wide - 1 )
                    System.Console.WriteLine(Player.Grafa(this.Field[i, j]));
                    else System.Console.Write(Player.Grafa(this.Field[i, j]));
                }
            }
        }
        public PlayBoard(int x, int y)  // tworzenie planszy - konstruktor
        {
            this.wide = y;
            this.altitude = x;
            this.Field = new int[x, y];
            for (int i = 0; i < this.altitude; i++)
            {
                for (int j = 0; j < this.wide; j++)
                {
                    this.Field[i, j] = 0; // Wszystkie pola to chodnik
                }
            }
            for (int i = 0; i < this.altitude; i++)
            {
                this.Field[i, 0] = 1;
                this.Field[i, this.wide -1] = 4;
            }
            for (int i = 0; i < this.wide; i++)
            {
                this.Field[0, i] = 1;
                this.Field[this.altitude -1 , i] = 4; // ściany wokół 
            }
            Random rnd = new Random();
            int temp = rnd.Next(0, 4);
            if (temp == 0 )
                this.Field[0, rnd.Next(1,this.wide - 2)] = 2;
            else if (temp == 1)
                this.Field[this.altitude - 1 , rnd.Next(1, this.wide-2)] = 2;
            else if (temp == 2)
                this.Field[rnd.Next(1,this.altitude -2), 0] = 2;
            else if (temp == 3)
                this.Field[rnd.Next(1,this.altitude -2), this.wide - 1 ] = 2;  // Exit
            int kolumna, wiersz,sciany;
            for (int i = 2; i < this.wide - 2; i = i + 4)
            {
                kolumna = i;
                wiersz = 0;
                sciany = 0;
                if (this.Field[0, kolumna] != 2)
                {
                    do
                    {
                        PlayBoard.WallDown(ref this.Field, ref wiersz, ref kolumna);
                        sciany++;
                    } while (sciany < rnd.Next(2, 7));
                    do
                    {
                        temp = rnd.Next(0, 4);
                        if (temp == 0)
                        {
                            PlayBoard.WallDown(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 1)
                        {
                            PlayBoard.WallUp(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 2)
                        {
                            PlayBoard.WallLeft(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 3)
                        {
                            PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                    } while (sciany < rnd.Next((this.wide + this.altitude) / 2, (this.wide + this.altitude)*4 ));
                }
            }//sciany od gory
            for (int i = 2; i < this.wide - 2; i = i + 4)
            {
                kolumna = i;
                wiersz = this.altitude -1;
                sciany = 0;
                if (this.Field[wiersz, kolumna] != 2)
                {
                    do
                    {
                        PlayBoard.WallUp(ref this.Field, ref wiersz, ref kolumna);
                        sciany++;
                    } while (sciany < rnd.Next(2, 7));
                    do
                    {
                        temp = rnd.Next(0, 4);
                        if (temp == 0)
                        {
                            PlayBoard.WallDown(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 1)
                        {
                            PlayBoard.WallUp(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 2)
                        {
                            PlayBoard.WallLeft(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 3)
                        {
                            PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                    } while (sciany < rnd.Next((this.wide + this.altitude) / 2, (this.wide + this.altitude) * 4));
                }
            }//sciany od dolu
            for (int i = 2; i < this.altitude - 2; i = i + 4)
            {
                kolumna = 0;
                wiersz = i;
                sciany = 0;
                if (this.Field[wiersz, kolumna] != 2)
                {
                    do
                    {
                        PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                        sciany++;
                    } while (sciany < rnd.Next(2, 7));
                    do
                    {
                        temp = rnd.Next(0, 4);
                        sciany = sciany + 1;
                        if (temp == 0)
                        {
                            PlayBoard.WallDown(ref this.Field, ref wiersz, ref kolumna);
                        }
                        else if (temp == 1)
                        {
                            PlayBoard.WallUp(ref this.Field, ref wiersz, ref kolumna);
                        }
                        else if (temp == 2)
                        {
                            PlayBoard.WallLeft(ref this.Field, ref wiersz, ref kolumna);
                        }
                        else if (temp == 3)
                        {
                            PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                        }
                    } while (sciany < rnd.Next((this.wide + this.altitude) / 2, (this.wide + this.altitude) * 4));
                }
            }//sciany od lewej
            for (int i = 2; i < this.altitude - 2; i = i + 4)
            {
                kolumna = this.wide-1;
                wiersz = i;
                sciany = 0;
                if (this.Field[wiersz, kolumna] != 2)
                {
                    do
                    {
                        PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                        sciany++;
                    } while (sciany < rnd.Next(2, 7));
                    do
                    {
                        temp = rnd.Next(0, 4);
                        if (temp == 0)
                        {
                            PlayBoard.WallDown(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 1)
                        {
                            PlayBoard.WallUp(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 2)
                        {
                            PlayBoard.WallLeft(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                        else if (temp == 3)
                        {
                            PlayBoard.WallRight(ref this.Field, ref wiersz, ref kolumna);
                            sciany = sciany + 1;
                        }
                    } while (sciany < rnd.Next((this.wide + this.altitude) / 2, (this.wide + this.altitude) * 4));
                }
            }//sciany od prawej

        }
    }

    class Player
    {
        private int x = 0;
        private int y = 0;
        internal static string Grafa(int wartosc)
        {
            if (wartosc == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return "+";
            }
            if (wartosc == 4)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return "+";
            }
            else if (wartosc == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                return "o";
            }
            else if (wartosc == 2)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                return "E";
            }
            else if (wartosc == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return "P";
            }
            else return "Z";
        }
        private static void Obraz(Player player, PlayBoard playboard)
        {
            for (int i = player.y - 3; i < player.y + 4; i++)
            {
                for (int j = player.x - 3; j < player.x + 4; j++)
                {
                    if( j > playboard.altitude - 1  || j<0 || i < 0 || i > playboard.wide -1)
                    {
                        if (j == player.x + 3) System.Console.WriteLine(Player.Grafa(4));
                        else System.Console.Write(Player.Grafa(4));
                    }
                    else
                    {
                        if (j == player.x + 3) System.Console.WriteLine(Player.Grafa(playboard.Field[j, i]));
                        else System.Console.Write(Player.Grafa(playboard.Field[j, i]));
                    }                    
                }
            }
        }
        private static bool MLeft(ref Player player,ref PlayBoard playboard)
        {
            if (playboard.Field[player.x - 1, player.y] == 0 || playboard.Field[player.x - 1, player.y] == 2)
            {
                if (playboard.Field[player.x - 1, player.y] == 2)
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.x = player.x - 1;
                    playboard.Field[player.x, player.y] = 2;
                    return true;
                }
                else
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.x = player.x - 1;
                    playboard.Field[player.x, player.y] = 3;
                    return true;
                }

            }
            else return false;

        }
        private static bool MRight(ref Player player, ref PlayBoard playboard)
        {
            if (playboard.Field[player.x + 1, player.y] == 0 || playboard.Field[player.x + 1, player.y] == 2)
            {
                if (playboard.Field[player.x + 1, player.y] == 2)
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.x = player.x + 1;
                    playboard.Field[player.x, player.y] = 2;
                    return true;
                }
                else
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.x = player.x + 1;
                    playboard.Field[player.x, player.y] = 3;
                    return true;
                }

            }
            else return false;

        }
        private static bool MDown(ref Player player, ref PlayBoard playboard)
        {
            if (playboard.Field[player.x, player.y + 1] == 0 || playboard.Field[player.x, player.y + 1] == 2)
            {
                if (playboard.Field[player.x, player.y + 1] == 2)
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.y = player.y + 1;
                    playboard.Field[player.x, player.y] = 2;
                    return true;
                }
                else
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.y = player.y + 1;
                    playboard.Field[player.x, player.y] = 3;
                    return true;
                }

            }
            else return false;

        }
        private static bool MUp(ref Player player, ref PlayBoard playboard)
        {
            if (playboard.Field[player.x, player.y - 1] == 0 || playboard.Field[player.x, player.y - 1] == 2)
            {
                if (playboard.Field[player.x, player.y - 1] == 2)
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.y = player.y - 1;
                    playboard.Field[player.x, player.y] = 2;
                    return true;
                }
                else
                {
                    playboard.Field[player.x, player.y] = 0;
                    player.y = player.y - 1;
                    playboard.Field[player.x, player.y] = 3;
                    return true;
                }
            }
            else return false;

        }
        string name = "stefan";
        private int dynamite;
        private static bool Dynamite(ref Player player, ref PlayBoard playboard)
        {
            if (player.dynamite == 0) return false;
            else
            {
                player.dynamite = player.dynamite - 1;
                for (int i = player.x-2; i < player.x+2; i++)
                {
                    for (int j = player.y-2; j < player.y+2; j++)
                    {
                        if (j < playboard.altitude-1 || j >= 1 || i >= 1 || i < playboard.wide-1 )
                        {
                            if (playboard.Field[i, j] == 1)
                                playboard.Field[i, j] = 0;
                        }
                    }
                }
                return true;

            }
        }
        public Player(string n)
        {
            name = n;
            this.dynamite = 10;
            
        }
        public static void Game(Player player, PlayBoard playboard)
        {
            Random rnd = new Random();
            int tmp = 1;
            do
            {
                player.x = rnd.Next(2, playboard.altitude - 2);
                player.y = rnd.Next(2, playboard.wide - 2);
                tmp = playboard.Field[player.x, player.y];
            } while (tmp != 0);
            playboard.Field[player.x, player.y] = 3;

            Player.Obraz(player, playboard);
            ConsoleKeyInfo czytacz;
            do
            {
                czytacz = Console.ReadKey();
                Console.Clear();
                if (czytacz.Key == ConsoleKey.LeftArrow) Player.MLeft(ref player,ref playboard);
                else if(czytacz.Key == ConsoleKey.RightArrow) Player.MRight(ref player, ref playboard);
                else if (czytacz.Key == ConsoleKey.UpArrow) Player.MUp(ref player, ref playboard);
                else if (czytacz.Key == ConsoleKey.DownArrow) Player.MDown(ref player, ref playboard);
                else if (czytacz.Key == ConsoleKey.D) Player.Dynamite(ref player, ref playboard);
                Player.Obraz(player, playboard);
            } while (playboard.Field[player.x, player.y] != 2);
            Console.Clear();
            System.Console.WriteLine("Wygrales {0}", player.name);
        }
    }
}

