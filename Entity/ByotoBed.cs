using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalLibrary.Entity
{
    public class ByotoBed
    {
        public string RoomCode = "";

        public int SEQ = 1;

        public int X = 30;

        public int Y = 5;

        public int Width = 80;

        public int Height = 30;

        public ByotoBedNumberAlignment Alignment = ByotoBedNumberAlignment.MiddleLeft;

        public ByotoBed(string room_code, int seq, ByotoBedNumberAlignment alignment = ByotoBedNumberAlignment.MiddleLeft)
        {
            this.RoomCode = room_code;
            this.SEQ = seq;
            this.Alignment = alignment;
        }

        public ByotoBed(string room_code, int seq, int x, int y, ByotoBedNumberAlignment alignment = ByotoBedNumberAlignment.MiddleLeft)
        {
            this.RoomCode = room_code;
            this.SEQ = seq;
            this.X = x;
            this.Y = y;
            this.Alignment = alignment;
        }

        public ByotoBed(string room_code, int seq, int x, int y, int width, int height, ByotoBedNumberAlignment alignment = ByotoBedNumberAlignment.MiddleLeft)
        {
            this.RoomCode = room_code;
            this.SEQ = seq;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Alignment = alignment;
        }

        static List<ByotoBed> list = new List<ByotoBed>();

        public static List<ByotoBed> GetList(string room_code)
        {
            // リストが空のときは生成する
            if (list.Count == 0)
            {
                // わかば
                // 301～306号室
                for (int i = 1; i <= 6; i++)
                {
                    list.Add(new ByotoBed((300 + i).ToString(), 1, 30, 40));
                    list.Add(new ByotoBed((300 + i).ToString(), 2, 135, 40));
                    list.Add(new ByotoBed((300 + i).ToString(), 3, 135, 5));
                    list.Add(new ByotoBed((300 + i).ToString(), 4, 30, 5));
                }

                // 312～321号室
                for (int i = 12; i <= 21; i++)
                {
                    list.Add(new ByotoBed((300 + i).ToString(), 1, 10, 30, ByotoBedNumberAlignment.TopLeft));
                }

                // 307, 309, 310 号室
                for (int i = 7; i <= 10; i++)
                {
                    // 308 を除く
                    if (i == 8) continue;

                    list.Add(new ByotoBed((300 + i).ToString(), 1, 135, 5));
                    list.Add(new ByotoBed((300 + i).ToString(), 2, 30, 5));
                    list.Add(new ByotoBed((300 + i).ToString(), 3, 30, 40));
                    list.Add(new ByotoBed((300 + i).ToString(), 4, 135, 40));
                }

                // 308, 311 号室
                for (int i = 8; i <= 11; i++)
                {
                    // 309, 310 を除く
                    if (i == 9 || i == 10) continue;

                    list.Add(new ByotoBed((300 + i).ToString(), 1, 135, 40));
                    list.Add(new ByotoBed((300 + i).ToString(), 2, 30, 40));
                }

                // 350号室
                list.Add(new ByotoBed("350", 1, 10, 40, ByotoBedNumberAlignment.TopLeft));
                list.Add(new ByotoBed("350", 2, 115, 40, ByotoBedNumberAlignment.TopLeft));

                // 351号室
                list.Add(new ByotoBed("351", 1, 10, 40, ByotoBedNumberAlignment.TopLeft));


                // さくら
                // 401～406号室
                for (int i = 1; i <= 6; i++)
                {
                    list.Add(new ByotoBed((400 + i).ToString(), 1, 30, 40));
                    list.Add(new ByotoBed((400 + i).ToString(), 2, 135, 40));
                    list.Add(new ByotoBed((400 + i).ToString(), 3, 135, 5));
                    list.Add(new ByotoBed((400 + i).ToString(), 4, 30, 5));
                }

                // 412～421号室
                for (int i = 12; i <= 21; i++)
                {
                    list.Add(new ByotoBed((400 + i).ToString(), 1, 10, 30, ByotoBedNumberAlignment.TopLeft));
                }

                // 407, 409, 410 号室
                for (int i = 7; i <= 10; i++)
                {
                    // 408 を除く
                    if (i == 8) continue;

                    list.Add(new ByotoBed((400 + i).ToString(), 1, 135, 5));
                    list.Add(new ByotoBed((400 + i).ToString(), 2, 30, 5));
                    list.Add(new ByotoBed((400 + i).ToString(), 3, 30, 40));
                    list.Add(new ByotoBed((400 + i).ToString(), 4, 135, 40));
                }

                // 408号室
                list.Add(new ByotoBed("408", 1, 135, 40));
                list.Add(new ByotoBed("408", 2, 30, 40));

                // 411号室
                list.Add(new ByotoBed("411", 1, 135, 40));

                // 450号室
                list.Add(new ByotoBed("450", 1, 10, 40, ByotoBedNumberAlignment.TopLeft));
                list.Add(new ByotoBed("450", 2, 115, 40, ByotoBedNumberAlignment.TopLeft));

                // 451号室
                list.Add(new ByotoBed("451", 1, 10, 40, ByotoBedNumberAlignment.TopLeft));


                // あやめ
                // 331～337号室
                for (int i = 1; i <= 7; i++)
                {
                    list.Add(new ByotoBed((330 + i).ToString(), 1, 30, 30));
                }

                // 338～340号室
                for (int i = 8; i <= 10; i++)
                {
                    list.Add(new ByotoBed((330 + i).ToString(), 1, 30, 30));
                    list.Add(new ByotoBed((330 + i).ToString(), 2, 30, 90));
                }

                // 341～349号室
                for (int i = 1; i <= 9; i++)
                {
                    list.Add(new ByotoBed((340 + i).ToString(), 1, 30, 30));
                }
            }

            List<ByotoBed> tmp_list = new List<ByotoBed>();

            foreach (ByotoBed obj in list)
            {
                // 病室が合致する場合は追加する
                if (obj.RoomCode.Equals(room_code))
                {
                    tmp_list.Add(obj);
                }
            }

            return tmp_list;
        }

    }

    public enum ByotoBedNumberAlignment : int
    {
        TopLeft = 1,
        MiddleLeft = 11,
        MiddleRight = 12,
        BottomLeft = 21
    }
}
