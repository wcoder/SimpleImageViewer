//
// GallerySlidePageAdapter.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo[&]Gмаil.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Com.Davemorrissey.Labs.Subscaleview;
using FFImageLoading;
using JObject = Java.Lang.Object;

namespace ImageGalleryPoc
{
    public class GallerySlidePageAdapter : InfiniteViewPagerAdapter
    {
        private const int MaxZoom = 10;

        private readonly IList<string> _pages;
        private readonly LayoutInflater _inflater;

        //private readonly Dictionary<int, View> viewCache = new Dictionary<int, View>();

        public GallerySlidePageAdapter(Context context, IList<string> images)
        {
            _pages = images;
            _inflater = LayoutInflater.From(context);
        }

        public override int RealCount => _pages.Count;

        public override bool IsViewFromObject(View view, JObject obj)
        {
            return view == obj;
        }

        public override JObject InstantiateItem(ViewGroup container, int position)
        {
            var listPosition = ToRealPosition(position);

            View convertView;
            //if (viewCache.ContainsKey(listPosition))
            //{
            //    convertView = viewCache[listPosition];
            //    viewCache.Remove(listPosition);
            //}
            //else
            {
                convertView = _inflater.Inflate(Resource.Layout.gallery_image, null);
                BindView(convertView, listPosition);
                container.AddView(convertView);
                //viewCache.Add(listPosition, convertView);
            }

            return convertView;
        }

        public override void DestroyItem(ViewGroup container, int position, JObject view)
        {
            //var listPosition = ToRealPosition(position);

            //if (position == 0 || position == Count - 1)
            //{
            //    viewCache.Add(listPosition, (View)view);
            //}
            //else
            {
                container.RemoveView((View)view);
            }
        }

        private void BindView(View view, int listPosition)
        {
            var galleryImageView = view.FindViewById<SubsamplingScaleImageView>(Resource.Id.galleryImageView);
            galleryImageView.SetMinimumDpi(MaxZoom);

            var imageUrl = _pages[listPosition];

            ImageService.Instance
                .LoadUrl(imageUrl)
                .WithCache(FFImageLoading.Cache.CacheType.Disk)
                //.Finish(x =>
                //{
                //    var path = ((PlatformImageLoaderTask<object>)x).ImageInformation.FilePath;
                //    var source = Com.Davemorrissey.Labs.Subscaleview.ImageSource.ForUri(path);
                //    galleryImageView.SetImage(source);
                //})
                //.DownloadOnlyAsync();
                .IntoAsync(new SImageViewTarget(galleryImageView));

            var activityIndicatorView = view.FindViewById<ProgressBar>(Resource.Id.activityIndicatorView);
            if (!galleryImageView.IsImageLoaded)
            {
                activityIndicatorView.Visibility = ViewStates.Visible;
            }
            galleryImageView.ImageLoaded += (s, e) =>
            {
                if (galleryImageView.IsImageLoaded)
                {
                    activityIndicatorView.Visibility = ViewStates.Gone;
                }
            };
            galleryImageView.Click += GalleryImageView_Click;
        }

        private void GalleryImageView_Click(object sender, EventArgs e)
        {

        }
    }
}
