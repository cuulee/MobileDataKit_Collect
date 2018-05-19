using MobileDataKit_Collect.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
  public  class RealmWrapper
    {
        private Realms.Realm _Realm;
        public Realms.Realm Db
        {
            get
            {
                if (_Realm == null)
                {
                    var db_name = App.UserName;
                    if (string.IsNullOrWhiteSpace(db_name))
                        db_name = "defaulth";
                    var mt = new SafeRealmConfiguration(db_name +"n.realm");
                  
                   
                    _Realm = Realms.Realm.GetInstance(mt);
                    Transaction = _Realm.BeginWrite();
                }
                 
                return _Realm;
            }

        }


        private Realms.Transaction Transaction;
        public void Commit()
        {
            if(Transaction !=null)
            {
                try
                {
                    Transaction.Commit();
                    Transaction.Dispose();
                    this.Transaction = null;
                }
                catch
                {

                }
             
              
            }
           if(Transaction ==null)
            Transaction = Db.BeginWrite();
        }
    }
}
