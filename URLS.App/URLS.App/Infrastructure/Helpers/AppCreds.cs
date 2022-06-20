using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Helpers
{
    public class AppCreds
    {
        public static AppLoginCreateModel GetApp()
        {
            var app = new AppLoginCreateModel();
            app.Version = AppInfo.Current.VersionString;

            var platfrom = DeviceInfo.Platform;

            if (platfrom == DevicePlatform.Android)
            {
                app.Id = "F7Gv-2mu8-rH7Z-U4pt";
                app.Secret = "bUqNPgQhJoj8oHyIsz2cwICuxTCGo0oBLDaBODiQ0pOLnLn6H99IEJipavIwHhbEzf5Y4s";
            }

            if (platfrom == DevicePlatform.iOS || platfrom == DevicePlatform.macOS || platfrom == DevicePlatform.MacCatalyst)
            {
                app.Id = "5BoG-NGOS-Ofqu-6mmS";
                app.Secret = "WI6IygntT3a7ur39Ndv6w6bsvWYYz31PMg6HdKfKzz3Wsi8q4L5aA65hwfzZZqcxOkahf6";
            }

            if (platfrom == DevicePlatform.WinUI)
            {
                app.Id = "T2Ke-RJUB-lV0u-BHpB";
                app.Secret = "cc8Qa7fILdAmmxiZszfaxzMUrJSPZKGagVxOqvhmyWwxznGHKyNpGLBOzpOBxdoOHyxcRY";
            }

            if (platfrom == DevicePlatform.Tizen || platfrom == DevicePlatform.tvOS || platfrom == DevicePlatform.Unknown)
            {
                app.Id = "T2KU-RJUB-lV0u-BHpB";
                app.Secret = "cc8Qa7fILdAmmxiZszfaxzMUrJSPZKGagVxOqvhmyWwxznGHKyNpGLBOzpOBxdoGHyxcRY";
            }

            return app;
        }
    }
}
