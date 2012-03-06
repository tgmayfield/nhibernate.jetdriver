using System;
using System.Collections;
using Iesi.Collections;

namespace NHibernate.JetDriver.Tests.ExtraLazy
{
    public class User
    {
        private string name;
        private string _passwordValue;
        private IDictionary session = new Hashtable();
        private ISet documents = new HashedSet();
        private ISet photos = new HashedSet();
        protected User() {}
        public User(string name, string passwordValue)
        {
            this.name = name;
            this._passwordValue = passwordValue;
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string PasswordValue
        {
            get { return _passwordValue; }
            set { _passwordValue = value; }
        }

        public virtual IDictionary Session
        {
            get { return session; }
            set { session = value; }
        }

        public virtual ISet Documents
        {
            get { return documents; }
            set { documents = value; }
        }


        public virtual ISet Photos
        {
            get { return photos; }
            set { photos = value; }
        }
    }
}