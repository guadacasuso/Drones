using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using File = Java.IO.File;

namespace VideoUploader
{
    [Activity(Label = "Video Upload", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private EditText txtDir;
        private ProgressBar pb;
        private Button btn;

        private const string FOLDER = "mnt/sdcard/DCIM/AR.Drone/";

        private const string BLOB_NAME = "videosdrone";
        private const string BLOB_KEY = "Bup/2MnQ+B8HLI2jn+kfXfFype6jAqxjwz/n+fO7iXS50OjIt5InCmTTlv29zkgX7AXunaYDYXS5YJLpF3npbQ==";

        private const string BLOB_FOLDER = "videos";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            btn = FindViewById<Button>(Resource.Id.btnUpload);
            txtDir = FindViewById<EditText>(Resource.Id.txtDir);
            pb = FindViewById<ProgressBar>(Resource.Id.pb);
            pb.Visibility = ViewStates.Invisible;
            txtDir.Text = FOLDER;
            btn.Click += ButtonOnClick;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            btn.Enabled = false;
            pb.Visibility = ViewStates.Visible;
            var h = new Handler();
            h.PostDelayed(Upload, 100);
        }

        private void Upload()
        {
            try
            {
                var finalFile = new File(txtDir.Text);
                var files = finalFile.ListFiles();
                var fileList = new Dictionary<string, DateTime>();
                foreach (var f in files)
                {
                    if (f.Name.StartsWith("video_") && f.Name.EndsWith(".mp4"))
                    {
                        var parts = f.Name.Split('_');
                        var date = parts[1];
                        var time = parts[2];
                        var year = int.Parse(date.Substring(0, 4));
                        var month = int.Parse(date.Substring(4, 2));
                        var day = int.Parse(date.Substring(6, 2));
                        var hour = int.Parse(time.Substring(0, 2));
                        var min = int.Parse(time.Substring(2, 2));
                        var sec = int.Parse(time.Substring(4, 2));
                        var datetime = new DateTime(year, month, day, hour, min, sec);
                        fileList.Add(f.Name, datetime);
                    }
                }
                var newest = fileList.OrderByDescending(f => f.Value.Ticks).First();
                var cred = new StorageCredentials(BLOB_NAME, BLOB_KEY);
                var client = new CloudStorageAccount(cred, true).CreateCloudBlobClient();
                var cont = client.GetContainerReference(BLOB_FOLDER);
                cont.CreateIfNotExists();
                var blob = cont.GetBlockBlobReference(newest.Key);
                var file = new File(txtDir.Text + newest.Key).AbsolutePath;
                using (var sr = System.IO.File.OpenRead(file))
                {
                    blob.UploadFromStream(sr);
                    var ad = new AlertDialog.Builder(this).Create();
                    ad.SetTitle("VIDEO UPLOADED!");
                    ad.SetMessage(newest.Key);
                    ad.SetButton("OK", (a, b) => ad.Dismiss());
                    ad.SetCancelable(false);
                    ad.Show();
                }
            }
            catch (Exception ex)
            {
                var ad = new AlertDialog.Builder(this).Create();
                ad.SetTitle("ERROR UPLOADING THE FILE");
                ad.SetMessage(ex.Message);
                ad.SetButton("OK", (a, b) => ad.Dismiss());
                ad.SetCancelable(false);
                ad.Show();
            }
        }
    }
}

