

namespace CameraApp
{

    using System;
    using System.Collections.Generic;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Graphics;
    using Android.OS;
    using Android.Provider;
    using Android.Widget;
    using Java.IO;
    using Environment = Android.OS.Environment;
    using Uri = Android.Net.Uri;
    //creating a new static class with variables
    //do no forget to give permission to app to access external memory
    //and camera in manifest
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;

    }
    [Activity(Label = "CameraApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private ImageView _imageView;



        //after class is created and permissions are given we need to update this OnCreate
        //function 

        //I believe this code here checks to see if there is an app to take pictures
        //which is a way to check to see if there is a camera on the device. 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if(IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures(); 
           
      
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.launchCameraButton);
            _imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            button.Click += TakeAPicture;
            

            }

            //Good and helpful references to help with camera app.
            //https://developer.xamarin.com/api/type/Android.Hardware.Camera/
            //http://www.c-sharpcorner.com/article/creating-a-camera-app-in-xamarin-android-app-using-visual-studio-2015/
            //https://developer.xamarin.com/recipes/android/other_ux/camera_intent/take_a_picture_and_save_using_camera_app/
        }

        private void CreateDirectoryForPictures ()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                     Environment.DirectoryPictures), "CameraApp");
            if(!App._dir.Exists())
            {
                App._dir.Mkdir();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            //gotta change this is
        
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }
        //Next step is to see where my images are being stored and if not create a view with a gallery. 
        //https://developer.xamarin.com/guides/android/getting_started/hello,android_multiscreen/hello,android_multiscreen_quickstart/

    }


}

