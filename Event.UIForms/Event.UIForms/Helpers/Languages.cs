namespace Event.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string PasswordError => Resource.PasswordError;

        public static string LoginError => Resource.LoginError;

        public static string Login => Resource.Login;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Remember => Resource.Remember;


        public static string CurrentPassword => Resource.CurrentPassword;
        public static string IncorrectPassword => Resource.IncorrectPassword;

        public static string NewPassword => Resource.NewPassword;

        public static string PasswordLength => Resource.PasswordLength;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordMatch => Resource.PasswordMatch;

        public static string FirtName => Resource.FirtName;

        public static string LastName => Resource.LastName;

        public static string EnterCountry => Resource.EnterCountry;

        public static string SelectCity => Resource.SelectCity;

        public static string PhoneNumber => Resource.PhoneNumber;

        public static string UserUpdate => Resource.UserUpdate;

        public static string ok => Resource.ok;

        public static string EmailValid => Resource.EmailValid;

        public static string VoteClosed => Resource.VoteClosed;

    }

}
