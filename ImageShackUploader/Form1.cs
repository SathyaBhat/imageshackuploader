/* Simple capture desktop and upload to Imageshack tool 
 * By Sathyajith Bhat - http://sathyabh.at/
 * Drop a mail - sathya@sathyasays.com
 * Uses Bryce's C# Wrapper for Imageshack API ( http://www.codeemporium.com/2009/06/14/dot-net-c-sharp-wrapper-for-the-imageshack-xml-api/ )
 * Thanks to Omkarnath for coding the grab only selection mode logic http://intelomkar.wordpress.com/2009/12/21/screencapture-library/
 * Legal stuff - You're free to use this app. Sending me a mail on how you've used it would be helpful :)
 * And yes -  I am not liable for anything you do. If you screw up - don't blame me. */


namespace ImageShackUploader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Net;
    using System.IO;
    using System.Windows.Forms;
    using System.Drawing.Imaging;
    using Brycestrosoft.ImageShackAPIWrapper;

    public partial class frmISUpload : Form
    {
        Image imgScreenCap;
        string strThumbnailLink = string.Empty;
        string strFullImageLink=string.Empty;
        public frmISUpload()
        {
            InitializeComponent();
        }

        private void niSnap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CaptureScreen())
                UploadImage();
        }

        public bool CaptureScreen()
        {
            string strFilename = "upload.jpg"; // chosen an arbitrary upload.jpeg filename
            // TO DO: Create a fancy thingamajig to randomize file name            
            if (Clipboard.ContainsImage())
            {
                imgScreenCap = Clipboard.GetImage();
                imgScreenCap.Save(strFilename, ImageFormat.Jpeg);
                System.Threading.Thread.Sleep(1000);
                imgScreenCap.Dispose();
                //   niSnap.BalloonTipText = "Image saved";
                //  niSnap.ShowBalloonTip(5000);
                return true;
            }
            else
            {
                niSnap.BalloonTipText ="No image in clipboard to upload";
                niSnap.ShowBalloonTip(10000);
                return false;
            }
        }

        public bool UploadImage()
        {
            UploadedImageDetails ImageDetails;
            
            IImageShackUploader uploader = new StandardImageShackUploader();
            try
            {
                ImageDetails = uploader.UploadImage("upload.jpg");
                niSnap.BalloonTipText = ("Image has been uploaded to ImageShack and link has been copied to clipboard");
                Clipboard.SetText(ImageDetails.ImageLink.ToString());
                strFullImageLink = ImageDetails.ImageLink.ToString();
                strThumbnailLink = ImageDetails.ThumbLink.ToString();
                niSnap.ShowBalloonTip(500000);
                GC.Collect();
                return true;
            }
            catch (Exception excError)
            {
                MessageBox.Show("Error: " +  excError.Message.ToString());
                GC.Collect();
                return false;
            }
        }

        private void frmISUpload_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Close();
        }

        private void SelModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            screenCapture.screenCapture sc = new screenCapture.screenCapture();
            sc.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg; //jpeg is default
            sc.imageName = "upload.jpg"; //default is img
            //above 2 lines are optional
            sc.showCanvas();
            System.Threading.Thread.Sleep(1000);
            UploadImage();
        }

        private void copyThumbnailLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (strThumbnailLink.Equals(""))
            {
                niSnap.BalloonTipText = "Nothing to copy!";
                niSnap.ShowBalloonTip(10000);
            }
            else
            {
                Clipboard.SetText(strThumbnailLink);
            }
        }
    }
}
