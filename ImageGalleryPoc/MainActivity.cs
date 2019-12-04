using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.View;

namespace ImageGalleryPoc
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private GallerySlidePageAdapter _adapter;
        private ViewPager _viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitComponents();
        }

        protected void InitComponents()
        {
            var urls = new string[] {
                "http://www.zoopicture.ru/assets/2013/04/4384474089_9c5f6329bb_z.jpg",
                "https://github.com/davemorrissey/subsampling-scale-image-view/raw/master/sample/assets/card.png",
                "http://www.zoopicture.ru/assets/2013/04/2748131046_8a253489b5_z.jpg",
                "http://www.zoopicture.ru/assets/2013/04/8512005268_a3e71c7d80_z.jpg",
                "http://www.zoopicture.ru/assets/2013/04/7338580328_862cff6e3c_b.jpg"
            };

            _adapter = new GallerySlidePageAdapter(this, urls);

            _viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            //_viewPager.AddOnPageChangeListener(new InfinitePageChangeListener(_viewPager));
            _viewPager.Adapter = _adapter;
            _viewPager.SetCurrentItem(3, false); // start item
            _viewPager.OffscreenPageLimit = 10;
            _viewPager.PageSelected += OnPageSelected;
            _viewPager.PageScrollStateChanged += ViewPager_PageScrollStateChanged;
        }

        private void ViewPager_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            if (e.State == ViewPager.ScrollStateIdle) // pseudo-infinite sliding
            {
                var adapter = (InfiniteViewPagerAdapter)_viewPager.Adapter;
                var position = _viewPager.CurrentItem;
                var realPosition = adapter.ToRealPosition(position);

                if (position == 0 || position == adapter.Count - 1)
                {
                    _viewPager.SetCurrentItem(realPosition + 1, false);
                }
            }
        }

        private void OnPageSelected(object s, ViewPager.PageSelectedEventArgs e)
        {

        }
    }
}