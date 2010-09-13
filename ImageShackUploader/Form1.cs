/* Simple capture desktop and upload to imgur tool 
 * By Sathyajith Bhat - http://sathyabh.at/
 * Drop a mail - sathya@sathyasays. com
 * Thanks to Omkarnath for coding the grab only selection mode logic http://intelomkar.wordpress.com/2009/12/21/screencapture-library/
 * Legal stuff - You're free to use this app. Sending me a mail on how you've used it would be helpful :)
 * And yes -  I am not liable for anything you do. If you screw up - don't blame me. */


namespace imguruploader
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public partial class frmISUpload : Form
    {
        Image imgScreenCap;
        string strThumbnailLink = string.Empty;
        string strFullImageLink = string.Empty;
        private string strAPIKey = "1fcb49d680e14d21e2dd2a37bb4c3e47";
        private string strAPIBaseURL = "http://imgur.com/api/upload.xml";
        public frmISUpload()
        {
            InitializeComponent();
            if ( Properties.Settings.Default.imgformat.Equals(ImageFormat.Png))
            {
                pNGOutputToolStripMenuItem.Checked = true;
                
            }
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
                return true;
            }
            else
            {
                niSnap.BalloonTipText = "No image in clipboard to upload";
                niSnap.ShowBalloonTip(10000);
                return false;
            }
        }

        public bool UploadImage()
        {

            try
            {
                // the request header
                Dictionary<string, string> fields = new Dictionary<string, string>();
                fields.Add("key", strAPIKey);
                HttpWebRequest hr = WebRequest.Create(strAPIBaseURL) as HttpWebRequest;
                string bound = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                hr.ContentType = "multipart/form-data; boundary=" + bound;
                hr.Method = "POST";
                hr.KeepAlive = true;
                hr.Credentials = CredentialCache.DefaultCredentials;

                byte[] boundBytes = Encoding.ASCII.GetBytes("\r\n--" + bound + "\r\n");
                string formDataTemplate = "\r\n--" + bound + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

                //add fields + a boundary
                MemoryStream fieldData = new MemoryStream();
                foreach (string key in fields.Keys)
                {
                    byte[] formItemBytes = Encoding.UTF8.GetBytes(
                            string.Format(formDataTemplate, key, fields[key]));
                    fieldData.Write(formItemBytes, 0, formItemBytes.Length);
                }
                fieldData.Write(boundBytes, 0, boundBytes.Length);

                //calculate the total length we expect to send
                string headerTemplate =
                        "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                long fileBytes = 0;

                byte[] headerBytes = Encoding.UTF8.GetBytes(
                        String.Format(headerTemplate, "image", "upload.jpg"));
                FileStream fs = new FileStream("upload.jpg", FileMode.Open, FileAccess.Read);

                fileBytes += headerBytes.Length;
                fileBytes += fs.Length;
                fileBytes += boundBytes.Length;
                hr.ContentLength = fieldData.Length + fileBytes;

                Stream s = hr.GetRequestStream();

                fieldData.WriteTo(s);


                s.Write(headerBytes, 0, headerBytes.Length);

                int bytesRead = 0;
                long bytesSoFar = 0;
                byte[] buffer = new byte[10240];
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                {
                    bytesSoFar += bytesRead;
                    s.Write(buffer, 0, bytesRead);
                }
                s.Write(boundBytes, 0, boundBytes.Length);
                fs.Close();
                s.Close();

                HttpWebResponse response = hr.GetResponse() as HttpWebResponse;
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                XmlDocument responseXml = new XmlDocument();
                responseXml.LoadXml(responseString);
                if (responseXml["rsp"].Attributes["stat"].Value == "ok")
                {
                    strFullImageLink = responseXml["rsp"]["original_image"].InnerText;
                    strThumbnailLink = responseXml["rsp"]["large_thumbnail"].InnerText;

                }

                niSnap.BalloonTipText = ("Image has been uploaded to imgur and link has been copied to clipboard");
                Clipboard.SetText(strFullImageLink);
                niSnap.ShowBalloonTip(500000);
                GC.Collect();
                return true;
            }
            catch (Exception excError)
            {
                MessageBox.Show("Error: " + excError.Message.ToString());
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
            //   sc.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg; //jpeg is default
            sc.ImageFormat = Properties.Settings.Default.imgformat;
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

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void pNGOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pNGOutputToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.imgformat = ImageFormat.Jpeg;
                Properties.Settings.Default.Save();
                pNGOutputToolStripMenuItem.Checked = false;
            }
            else
            {
                Properties.Settings.Default.imgformat = ImageFormat.Png;
                Properties.Settings.Default.Save();
                pNGOutputToolStripMenuItem.Checked = true;
               


            }

        }
    }
}
