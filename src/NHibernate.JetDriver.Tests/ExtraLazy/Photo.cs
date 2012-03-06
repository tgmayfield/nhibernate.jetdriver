using System;

namespace NHibernate.JetDriver.Tests.ExtraLazy
{
    public class Photo
    {

        protected Photo() { }
        public Photo(string title, User owner)
        {
            this.Title = title;
            this.Owner = owner;
        }

        public virtual string Title
        {
            get;
            set;
        }

        public virtual User Owner
        {
            get;
            set;
        }
    }
}