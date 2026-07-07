using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MedicalLibrary.Entity
{
    public class ByotoRoom
    {
        public string ByotoCode = "";

        public string Code = "";

        public string Cont1 = "";

        public string Cont2 = "";

        public int X = 0;

        public int Y = 0;

        public int Width = 220;

        public int Height = 75;


        public ByotoRoom(string byoto, string code, int x, int y, int width, int height)
        {
            this.ByotoCode = byoto;
            this.Code = code;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }


        public ByotoRoom(string byoto, string code, int x, int y)
        {
            this.ByotoCode = byoto;
            this.Code = code;
            this.X = x;
            this.Y = y;
        }


        static Dictionary<string, ByotoRoom> dict = new Dictionary<string, ByotoRoom>();

        public static Dictionary<string, ByotoRoom> Dict
        {
            get
            {
                if (dict.Count == 0)
                {
                    // わかば
                    dict.Add("301", new ByotoRoom("03", "301", 710, 380));
                    dict.Add("302", new ByotoRoom("03", "302", 610, 305));
                    dict.Add("303", new ByotoRoom("03", "303", 510, 230));
                    dict.Add("304", new ByotoRoom("03", "304", 410, 155));
                    dict.Add("305", new ByotoRoom("03", "305", 310, 80));
                    dict.Add("306", new ByotoRoom("03", "306", 210, 5));

                    dict.Add("307", new ByotoRoom("03", "307", 10, 80));
                    dict.Add("308", new ByotoRoom("03", "308", 10, 155));
                    dict.Add("309", new ByotoRoom("03", "309", 10, 230));
                    dict.Add("310", new ByotoRoom("03", "310", 10, 305));
                    dict.Add("311", new ByotoRoom("03", "311", 10, 380));

                    dict.Add("312", new ByotoRoom("03", "312", 810, 530, 100, 75));
                    dict.Add("313", new ByotoRoom("03", "313", 710, 530, 100, 75));
                    dict.Add("314", new ByotoRoom("03", "314", 610, 530, 100, 75));
                    dict.Add("315", new ByotoRoom("03", "315", 510, 530, 100, 75));
                    dict.Add("316", new ByotoRoom("03", "316", 410, 530, 100, 75));
                    dict.Add("317", new ByotoRoom("03", "317", 310, 530, 100, 75));
                    dict.Add("318", new ByotoRoom("03", "318", 210, 530, 100, 75));
                    dict.Add("319", new ByotoRoom("03", "319", 110, 530, 100, 75));
                    dict.Add("320", new ByotoRoom("03", "320", 10, 530, 100, 75));

                    dict.Add("321", new ByotoRoom("03", "321", 10, 455, 100, 75));

                    dict.Add("350", new ByotoRoom("03", "350", 310, 380, 200, 75));
                    dict.Add("351", new ByotoRoom("03", "351", 310, 305, 100, 75));

                    // さくら
                    dict.Add("401", new ByotoRoom("04", "401", 710, 380));
                    dict.Add("402", new ByotoRoom("04", "402", 610, 305));
                    dict.Add("403", new ByotoRoom("04", "403", 510, 230));
                    dict.Add("404", new ByotoRoom("04", "404", 410, 155));
                    dict.Add("405", new ByotoRoom("04", "405", 310, 80));
                    dict.Add("406", new ByotoRoom("04", "406", 210, 5));

                    dict.Add("407", new ByotoRoom("04", "407", 10, 80));
                    dict.Add("408", new ByotoRoom("04", "408", 10, 155));
                    dict.Add("409", new ByotoRoom("04", "409", 10, 230));
                    dict.Add("410", new ByotoRoom("04", "410", 10, 305));
                    dict.Add("411", new ByotoRoom("04", "411", 10, 380));

                    dict.Add("412", new ByotoRoom("04", "412", 810, 530, 100, 75));
                    dict.Add("413", new ByotoRoom("04", "413", 710, 530, 100, 75));
                    dict.Add("414", new ByotoRoom("04", "414", 610, 530, 100, 75));
                    dict.Add("415", new ByotoRoom("04", "415", 510, 530, 100, 75));
                    dict.Add("416", new ByotoRoom("04", "416", 410, 530, 100, 75));
                    dict.Add("417", new ByotoRoom("04", "417", 310, 530, 100, 75));
                    dict.Add("418", new ByotoRoom("04", "418", 210, 530, 100, 75));
                    dict.Add("419", new ByotoRoom("04", "419", 110, 530, 100, 75));
                    dict.Add("420", new ByotoRoom("04", "420", 10, 530, 100, 75));

                    dict.Add("421", new ByotoRoom("04", "421", 10, 455, 100, 75));

                    dict.Add("450", new ByotoRoom("04", "450", 310, 380, 200, 75));
                    dict.Add("451", new ByotoRoom("04", "451", 310, 305, 100, 75));

                    // あやめ
                    dict.Add("331", new ByotoRoom("05", "331", 670, 10, 150, 75));
                    dict.Add("332", new ByotoRoom("05", "332", 670, 85, 150, 75));
                    dict.Add("333", new ByotoRoom("05", "333", 670, 160, 150, 75));
                    dict.Add("334", new ByotoRoom("05", "334", 670, 235, 150, 75));
                    dict.Add("335", new ByotoRoom("05", "335", 670, 310, 150, 75));
                    dict.Add("336", new ByotoRoom("05", "336", 670, 385, 150, 75));
                    dict.Add("337", new ByotoRoom("05", "337", 670, 460, 150, 75));

                    dict.Add("338", new ByotoRoom("05", "338", 500, 400, 150, 135));
                    dict.Add("339", new ByotoRoom("05", "339", 340, 400, 150, 135));
                    dict.Add("340", new ByotoRoom("05", "340", 180, 400, 150, 135));

                    dict.Add("341", new ByotoRoom("05", "341", 10, 460, 150, 75));
                    dict.Add("342", new ByotoRoom("05", "342", 10, 385, 150, 75));
                    dict.Add("343", new ByotoRoom("05", "343", 10, 310, 150, 75));
                    dict.Add("344", new ByotoRoom("05", "344", 10, 235, 150, 75));
                    dict.Add("345", new ByotoRoom("05", "345", 10, 160, 150, 75));
                    dict.Add("346", new ByotoRoom("05", "346", 10, 85, 150, 75));
                    dict.Add("347", new ByotoRoom("05", "347", 10, 10, 150, 75));

                    dict.Add("348", new ByotoRoom("05", "348", 340, 85, 150, 75));
                    dict.Add("349", new ByotoRoom("05", "349", 340, 160, 150, 75));
                }

                return dict;
            }
        }
    }
}
