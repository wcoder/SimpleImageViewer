//
// GallerySlidePageAdapter.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo[&]Gмаil.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using Android.Support.V4.View;

namespace ImageGalleryPoc
{
    public abstract class InfiniteViewPagerAdapter : PagerAdapter
    {
        // Since we want to put 2 additional pages at left & right,
        // the actual size will plus 2.
        public override int Count => RealCount == 0 ? 0 : RealCount + 2;

        public abstract int RealCount { get; }

        public int ToRealPosition(int position)
        {
            var realCount = RealCount;

            if (realCount == 0)
            {
                return 0;
            }

            // Put last page model to the first position.
            if (position == 0)
            {
                return realCount - 1;
            }

            // Put first page model to the last position.
            if (position >= realCount + 1)
            {
                return 0;
            }

            return position - 1;
        }
    }
}
