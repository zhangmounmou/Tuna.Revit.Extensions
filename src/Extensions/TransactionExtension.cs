﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension
{
    public static class TransactionExtension
    {
        /// <summary>
        /// This is a function which used to start a document transaction
        /// </summary>
        /// <param name="document"></param>
        /// <param name="action"></param>
        /// <param name="name"></param>
        /// <returns>If document is read only,return <see cref="Autodesk.Revit.DB.TransactionStatus.Error"/></returns>
        public static TransactionStatus NewTransaction(this Document document, Action action, bool rollback = false, string name = "Default Transaction Name")
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            return NewTransaction(document, (d) => action.Invoke(), rollback, name);
        }

        /// <summary>
        /// This is a function which used to start a document transaction
        /// </summary>
        /// <param name="document"></param>
        /// <param name="action"></param>
        /// <param name="name"></param>
        /// <returns>If document is read only,return <see cref="Autodesk.Revit.DB.TransactionStatus.Error"/></returns>
        public static TransactionStatus NewTransaction(this Document document, Action<Document> action, bool rollback = false, string name = "Default Transaction Name")
        {
            if (!document!.IsValidObject)
            {
                throw new ArgumentNullException(nameof(document), "document is null or invalid object");
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            if (document.IsReadOnly)
            {
                return TransactionStatus.Error;
            }

            using (Transaction transaction = new Transaction(document, name))
            {
                if (transaction.Start() == TransactionStatus.Started)
                {
                    action.Invoke(document);
                    if (!rollback)
                    {
                        return transaction.Commit();
                    }
                }
                return transaction.RollBack(transaction.GetFailureHandlingOptions().SetClearAfterRollback(true));
            }
        }

        /// <summary>
        /// This is a function which used to start a document SubTransaction 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TransactionStatus NewSubtransaction(this Document document, Action action, bool rollback = false)
        {
            if (!document!.IsValidObject)
            {
                throw new ArgumentNullException(nameof(document), "document is null or invalid object");
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            using (SubTransaction transaction = new SubTransaction(document))
            {
                if (transaction.Start() == TransactionStatus.Started)
                {
                    action.Invoke();
                    if (!rollback)
                    {
                        return transaction.Commit();
                    }
                }
                return transaction.RollBack();
            }
        }

        /// <summary>
        /// This is a function which used to start a document Transaction Group
        /// </summary>
        /// <param name="document"></param>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TransactionStatus NewTransactionGroup(this Document document, Action action, string name = "Default Transaction Group Name", bool rollback = false, bool assimilate = true)
        {
            if (!document!.IsValidObject)
            {
                throw new ArgumentNullException(nameof(document), "document is null or invalid object");
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            using (TransactionGroup tsg = new TransactionGroup(document, name))
            {
                if (tsg.Start() == TransactionStatus.Started)
                {
                    action.Invoke();
                    if (!rollback)
                    {
                        return assimilate ? tsg.Assimilate() : tsg.Commit();
                    }
                }
                return tsg.RollBack();
            }
        }


        public static void NewTransactionGroup(this Document document, Action<TransactionGroup> action, string name = "Default Transaction Group Name")
        {
            if (!document!.IsValidObject)
            {
                throw new ArgumentNullException(nameof(document), "document is null or invalid object");
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "action can not be null");
            }

            using (TransactionGroup tsg = new TransactionGroup(document, name))
            {
                if (tsg.Start() == TransactionStatus.Started)
                {
                     action.Invoke(tsg);
                }
            }
        }
    }
}
