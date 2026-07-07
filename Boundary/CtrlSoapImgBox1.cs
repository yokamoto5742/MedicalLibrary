using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Boundary
{
    public class CtrlSoapImgBox1 : PictureBox
    {
        public SoapImg SoapImg1;

        public CtrlSoapImgBox1(SoapImg soap_img)
        {
            this.Init(soap_img);
        }

        public void Init(SoapImg soap_img)
        {
            this.SoapImg1 = soap_img;

            if (soap_img.ImgTmpExist)
            {
                // ローカルのテンポラリにある場合は、それを取得する。
                this.BackgroundImage = Image.FromFile(soap_img.ImgTmpPath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (soap_img.ImgExist)
            {
                // ローカルになく、サーバーにある場合は、それを取得する。
                this.BackgroundImage = Image.FromFile(soap_img.ImgPath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
    }
}
