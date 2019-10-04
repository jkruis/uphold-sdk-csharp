using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public enum AuthenticationTypes
    {
        Authy,
        Totp
    }

    public class AuthenticationMethod : BaseUpholdEntity
    {
        public string Id { get; set; }
        public bool Default { get; set; }
        public string Label { get; set; }
        public AuthenticationTypes Type { get; set; }
        public bool Verified { get; set; }
        public DateTime VerifiedAt { get; set; }
    }
}
