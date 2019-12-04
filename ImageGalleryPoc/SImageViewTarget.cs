//
// GallerySlidePageAdapter.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo[&]Gмаil.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using Com.Davemorrissey.Labs.Subscaleview;
using FFImageLoading.Drawables;
using FFImageLoading.Work;

namespace ImageGalleryPoc
{
    public class SImageViewTarget : ITarget<SelfDisposingBitmapDrawable, SubsamplingScaleImageView>
    {
        public SImageViewTarget(SubsamplingScaleImageView imageView)
        {
            Control = imageView;
        }

        public SubsamplingScaleImageView Control { get; }

        public bool IsValid => true;

        public object TargetControl => Control;

        public void Set(IImageLoaderTask task, SelfDisposingBitmapDrawable image, bool animated)
        {
            var source = Com.Davemorrissey.Labs.Subscaleview.ImageSource.ForBitmap(image.Bitmap);
            Control.SetImage(source);
        }

        public void SetAsEmpty(IImageLoaderTask task)
        {
        }
    }
}
